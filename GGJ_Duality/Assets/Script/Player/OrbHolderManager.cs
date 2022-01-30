using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class OrbHolderManager : MonoBehaviour
{
    //private int firstOrb = -1, secondOrb = -1;
    public int firstOrb = -1;
    public int secondOrb = -1;

    public string firstOrbName, secondOrbName;

    [SerializeField] private int firstOrbPedestal, secondOrbPedestal;

    [SerializeField] private Transform throwOrigin;
    private float throwForce = 5f;
    public bool AddOrb(Orb orb) {
        if(firstOrb == -1) {
            firstOrb = orb.orb_id;
            firstOrbName = orb.orb_name;
            firstOrbPedestal = orb.orb_pedestalID;
            ChangeMechanics();
            return true;
        } else if (secondOrb == -1) {
            secondOrb = orb.orb_id;
            secondOrbName = orb.orb_name;
            secondOrbPedestal = orb.orb_pedestalID;
            ChangeMechanics();
            return true;
        }

        return false;
    }

    public void ThrowLeftContext(InputAction.CallbackContext context) {
        if (context.performed) {
            ThrowLeft();
        }
    }

    private void ThrowLeft() {
        //holds the first orb with left click
        if(firstOrb != -1) {
            //you have an orb in the left hand

            GameObject orb = Instantiate(OrbList.instance.orbMechanics[firstOrb].gameObject, throwOrigin.position, Quaternion.identity);
            
            orb.GetComponent<Orb>().TurnOnCollider();
            orb.GetComponent<Orb>().orb_pedestalID = firstOrbPedestal;
            orb.GetComponent<Orb>().thrown = true;
            orb.GetComponent<Rigidbody>().AddForce(throwOrigin.forward * throwForce, ForceMode.Impulse);

            OrbList.instance.orbMechanics[firstOrb].GetComponent<Orb>().RemoveThisMechanic(gameObject.GetComponent<PlayerMovement>());
            firstOrb = -1;
            
            firstOrbName = "";
            ChangeMechanics();
        } else {
            return;
        }
    }

    public void ThrowRightContext(InputAction.CallbackContext context) {
        if (context.performed) {
            ThrowRight();
        }
    }

    private void ThrowRight() {
        //holds the second orb with right click
        if (secondOrb != -1) {
            //you have an orb in the right hand

            GameObject orb = Instantiate(OrbList.instance.orbMechanics[secondOrb].gameObject, throwOrigin.position, Quaternion.identity);
            orb.GetComponent<Orb>().TurnOnCollider();
            orb.GetComponent<Orb>().orb_pedestalID = secondOrbPedestal;
            orb.GetComponent<Orb>().thrown = true;
            orb.GetComponent<Rigidbody>().AddForce(throwOrigin.forward * throwForce, ForceMode.Impulse);

            OrbList.instance.orbMechanics[secondOrb].GetComponent<Orb>().RemoveThisMechanic(gameObject.GetComponent<PlayerMovement>());
            secondOrb = -1;
            secondOrbName = "";
            ChangeMechanics();
        }
        else {
            return;
        }
    }
    private void ChangeMechanics() {
        if(firstOrb != -1) {
            OrbList.instance.orbMechanics[firstOrb].GetComponent<Orb>().InsertNewMechanic(gameObject.GetComponent<PlayerMovement>());
        }
        if(secondOrb != -1) {
            OrbList.instance.orbMechanics[secondOrb].GetComponent<Orb>().InsertNewMechanic(gameObject.GetComponent<PlayerMovement>());
        }

        GameManager.instance.UpdateUIOrbs();

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
