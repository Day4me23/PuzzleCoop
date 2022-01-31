using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class CameraMovement : MonoBehaviour {
    public GameObject headModel;
    private float headRotation = 37.328f;
    public float sensitivity;
    private float xRotation = 0f;
    float x, y;
    private void Start() {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void LateUpdate() {
        xRotation -= y;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        headRotation -= y/2.5f;
        // headRotation = Mathf.Clamp(headRotation, -53.328f, 127.328f);
        headRotation = Mathf.Clamp(headRotation, 0f, 70f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        headModel.transform.localRotation = Quaternion.Euler(headRotation, 0f, 0f);

        transform.parent.Rotate(Vector3.up * x);
    }

    public void CamMove(InputAction.CallbackContext context) {
        x = context.ReadValue<float>() * (sensitivity / 2);

    }

    public void CamY(InputAction.CallbackContext context) {
        y = context.ReadValue<float>() * (sensitivity / 2);
    }
}