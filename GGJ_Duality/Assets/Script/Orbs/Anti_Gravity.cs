using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anti_Gravity : Orb
{
    //Flip the gravity
    public override void InsertNewMechanic(PlayerMovement player) {

        player.ChangeGravity();

    }

    public override void RemoveThisMechanic(PlayerMovement player) {


        player.ChangeGravity();

    }




}
