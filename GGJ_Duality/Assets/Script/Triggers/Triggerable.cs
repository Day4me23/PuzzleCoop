using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triggerable : MonoBehaviour {
    [Header("Triggerable Variables")]
    [SerializeField] List<Trigger> triggers;
    public bool active;
   /* private void Update() {
        int count = 0;
        for (int i = 0; i < triggers.Count - 1; i++) {
            if (triggers[i] != null) {
                if (triggers[i].active) {
                    count++;
                }
            }
        }
        active = (count == triggers.Count);
    } */

    public bool CheckIfActive() {
        int count = 0;
        for (int i = 0; i < triggers.Count - 1; i++) {
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