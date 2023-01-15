using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class IceMovement : MonoBehaviour, IRefreshable
{
    [HideInInspector]
    public IceEffector data;

    NavMeshAgent agent;
    NavMeshTest movement;

    private float duration;

    private float initialSpeed;
    private float initialRotation;
    private float initialAcceleration;


    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        movement = GetComponent<NavMeshTest>();

        initialSpeed = movement.baseMoveSpeed;
        initialRotation = movement.baseAngularSpeed;
        initialAcceleration = movement.baseAcceleration;
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
        StopAllCoroutines();
        StartCoroutine(LerpValues());
    }

    private IEnumerator LerpValues()
    {
        float targetSpeed = initialSpeed * data.speedFactor;
        float targetRotation = initialRotation * data.rotationFactor;
        float targetAcceleration = initialAcceleration * data.accelerationFactor;

        agent.speed = targetSpeed;
        agent.angularSpeed = targetRotation;
        agent.acceleration = targetAcceleration;

        duration = data.duration;
        float ratio = 1f / duration;
        float time = 0f;
        while (time < duration)
        {
            agent.speed = Mathf.Lerp(targetSpeed, initialSpeed, time * ratio);
            agent.angularSpeed = Mathf.Lerp(targetRotation, initialRotation, time * ratio);
            agent.acceleration = Mathf.Lerp(targetAcceleration, initialAcceleration, time * ratio);

            time += Time.deltaTime;

            yield return null;
        }
    }
}