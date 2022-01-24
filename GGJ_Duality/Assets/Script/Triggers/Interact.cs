using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour {
    static float range = 3.5f;
    Transform cam;
    private void Awake() {
        cam = Camera.main.transform;
    }
    void Update() {
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range)) {
            if (hit.transform.GetComponent<Interactable>() == null) return;
            hit.transform.GetComponent<Interactable>().Overview();
            if (Input.GetKeyDown(KeyCode.E)) hit.transform.GetComponent<Interactable>().Interact();
        }
    }
}
