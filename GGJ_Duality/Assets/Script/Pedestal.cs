using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pedestal : MonoBehaviour
{
    [SerializeField] GameObject orbPrefab;
    [SerializeField] Vector3 offset;
    GameObject reference;

    private void FixedUpdate()
    {
        if (reference == null)
            reference = Instantiate(orbPrefab, offset, Quaternion.identity);
    }
}
