using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Super_Speed : Orb
{

    //Player should fall slower
    public override void InsertNewMechanic(PlayerMovement player) {

        player.FastSpeed();

    }

    public override void RemoveThisMechanic(PlayerMovement player) {


        player.NormalSpeed();

    }

}
