using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager instance;
    private void Awake()
    {
        DontDestroyOnLoad(this);
        if (instance == null)
            instance = this;
        else Destroy(gameObject);
    }
    #endregion
    [SerializeField] List<Transform> players = new List<Transform>();
    private void Start()
    {
        GetPlayers();
    }

    void GetPlayers()
    {
        GameObject[] temp = GameObject.FindGameObjectsWithTag("player");
        foreach (GameObject player in temp) players.Add(player.transform);
    }
}
