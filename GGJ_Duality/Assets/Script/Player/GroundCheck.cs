using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    
    public bool CheckGround(Vector3 position, float radius, LayerMask playerLayer) {
        RaycastHit hit;
        return Physics.SphereCast(position, radius, -transform.up, out hit, radius, ~playerLayer);

        //return Physics.CheckSphere(position, radius, groundLayer);
    }

}
