using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "NewGumEffector", menuName = "ScriptableObject/GumEffector")]
public class GumEffector : Effector
{
    public float force = 5f;
    public LayerMask layers;

    // Agent values
    public float speedFactor = 5f;
    public float rotationFactor = 50f;
    public float accelerationFactor = 8f;
    public float torqueFactor = 1f;


    public override void ApplyEffect(GameObject go)
    {
        //// Check if one already exists
        //GumMovement gumMovement = go.GetComponent<GumMovement>();
        //if (gumMovement == null)
        //{
        //    gumMovement = go.AddComponent<GumMovement>();
        //    gumMovement.data = this;
        //}
        //else if (!gumMovement.enabled)
        //{
        //    gumMovement.enabled = true;
        //}

        //gumMovement.duration = duration;
    }
}
