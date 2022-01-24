using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : Trigger {
    public virtual void Interact() {
        Debug.Log("Interacting with: " + this);
    }
    public virtual void Overview() {
        Debug.Log("Looking at: " + this);
    }
}
