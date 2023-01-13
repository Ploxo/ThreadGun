using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCNavMesh : MonoBehaviour
{
    [SerializeField]
    private float baseMoveSpeed = 10f;
    [SerializeField]
    private float baseRotationSpeed = 2f; // radians/s
    [SerializeField]
    private float baseAccelerationForce = 10f; // ForceMode.Force should be scaled to time
    [SerializeField]
    private float baseTorque = 2f; 

    public float moveSpeed;
    public float rotationSpeed;
    public float accelerationForce;
    public float torque;

    [SerializeField]
    private List<Transform> movePositionTransforms;
    private NavMeshAgent navMeshAgent;
    private Rigidbody rb;

    private float checkDistanceSq;

    private int currentIndex;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();

        checkDistanceSq = GetComponent<CapsuleCollider>().radius + 0.05f;
        checkDistanceSq *= checkDistanceSq;

        navMeshAgent.updatePosition = false;
        navMeshAgent.updateRotation = false;

        ResetMovement();
    }

    public void ResetMovement()
    {
        moveSpeed = baseMoveSpeed;
        rotationSpeed = baseRotationSpeed;
        accelerationForce = baseAccelerationForce;
        torque = baseTorque;
    }

    private void Update()
    {
        if (!navMeshAgent.enabled)
            return;

        navMeshAgent.SetDestination(movePositionTransforms[currentIndex].position);

        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance + 0.001f)
        {
            currentIndex = (currentIndex + 1) % movePositionTransforms.Count;
        }
    }

    private void FixedUpdate()
    {
        MoveAgent();
    }

    private void MoveAgent()
    {
        if (navMeshAgent.pathPending)
            return;

        // Aim toward the next path point for rotation
        Vector3 direction = (navMeshAgent.steeringTarget - transform.position);
        Vector3 directionNormalized = direction.normalized;

        // Raycast for immediate obstacles; since we control the agent, it can get into weird spots
        bool obstacle = false;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, checkDistanceSq))
        {
            obstacle = (hit.point - transform.position).sqrMagnitude < checkDistanceSq;
        }

        if (rb.velocity.magnitude < moveSpeed && !obstacle)
            rb.AddForce(transform.forward * accelerationForce, ForceMode.Force);

        if (rb.angularVelocity.magnitude < rotationSpeed)
        {
            Vector3 torqueVector = Vector3.Cross(transform.forward, direction);
            rb.AddTorque(Vector3.Project(torqueVector, Vector3.up).normalized * torque, ForceMode.Acceleration);
        }

        Debug.DrawLine(transform.position, transform.position + direction);

        navMeshAgent.nextPosition = transform.position;
    }

    // Set a new destination for the transform
    public void SetTargetTransform(int index, Vector3 newPosition)
    {
        movePositionTransforms[index].position = newPosition;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(transform.position, transform.position + transform.forward);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * checkDistanceSq);

        if (navMeshAgent != null && movePositionTransforms != null )
        {
            Gizmos.color = Color.yellow;
            foreach (var waypoint in movePositionTransforms)
            {
                Gizmos.DrawWireSphere(waypoint.position, navMeshAgent.stoppingDistance);
            }
        }
    }
}
