using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbHolderManager : MonoBehaviour
{
    private int firstOrb, secondOrb;
    [SerializeField] private string firstOrbName, secondOrbName;
    public bool AddOrb(Orb orb) {
        if(firstOrb == -1) {
            firstOrb = orb.orb_id;
            firstOrbName = orb.orb_name;
            ChangeMechanics();
            return true;
        } else if (secondOrb == -1) {
            secondOrb = orb.orb_id;
            secondOrbName = orb.orb_name;
            ChangeMechanics();
            return true;
        }

        return false;
        
    }

    public void RemoveOrb() {
        //throwing
    }

    public int GetFirstOrb() {
        return firstOrb;
    }

    public int GetSecondOrb() {
        return secondOrb;
    }

    private void ChangeMechanics() {
        if(firstOrb != -1) {
            OrbList.instance.orbMechanics[firstOrb].GetComponent<Orb>().InsertNewMechanic(gameObject.GetComponent<PlayerMovement>());
        }
            //firstOrb.GetComponent<Orb>().InsertNewMechanic(gameObject.GetComponent<PlayerMovement>());
        if(secondOrb != -1) {
            OrbList.instance.orbMechanics[secondOrb].GetComponent<Orb>().InsertNewMechanic(gameObject.GetComponent<PlayerMovement>());
        }
           // secondOrb.GetComponent<Orb>().InsertNewMechanic(gameObject.GetComponent<PlayerMovement>());
    }


    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Orb")) {
            if (AddOrb(other.GetComponent<Orb>())){ //if you can add the orb
                //you added the orb
                Destroy(other.gameObject);
            }
        }
    }

}
