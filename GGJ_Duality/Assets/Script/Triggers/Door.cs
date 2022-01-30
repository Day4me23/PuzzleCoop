using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Triggerable
{
    [Header("Door Variables")]
    //door objects
    [SerializeField] private GameObject leftDoor;
    [SerializeField] private GameObject rightDoor;

    [SerializeField] private Transform leftClosedState;
    [SerializeField] private Transform leftOpenState;

    [SerializeField] private Transform rightClosedState;
    [SerializeField] private Transform rightOpenState;

    [SerializeField] private float totalTime;
    [SerializeField] private bool opened;
    
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            Debug.Log("Player is near the door!");
            if (!opened) { //if not opened
                if (CheckIfActive()) {
                    //checks to see if the door can open

                    StopAllCoroutines();
                    StartCoroutine(OpenDoor(totalTime));
                }
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            if (opened) {
                StopAllCoroutines();
                StartCoroutine(CloseDoor(totalTime));
            }
        }
    }

    IEnumerator OpenDoor(float time) {
        opened = true;
        Vector3 startingLeftPos = leftDoor.transform.position;
        Vector3 startingRightPos = rightDoor.transform.position;

        float elapsedTime = 0;

        while (elapsedTime < time) {
            //leftSide
            leftDoor.transform.position = Vector3.Lerp(startingLeftPos, leftOpenState.position, (elapsedTime / time));
            rightDoor.transform.position = Vector3.Lerp(startingRightPos, rightOpenState.position, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        leftDoor.transform.localPosition = leftOpenState.localPosition;
        rightDoor.transform.localPosition = rightOpenState.localPosition;
    }

    IEnumerator CloseDoor(float time) {
        opened = false;
        Vector3 startingLeftPos = leftDoor.transform.position;
        Vector3 startingRightPos = rightDoor.transform.position;

        float elapsedTime = 0;

        while (elapsedTime < time) {
            //leftSide
            leftDoor.transform.position = Vector3.Lerp(startingLeftPos, leftClosedState.position, (elapsedTime / time));
            rightDoor.transform.position = Vector3.Lerp(startingRightPos, rightClosedState.position, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        leftDoor.transform.localPosition = leftClosedState.localPosition;
        rightDoor.transform.localPosition = rightClosedState.localPosition;
    } 
}
