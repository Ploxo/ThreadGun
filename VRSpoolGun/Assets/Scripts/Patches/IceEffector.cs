using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "NewIceEffector", menuName = "ScriptableObject/IceEffector")]
public class IceEffector : Effector
{
    // Agent values
    public float speedFactor = 5f;
    public float rotationFactor = 5f;
    public float accelerationFactor = 5f;
    public float torqueFactor = 5f;


    public override void ApplyEffect(GameObject go)
    {
        ////Debug.Log("Applied Ice");

        //// Check if one already exists
        //IceMovement iceMovement = go.GetComponent<IceMovement>();
        //if (iceMovement == null)
        //{
        //    iceMovement = go.AddComponent<IceMovement>();
        //    iceMovement.data = this;
        //}
        //else if (!iceMovement.enabled)
        //{
        //    iceMovement.enabled = true;
        //}

        //iceMovement.duration = duration;
    }
}





//////////////////// ORIG
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.AI;

//[CreateAssetMenu(fileName = "NewIceEffector", menuName = "ScriptableObject/IceEffector")]
//public class IceEffector : Effector
//{
//    public float duration = 1f;
//    public Vector3 torqueDirection = Vector3.up;
//    public float torque = 10f;
//    public float moveSpeed = 50f;
//    public float speedFactor = 5f;
//    public float rotationFactor = 50f;
//    public float accelerationFactor = 8f;


//    public override void ApplyEffect(GameObject go)
//    {
//        //Debug.Log("Applied Ice");

//        // Check if one already exists
//        IceMovement iceMovement = go.GetComponent<IceMovement>();
//        if (iceMovement == null)
//        {
//            iceMovement = go.AddComponent<IceMovement>();
//            iceMovement.data = this;
//        }
//        else if (!iceMovement.enabled)
//        {
//            iceMovement.enabled = true;
//        }

//        iceMovement.duration = duration;

//        // Old
//        //Rigidbody rb = go.GetComponent<Rigidbody>();

//        //if (rb != null)
//        //{
//        //    rb.AddTorque(torqueDirection * torque, ForceMode.Force);
//        //}
//    }

//    public override void RemoveEffect(GameObject go)
//    {
//        IceMovement effect = go.GetComponent<IceMovement>();

//        if (effect != null)
//            effect.enabled = false;
//    }
//}

//public class IceMovement : MonoBehaviour
//{
//    public IceEffector data;

//    NavMeshAgent agent;
//    NPCNavMesh movement;
//    Rigidbody rb;

//    public float duration;

//    public Vector3 destination;

//    public Vector3 direction;
//    public float speed;
//    public float maxSpeed;
//    public float acceleration;

//    public Vector3 torqueDirection;
//    public float torque;
//    public float maxRotation;

//    private float initialSpeed;
//    private float initialRotation;
//    private float initialAcceleration;


//    private void Awake()
//    {
//        agent = GetComponent<NavMeshAgent>();
//        movement = GetComponent<NPCNavMesh>();
//        rb = GetComponent<Rigidbody>();

//        initialSpeed = agent.speed;
//        initialRotation = agent.angularSpeed;
//        initialAcceleration = agent.acceleration;

//        maxSpeed = initialSpeed * data.speedFactor;
//        maxRotation = initialRotation * data.rotationFactor;
//        acceleration = initialAcceleration * data.accelerationFactor;
//    }

//    private void OnEnable()
//    {
//        //Vector3 initialVelocity = agent.velocity;
//        //Debug.Log("Initial: " + initialVelocity);
//        //direction = Vector3.ProjectOnPlane(initialVelocity, transform.up).normalized;

//        // We require the agent and the movement to be disabled so they don't overwrite movement
//        //movement.enabled = false;
//        //agent.enabled = false;
//        //rb.isKinematic = false;

//        //rb.velocity = initialVelocity;

//        agent.speed = maxSpeed;
//        agent.angularSpeed = maxRotation;
//        agent.acceleration = acceleration;

//        StartCoroutine(SpeedDecay());
//    }

//    private void OnDisable()
//    {
//        StopAllCoroutines();
//        Reset();

//        //Vector3 velocity = rb.velocity;

//        //rb.isKinematic = true;
//        //agent.enabled = true;
//        //movement.enabled = true;

//        //agent.velocity = rb.velocity;
//    }

//    private void Reset()
//    {
//        agent.speed = initialSpeed;
//        agent.angularSpeed = initialRotation;
//        agent.acceleration = initialAcceleration;
//    }

//    private IEnumerator SpeedDecay()
//    {
//        float ratio = 1f / duration;
//        float time = 0f;
//        while (time < duration)
//        {
//            agent.speed = Mathf.Lerp(maxSpeed, initialSpeed, time * ratio);
//            agent.angularSpeed = Mathf.Lerp(maxRotation, initialRotation, time * ratio);
//            agent.acceleration = Mathf.Lerp(acceleration, initialAcceleration, time * ratio);

//            time += Time.deltaTime;
//            Debug.Log("duration: " + time);

//            yield return null;
//        }

//        RemoveSelf();
//    }

//    private void FixedUpdate()
//    {
//        //Debug.Log($"Adding force {direction * speed}");

//        //if (rb.velocity.magnitude < maxSpeed)
//        //    rb.AddForce(direction * speed, ForceMode.Force);
//        //if (rb.angularVelocity.magnitude < maxRotation)
//        //    rb.AddTorque(torqueDirection * torque, ForceMode.Force);
//    }

//    private void RemoveSelf()
//    {
//        this.enabled = false;
//    }

//    private void OnDrawGizmosSelected()
//    {
//        Gizmos.DrawLine(transform.position, transform.position + direction);
//    }
//}

