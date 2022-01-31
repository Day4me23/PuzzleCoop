using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieZone : MonoBehaviour
{
    /*private void OnCollisionEnter(Collider other)
    {
        if(other.collider.gameObject.CompareTag)
        if (other.transform.GetComponent<PlayerMovement>() != null)
        {
            other.GetComponent<Transform>().position = other.transform.GetComponent<PlayerMovement>().currentCheckpoint.position;
        }
    } */

    /*private void OnCollisionEnter(Collision collision) {
        if (collision.collider.gameObject.CompareTag("Player"))
            Debug.Log("A player has landed in a deadZone");
            if (collision.transform.GetComponent<PlayerMovement>() != null) {
                collision.gameObject.GetComponent<Transform>().position = collision.transform.GetComponent<PlayerMovement>().currentCheckpoint.position;
            }
    } */

    private void OnTriggerEnter(Collider collision) {
        if (collision.gameObject.CompareTag("Player")) {
            Debug.Log("A player has landed in a deadZone");
            if (collision.transform.GetComponent<PlayerMovement>() != null) {
                collision.gameObject.transform.position = collision.transform.GetComponent<PlayerMovement>().currentCheckpoint.position;
            }
        }
            
        
    }
}
