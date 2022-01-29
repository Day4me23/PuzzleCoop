using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb : MonoBehaviour
{
    public string orb_name;
    public int orb_id; //for the list of orbs
    public virtual void InsertNewMechanic(PlayerMovement player) {

    }

    public virtual void RemoveThisMechanic(PlayerMovement player) {

    }

    private void OnTriggerEnter(Collider other) {
        if (!other.CompareTag("Player")) {
            if(other.GetComponent<BoxCollider>() != null) {
                if (!other.GetComponent<BoxCollider>().isTrigger) {
                    Destroy(this.gameObject);
                }
            } else {
                Destroy(this.gameObject);
            }
            //if this hits anything but a player or trigger 
            
        } else {
            Destroy(this.gameObject);
        }
    }
}
