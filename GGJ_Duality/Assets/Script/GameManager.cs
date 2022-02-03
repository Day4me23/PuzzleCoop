using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform oneOrigin;
    [SerializeField] private Transform twoOrigin;

    [SerializeField] private TextMeshProUGUI waitingText;
    [SerializeField] private GameObject waitingScreen;

    [SerializeField] private Image player_one_left_Circle;
    [SerializeField] private Image player_one_right_Circle;
    [SerializeField] private Image player_two_left_Circle;
    [SerializeField] private Image player_two_right_Circle;

    [SerializeField] private TextMeshProUGUI player_one_leftText;
    [SerializeField] private TextMeshProUGUI player_one_rightText;
    [SerializeField] private TextMeshProUGUI player_two_leftText;
    [SerializeField] private TextMeshProUGUI player_two_rightText;

    [SerializeField] private GameObject playerOneModel;
    [SerializeField] private GameObject playerTwoModel;
    [SerializeField] private LayerMask player_one_layer;
    [SerializeField] private LayerMask player_two_layer; 

    public bool levelHasStarted;
    #region Singleton
    public static GameManager instance;
    private int amountOfPlayers = 0;


    private void Awake()
    {
        DontDestroyOnLoad(this);
        if (instance == null)
            instance = this;
        else Destroy(gameObject);
    }
    #endregion
    [SerializeField] List<GameObject> players = new List<GameObject>();
    

    private void Start() {
        waitingText.text = "Waiting for TWO Players...";
    }



    public void StartLevel() {
        //dumb
        players[0].GetComponent<CharacterController>().enabled = false; // <----- very dumb
        players[1].GetComponent<CharacterController>().enabled = false;
        players[0].transform.position = oneOrigin.position;
        players[1].transform.position = twoOrigin.position;
        players[0].GetComponent<CharacterController>().enabled = true; //<---- like why even?
        players[1].GetComponent<CharacterController>().enabled = true;

        players[0].transform.GetChild(1).GetComponent<Camera>().cullingMask &= ~player_one_layer;
        players[1].transform.GetChild(1).GetComponent<Camera>().cullingMask &= ~player_two_layer;
        // -_-
        levelHasStarted = true;
    }

    public void GetPlayers()
    {
        amountOfPlayers++;
        //Change back to 2 players
        if (amountOfPlayers == 2) {
            Debug.Log("Grabbing players!");
            players.Clear();
            GameObject[] temp = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject player in temp) players.Add(player.gameObject);
            // Remove after testing
           // foreach (GameObject player in temp) players.Add(player.gameObject);

            players[0].GetComponent<PlayerMovement>().oppositePlayer = players[1].GetComponent<PlayerMovement>();
            players[1].GetComponent<PlayerMovement>().oppositePlayer = players[0].GetComponent<PlayerMovement>();
            GameObject model_1 = Instantiate(playerOneModel, players[0].GetComponent<PlayerMovement>().model.position, Quaternion.identity);
            model_1.transform.parent = players[0].GetComponent<PlayerMovement>().model;
            model_1.transform.localPosition = Vector3.zero;
            model_1.transform.localRotation = Quaternion.Euler(0, 0, 0);
            players[0].transform.GetChild(1).GetComponent<CameraMovement>().headModel = GameObject.FindGameObjectWithTag("Player_One_Head_Model");

            GameObject model_2 = Instantiate(playerTwoModel, players[1].GetComponent<PlayerMovement>().model.position, Quaternion.identity);
            model_2.transform.parent = players[1].GetComponent<PlayerMovement>().model;
            model_2.transform.localPosition = Vector3.zero;
            model_2.transform.localRotation = Quaternion.Euler(0, 0, 0);
            players[1].transform.GetChild(1).GetComponent<CameraMovement>().headModel = GameObject.FindGameObjectWithTag("Player_Two_Head_Model");
            waitingScreen.SetActive(false);
            StartLevel();
        } else {
            waitingText.text = "Waiting for One Player...";
            return;
        }
    }

    public void UpdateUIOrbs() {
        UpdatePlayerOneUI(players[0]);
        UpdatePlayerTwoUI(players[1]);
    }

    private void UpdatePlayerOneUI(GameObject player) {

        player_one_leftText.text = player.GetComponent<OrbHolderManager>().firstOrbName;
        player_one_rightText.text = player.GetComponent<OrbHolderManager>().secondOrbName;

        if (player.GetComponent<OrbHolderManager>().firstOrb != -1) {
            player_one_left_Circle.color = OrbList.instance.orbMechanics[player.GetComponent<OrbHolderManager>().firstOrb].orbColor;
        } else {
            player_one_left_Circle.color = Color.black;
        }

        if(player.GetComponent<OrbHolderManager>().secondOrb != -1) {
            player_one_right_Circle.color = OrbList.instance.orbMechanics[player.GetComponent<OrbHolderManager>().secondOrb].orbColor;
        } else {
            player_one_right_Circle.color = Color.black;
        }
        
    }
    private void UpdatePlayerTwoUI(GameObject player) {

        player_two_leftText.text = player.GetComponent<OrbHolderManager>().firstOrbName;
        player_two_rightText.text = player.GetComponent<OrbHolderManager>().secondOrbName;

        if (player.GetComponent<OrbHolderManager>().firstOrb != -1) {
            player_two_left_Circle.color = OrbList.instance.orbMechanics[player.GetComponent<OrbHolderManager>().firstOrb].orbColor;
        } else {
            player_two_left_Circle.color = Color.black;
        }
        
        if(player.GetComponent<OrbHolderManager>().secondOrb != -1) {
            player_two_right_Circle.color = OrbList.instance.orbMechanics[player.GetComponent<OrbHolderManager>().secondOrb].orbColor;
        } else {
            player_two_right_Circle.color = Color.black;
        }
        

    }

}
