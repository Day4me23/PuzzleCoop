using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Super_Speed : Orb
{

    //Player should fall slower
    public override void InsertNewMechanic(PlayerMovement player) {

        player.IncreaseSpeed();

    }

    public override void RemoveThisMechanic(PlayerMovement player) {


        player.DecreaseSpeed();

    }

}
