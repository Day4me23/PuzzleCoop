using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triggerable : MonoBehaviour 
{
    [SerializeField] List<Trigger> triggers;
    public bool active;

    public bool CheckIfActive() {
        int count = 0;
        for (int i = 0; i < triggers.Count; i++) {
            if (triggers[i] != null) {
                if (triggers[i].active) {
                    count++;
                }
            }
        }
        active = (count == triggers.Count);
        return active;
    }
}