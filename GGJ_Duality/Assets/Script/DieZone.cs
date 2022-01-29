using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.GetComponent<PlayerMovement>() != null)
        {
            other.GetComponent<Transform>().position = other.transform.GetComponent<PlayerMovement>().currentCheckpoint.position;
        }
    }
}
