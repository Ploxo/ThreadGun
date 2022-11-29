using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleGumEffector : IPatchEffector
{
    public PhysicMaterial bubbleGumMaterial;
    public float force = 5f;
    public Vector3 forceDirection = Vector3.up;


    public void ApplyEffect(GameObject go)
    {
        //Debug.Log("Applied BubbleGum");

        Rigidbody rb = go.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.AddForce(forceDirection * force, ForceMode.Impulse);
        }
    }
}
