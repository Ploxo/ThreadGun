using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "NewGumEffector", menuName = "ScriptableObject/GumEffector")]
public class GumEffector : Effector
{
    public float force = 5f;
    public Vector3 forceDirection = Vector3.up;


    public override void ApplyEffect(GameObject go)
    {
        //Debug.Log("Applied BubbleGum");

        NavMeshAgent agent = go.GetComponent<NavMeshAgent>();
        if (agent != null) 
        {
            Debug.Log("ran velocity");
            agent.velocity = new Vector3(agent.velocity.x, 50f, agent.velocity.z);
        }

        //Rigidbody rb = go.GetComponent<Rigidbody>();

        //if (rb != null)
        //{
        //    rb.AddForce(forceDirection * force, ForceMode.Impulse);
        //}
    }
}
