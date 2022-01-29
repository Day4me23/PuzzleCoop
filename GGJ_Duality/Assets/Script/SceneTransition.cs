using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    int count = 0;
    [SerializeField] int sceneNumber;
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.GetComponent<PlayerMovement>() != null)
        {
            count++;
            if (count >= 2)
                SceneManager.LoadScene(sceneNumber);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.GetComponent<PlayerMovement>() != null)
            count--;
    }
}
