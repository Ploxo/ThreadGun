using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PatchType
{
    None,
    Bouncy,
    Slippery
}

public class Patch : MonoBehaviour
{
    public PatchType patchType;
    public int height;


    private void OnTriggerEnter(Collider other)
    {
        
    }
}
