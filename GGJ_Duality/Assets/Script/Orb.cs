using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb : MonoBehaviour
{
    public string orb_name;
    public int orb_id; //for the list of orbs
    public int orb_pedestalID;

    public Pedestal myPedestal;
    public bool thrown; //if this orb was thrown
    public virtual void InsertNewMechanic(PlayerMovement player) {

    }

    public virtual void RemoveThisMechanic(PlayerMovement player) {

    }

    public void TurnOnCollider() {
        GetComponent<Collider>().enabled = true;
    }


    private void OnTriggerEnter(Collider other) {
        if (!other.CompareTag("Player")) {
            if(other.GetComponent<BoxCollider>() != null) {
                if (!other.GetComponent<BoxCollider>().isTrigger) {
                    DeleteOrb();
                }
            } else {
                DeleteOrb();
            }

            //if this hits anything but a player or trigger 
            
        } else {
            DeleteOrb();
        }
    }

    private void DeleteOrb() {
        if (thrown) {
            PedestalManager.instance.FindPedestal(orb_pedestalID, orb_id);
            //myPedestal.SpawnOrb(orb_id);
        }
        
        Destroy(this.gameObject);
    }
}
