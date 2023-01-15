using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceCollector : MonoBehaviour
{
    public delegate void GatherDelegate();
    public event GatherDelegate onGatherComplete;

    public int carried = 0;
    public float gatherTime = 3f;
    public float dropoffTime = 5f;
    public Transform target;

    public ResourceManager homeBase;

    [SerializeField] private int capacity = 5;

    private NavMeshTest movement;


    private void Awake()
    {
        homeBase = GameObject.FindGameObjectWithTag("Base").GetComponent<ResourceManager>();
    }

    private void Start()
    {
        movement = GetComponent<NavMeshTest>();

        if (homeBase.GetRandomNode(out target))
            SetTarget(target);
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
        movement.SetTargetTransform(newTarget);
    }

    public void AddResource(int value)
    {
        carried += value;
    }

    private IEnumerator Gather()
    {
        Debug.Log("Start gather");

        float time = 0f;
        while (time < gatherTime)
        {
            time += Time.deltaTime;

            yield return null;
        }

        AddResource(target.GetComponent<ResourceNode>().ProvideResource(capacity - carried));
        onGatherComplete?.Invoke();

        SetTarget(homeBase.transform);
    }

    private IEnumerator Dropoff()
    {
        float time = 0f;
        while (time < dropoffTime)
        {
            time += Time.deltaTime;

            yield return null;
        }

        homeBase.AddResource(carried);
        carried = 0;

        if (homeBase.GetRandomNode(out target))
            SetTarget(target);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == target && other.CompareTag("Resource"))
        {

            ResourceNode node = other.GetComponent<ResourceNode>();
            if (node.Amount > 0 && node.AddGatherer(gameObject))
            {
                Debug.Log("Found node");
                StartCoroutine(Gather());
            }
        }
        else if (carried > 0 && other.transform == homeBase.transform && other.CompareTag("Base"))
        {
            StartCoroutine(Dropoff());
        }

        if (movement.DestinationReached())
        {


        }
    }
}
