using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour 
{
    [Header("Trigger Variables")]
    public List<Triggerable> sources = new List<Triggerable>();
    public bool active;
}