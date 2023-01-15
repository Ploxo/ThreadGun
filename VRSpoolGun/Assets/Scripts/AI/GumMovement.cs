using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GumMovement : MonoBehaviour, IRefreshable
{
    public GumEffector data;

    Rigidbody rb;
    NavMeshAgent agent;
    NavMeshTest movement;

    public LayerMask layers;

    private float duration;


    private void Awake()
    {
        //data = (GumEffector)ThreadManager.Instance.GetThread(ThreadType.Gum).effector;

        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        movement = GetComponent<NavMeshTest>();

        layers = data.layers;
    }

    private void OnEnable()
    {
        //movement.ResetMovement();

        //movement.moveSpeed *= data.speedFactor;
        //movement.rotationSpeed *= data.rotationFactor;
        //movement.accelerationForce *= data.accelerationFactor;
        //movement.torque *= data.torqueFactor;

        agent.speed *= data.speedFactor;
        agent.angularSpeed *= data.rotationFactor;
        agent.acceleration *= data.accelerationFactor;
    }

    private void OnDisable()
    {
        movement.ResetMovement();
    }

    private void FixedUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 0.35f, layers))
        {

            rb.AddForce(Vector3.up * data.force, ForceMode.Impulse);
        }
    }

    public void Refresh()
    {
        duration = data.duration;
    }

    private void OnDrawGizmosSelected()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 0.35f, layers))
        {
            Gizmos.DrawLine(transform.position, hit.point);
        }
    }
}
