using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndScene : MonoBehaviour
{
    [SerializeField] GameObject Background;
    public void END()
    {
        Background.SetActive(true);
        StartCoroutine(Thing());
    }
    IEnumerator Thing()
    {
        yield return new WaitForSecondsRealtime(10);
        SceneManager.LoadScene("Main_Menu");
    }
}
