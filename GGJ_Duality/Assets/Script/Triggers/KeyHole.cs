using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyHole : Interactable
{
    [SerializeField] private Transform origin; //holds the orb
    [Header("For specific orb requirements")]
    [SerializeField] private bool needsID;
    [SerializeField] private int requiredIdentification;
    private bool hasKey;
    public override void Interact(GameObject obj) {
        if (!hasKey) {
            //base.Interact(obj);
            if (needsID) {
                if (obj.GetComponent<Orb>().orb_id != requiredIdentification) {
                    //wrong orb
                    PedestalManager.instance.FindPedestal(obj.GetComponent<Orb>().orb_pedestalID, obj.GetComponent<Orb>().orb_id);
                    Destroy(obj);
                    return;
                }
            }
            obj.GetComponent<Collider>().enabled = false;
            obj.GetComponent<Rigidbody>().velocity = Vector3.zero;
            obj.transform.position = origin.position;
            active = true;
            hasKey = true;

            foreach (Triggerable source in sources) {
                if (source.CheckIfActive()) {
                    if(source.GetComponent<Door>() != null) {
                        source.GetComponent<Door>().GetComponent<BoxCollider>().enabled = true;
                    }
                }
            }  
        }
        else {
            PedestalManager.instance.FindPedestal(obj.GetComponent<Orb>().orb_pedestalID, obj.GetComponent<Orb>().orb_id);
            Destroy(obj);
            return;
        }
    }

}
