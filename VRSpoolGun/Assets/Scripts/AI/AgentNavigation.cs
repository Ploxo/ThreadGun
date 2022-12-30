using UnityEngine;
using UnityEngine.AI;

public class AgentNavigation : MonoBehaviour
{
    public OVRInput.Button button;
    public OVRInput.Controller controller;

    public Transform target;
    public NavMeshAgent agent;
    public float distance = 0.1f;

    void Start()
    {
        agent.stoppingDistance = distance;
    }

    void Update()
    {
        if (OVRInput.GetDown(button, controller))
        {
            
        }

        if (target != null)
        {
            if (Vector3.Distance(transform.position, target.position) < distance)
            {
                target = null;

            }
        }
    }

    public void SetNewTarget(Vector3 position)
    {
        agent.SetDestination(target.position);
    }
}
