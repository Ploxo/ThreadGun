using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshTest : MonoBehaviour
{
    public bool isControlledByAgent = true;

    public LayerMask groundLayers;
    public LayerMask collisionLayers;
    public float torque = 60f;

    private NavMeshAgent navMeshAgent;
    private Rigidbody rb;

    public float baseMoveSpeed = 10f;
    public float baseAngularSpeed = 2f; // radians/s
    public float baseAcceleration = 10f; // ForceMode.Force should be scaled to time
    public float baseTorque = 60f;

    [SerializeField]
    private List<Transform> movePositionTransforms;
    public bool loopWaypoints = true;
    //private int currentIndex = 0;

    public Transform target;

    public float checkDistance;
    bool grounded = true;
    float checkDistanceSq;


    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();

        checkDistanceSq = GetComponent<CapsuleCollider>().radius + 0.05f;
        checkDistanceSq *= checkDistanceSq;
    }

    public bool DestinationReached()
    {
        if (!navMeshAgent.pathPending)
        {
            //Debug.Log(navMeshAgent.stoppingDistance + ", " + navMeshAgent.remainingDistance);
            if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                if (
                    rb.velocity.sqrMagnitude < 0.001f &&
                    navMeshAgent.velocity.sqrMagnitude <= 0.001f)
                {
                    return true;
                }
            }
        }

        return false;

        //return !navMeshAgent.pathPending && navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance + 0.001f;
    }

    private void Update()
    {
        if (!navMeshAgent.enabled)
            return;

        if (isControlledByAgent && navMeshAgent.isStopped)
            SetAgentMovementActive(true);
        else if ((!isControlledByAgent || !grounded) && !navMeshAgent.isStopped)
            SetAgentMovementActive(false);

        //navMeshAgent.SetDestination(movePositionTransforms[currentIndex].position);

        //if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance + 0.001f)
        //{
        //    if (loopWaypoints)
        //        currentIndex = (currentIndex + 1) % movePositionTransforms.Count;
        //    else
        //        currentIndex = Mathf.Min((currentIndex + 1), movePositionTransforms.Count - 1);
        //}

        if (target != null)
            navMeshAgent.SetDestination(target.position);
    }

    public void SetAgentActive(bool value)
    {
        isControlledByAgent = value;
    }

    private void SetAgentMovementActive(bool value)
    {
        if (value == true && !grounded)
            return;

        navMeshAgent.updatePosition = value;
        navMeshAgent.updateRotation = value;
        navMeshAgent.isStopped = !value;

        rb.isKinematic = value;
        rb.useGravity = !value;

        if (value == false)
        {
            rb.velocity = navMeshAgent.velocity;
        }
        else
        {
            navMeshAgent.nextPosition = transform.position;
            navMeshAgent.velocity = rb.velocity;
        }
    }

    private void FixedUpdate()
    {
        GroundCheck();

        if (!isControlledByAgent || !grounded)
            PhysicsMovement();
    }

    // Set a new destination for the transform
    public void SetTargetTransform(Transform newTarget)
    {
        //movePositionTransforms[index].position = newPosition;

        target = newTarget;
    }

    public void SetTargetPosition(Vector3 newPosition)
    {
        target.position = newPosition;
    }

    public void ResetMovement()
    {
        navMeshAgent.speed = baseMoveSpeed;
        navMeshAgent.angularSpeed = baseAngularSpeed;
        navMeshAgent.acceleration = baseAcceleration;
    }

    private void PhysicsMovement()
    {
        if (isControlledByAgent && navMeshAgent.pathPending)
            return;

        // Aim toward the next path point for rotation
        Vector3 direction = (navMeshAgent.steeringTarget - transform.position);
        Vector3 directionNormalized = direction.normalized;

        //Raycast for immediate obstacles; since we control the agent, it can get into weird spots
       bool obstacle = false;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, checkDistanceSq, collisionLayers))
        {
            obstacle = (hit.point - transform.position).sqrMagnitude < checkDistanceSq;
        }

        if (rb.velocity.magnitude < navMeshAgent.speed && !obstacle)
            rb.AddForce(transform.forward * navMeshAgent.acceleration, ForceMode.Force);

        if (rb.angularVelocity.magnitude < navMeshAgent.angularSpeed * Mathf.Deg2Rad)
        {
            Vector3 torqueVector = Vector3.Cross(transform.forward, direction);
            rb.AddTorque(Vector3.Project(torqueVector, Vector3.up).normalized * torque, ForceMode.Acceleration);
        }

        navMeshAgent.updatePosition = false;
        navMeshAgent.nextPosition = transform.position;
    }

    private void GroundCheck()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, checkDistance, groundLayers))
        {
            if (rb.velocity.y < 0f && !grounded)
            {
                grounded = true;
            }
        }
        else if (grounded)
        {
            grounded = false; // No longer on the ground since raycast missed
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * checkDistance);

        if (navMeshAgent != null)
        {
            Gizmos.DrawWireSphere(transform.position, navMeshAgent.stoppingDistance);
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, navMeshAgent.velocity);
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(transform.position, navMeshAgent.desiredVelocity);

            Gizmos.DrawWireSphere(navMeshAgent.nextPosition, 0.5f);
        }


    }
}
