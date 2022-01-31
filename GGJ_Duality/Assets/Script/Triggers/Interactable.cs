using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : Trigger {
    public virtual void Interact(GameObject obj) {
        Debug.Log("Interacting with: " + this);
        foreach (Triggerable source in sources)
            source.CheckIfActive();
    }
    public virtual void Overview() {
        Debug.Log("Looking at: " + this);
    }
}
