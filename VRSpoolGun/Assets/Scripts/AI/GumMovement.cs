using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GumMovement : MonoBehaviour, IRefreshable
{
    public GumEffector data;

    Rigidbody rb;
    NPCNavMesh movement;

    public LayerMask layers;

    private float duration;
    private bool isGrounded = true;


    private void Awake()
    {
        //data = (GumEffector)ThreadManager.Instance.GetThread(ThreadType.Gum).effector;

        rb = GetComponent<Rigidbody>();
        movement = GetComponent<NPCNavMesh>();

        layers = data.layers;
    }

    private void OnEnable()
    {
        movement.ResetMovement();
        movement.moveSpeed *= data.speedFactor;
        movement.rotationSpeed *= data.rotationFactor;
        movement.accelerationForce *= data.accelerationFactor;
        movement.torque *= data.torqueFactor;
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
            isGrounded = true;

            rb.AddForce(Vector3.up * data.force, ForceMode.Impulse);
        }

        isGrounded = false;
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
