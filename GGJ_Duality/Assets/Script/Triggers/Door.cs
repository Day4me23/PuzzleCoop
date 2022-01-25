using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Triggerable
{

    [SerializeField] private Transform leftClosedState;
    [SerializeField] private Transform leftOpenState;

    [SerializeField] private Transform rightClosedState;
    [SerializeField] private Transform rightOpenState;

    [SerializeField] private float speed;

    
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            if (CheckIfDoorCanOpen()) {

            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.CompareTag("Player")) {

        }
    }

    /// <summary>
    /// Checks to see if the door is unlocked. 
    /// </summary>
    public bool CheckIfDoorCanOpen() {

    }

    IEnumerator OpenDoor() {

    }

    IEnumerator CloseDoor() {

    }
}
