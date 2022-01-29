using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
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

    public void GetPlayers()
    {
        amountOfPlayers++;
        if (amountOfPlayers == 2) {
            Debug.Log("Grabbing players!");
            players.Clear();
            GameObject[] temp = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject player in temp) players.Add(player.gameObject);

            players[0].GetComponent<PlayerMovement>().oppositePlayer = players[1].GetComponent<PlayerMovement>();
            players[1].GetComponent<PlayerMovement>().oppositePlayer = players[0].GetComponent<PlayerMovement>();
        } else {
            return;
        }
    }
}
