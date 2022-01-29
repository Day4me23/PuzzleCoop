using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public bool CheckGround(Vector3 position, float radius, LayerMask playerLayer) {
        
        //RaycastHit hit;
        
       // return Physics.SphereCast(position, radius, -transform.up, out hit, 0f, ~playerLayer);

        return Physics.CheckSphere(position, radius, ~playerLayer, QueryTriggerInteraction.Ignore);
    }

    private void OnDrawGizmosSelected() {
        Gizmos.DrawSphere(this.transform.position, 0.2f);
    }
}
