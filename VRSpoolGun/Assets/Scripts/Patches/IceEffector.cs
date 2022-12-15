using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewIceEffector", menuName = "ScriptableObject/IceEffector")]
public class IceEffector : Effector
{
    public float torque = 10f;
    public Vector3 torqueDirection = Vector3.up;


    public override void ApplyEffect(GameObject go)
    {
        //Debug.Log("Applied Ice");

        Rigidbody rb = go.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.AddTorque(torqueDirection * torque, ForceMode.Force);
        }
    }
}
