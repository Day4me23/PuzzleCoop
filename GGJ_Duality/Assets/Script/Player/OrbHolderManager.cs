using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbHolderManager : MonoBehaviour
{
    private Orb firstOrb, secondOrb;

    public void AddOrb(Orb orb) {
        if(firstOrb == null) {
            firstOrb = orb;
        } else if (secondOrb == null) {
            secondOrb = orb;
        } else {
            return;
        }

        ChangeMechanics();

    }

    public void RemoveOrb() {
        //throwing
    }

    public Orb GetFirstOrb() {
        return firstOrb;
    }

    public Orb GetSecondOrb() {
        return secondOrb;
    }

    private void ChangeMechanics() {
        firstOrb.GetComponent<Orb>().InsertNewMechanic();
        secondOrb.GetComponent<Orb>().InsertNewMechanic();
    }
}
