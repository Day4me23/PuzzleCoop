using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndScene : MonoBehaviour
{
    [SerializeField] GameObject Background;
    private void Start()
    {
        StartCoroutine(Thing());
    }
    IEnumerator Thing()
    {
        yield return new WaitForSecondsRealtime(15);
        SceneManager.LoadScene("Main_Menu");
    }
}
