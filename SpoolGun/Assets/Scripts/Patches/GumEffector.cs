using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewGumEffector", menuName = "ScriptableObject/GumEffector")]
public class GumEffector : Effector
{
    public float force = 5f;
    public Vector3 forceDirection = Vector3.up;


    public override void ApplyEffect(GameObject go)
    {
        //Debug.Log("Applied BubbleGum");

        Rigidbody rb = go.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.AddForce(forceDirection * force, ForceMode.Impulse);
        }
    }
}
