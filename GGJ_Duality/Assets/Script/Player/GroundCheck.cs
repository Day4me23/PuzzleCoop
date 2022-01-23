using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    
    public bool CheckGround(Vector3 position, float radius, LayerMask groundLayer) {
        return Physics.CheckSphere(position, radius, groundLayer);
    }

}