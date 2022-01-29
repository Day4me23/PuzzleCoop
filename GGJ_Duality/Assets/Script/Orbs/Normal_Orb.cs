using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Normal_Orb : Orb
{

    //meant for only triggers
    public override void InsertNewMechanic(PlayerMovement player) {

       // player.FastSpeed();

    }

    public override void RemoveThisMechanic(PlayerMovement player) {


        //player.NormalSpeed();
        //player.oppositePlayer.NormalSpeed();
    }

}
