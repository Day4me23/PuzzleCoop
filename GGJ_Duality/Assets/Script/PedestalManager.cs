using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedestalManager : MonoBehaviour
{

    public static PedestalManager instance;
    public List<Pedestal> pedestals = new List<Pedestal>();
    private void Awake() {
        if(instance == null) {
            instance = this;
        } else {
            Destroy(this.gameObject);
        }
    }

    public void FindPedestal(int pedID, int orbID) {
        for(int i = 0; i < pedestals.Count; i++) {
            if(pedestals[i].pedistalID == pedID) {
                pedestals[i].SpawnOrb(pedID, orbID);
            }
        }
    }

}
