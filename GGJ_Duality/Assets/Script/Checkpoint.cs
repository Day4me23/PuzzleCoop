using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.GetComponent<PlayerMovement>() != null)
        {
            other.transform.GetComponent<PlayerMovement>().currentCheckpoint = transform;
        }
    }
}
