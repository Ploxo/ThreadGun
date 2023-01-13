using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class IceMovement : MonoBehaviour, IRefreshable
{
    public IceEffector data;

    Rigidbody rb;

    NPCNavMesh movement;

    private float duration;

    private float initialSpeed;
    private float initialRotation;
    private float initialAcceleration;
    private float initialTorque;


    private void Awake()
    {
        //data = (IceEffector)ThreadManager.Instance.GetThread(ThreadType.Ice).effector;

        rb = GetComponent<Rigidbody>();

        movement = GetComponent<NPCNavMesh>();

        initialSpeed = movement.moveSpeed;
        initialRotation = movement.rotationSpeed;
        initialAcceleration = movement.accelerationForce;
        initialTorque = movement.torque;
    }

    private void OnEnable()
    {


        StartCoroutine(LerpValues());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
        movement.ResetMovement();
    }

    public void Refresh()
    {
        duration = data.duration;
        StopAllCoroutines();
        StartCoroutine(LerpValues());
    }

    private IEnumerator LerpValues()
    {
        float targetSpeed = initialSpeed * data.speedFactor;
        float targetRotation = initialRotation * data.rotationFactor;
        float targetAcceleration = initialAcceleration * data.accelerationFactor;
        float targetTorque = initialTorque * data.torqueFactor;

        movement.moveSpeed = targetSpeed;
        movement.rotationSpeed = targetRotation;
        movement.accelerationForce = targetAcceleration;
        movement.torque = targetTorque;

        float ratio = 1f / duration;
        float time = 0f;
        while (time < duration)
        {
            movement.moveSpeed = Mathf.Lerp(targetSpeed, initialSpeed, time * ratio);
            movement.rotationSpeed = Mathf.Lerp(targetRotation, initialRotation, time * ratio);
            movement.accelerationForce = Mathf.Lerp(targetAcceleration, initialAcceleration, time * ratio);
            movement.torque = Mathf.Lerp(targetTorque, initialTorque, time * ratio);

            time += Time.deltaTime;

            yield return null;
        }
    }
}