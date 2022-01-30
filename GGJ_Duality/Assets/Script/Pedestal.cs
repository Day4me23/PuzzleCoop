using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pedestal : MonoBehaviour
{
    [SerializeField] Transform offset;
    public int pedistalID;
    public void SpawnOrb(int pedID, int orbID) {

        GameObject orb = Instantiate(OrbList.instance.orbMechanics[orbID].gameObject, offset.position, Quaternion.identity);
        orb.GetComponent<Orb>().TurnOnCollider();
        orb.GetComponent<Orb>().orb_pedestalID = pedID;
    }

}
