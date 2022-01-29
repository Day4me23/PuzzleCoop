using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbList : MonoBehaviour
{
    public static OrbList instance;
    public List<Orb> orbMechanics = new List<Orb>();

    private void Awake() {
        if(instance == null) {
            instance = this;
        } else {
            Destroy(this.gameObject);
        }
    }
}
