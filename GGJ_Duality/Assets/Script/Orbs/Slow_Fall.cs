using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slow_Fall : Orb
{

    //Player should fall slower
    public override void InsertNewMechanic(PlayerMovement player) {

        player.DecreaseFallingSpeed();

    }

    public override void RemoveThisMechanic(PlayerMovement player) {


        player.IncreaseFallingSpeed();

    }

}
