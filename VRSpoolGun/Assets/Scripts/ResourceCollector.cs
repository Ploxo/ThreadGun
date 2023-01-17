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

    //public Transform homeBaseTransform;
    public ResourceManager homeBase;

    [SerializeField] private int capacity = 5;

    private NavMeshTest movement;

    bool gathering = false;
    bool droppingOff = false;


    private void Start()
    {
        homeBase = GameObject.Find("BaseObject").GetComponent<ResourceManager>();
        //if (homeBaseTransform == null)
        //    Debug.LogError("Base transform is null in start");

        //homeBase = homeBaseTransform.GetComponent<ResourceManager>();
        //if (homeBase == null)
        //    Debug.LogError("Base is null in start");

        movement = GetComponent<NavMeshTest>();
    }

    private void Update()
    {
        if (target == null)
        {
            if (carried == capacity)
            {
                SetTarget(homeBase.transform);
            }
            else
            {
                if (homeBase.resourceTarget != null)
                    SetTarget(homeBase.resourceTarget);
                else
                    SetTarget(homeBase.transform);
            }
        }
        else if (carried < capacity && homeBase.resourceTarget != null)
        {
            SetTarget(homeBase.resourceTarget);
        }
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

    public void StopGathering()
    {
        StopAllCoroutines();
        gathering = false;
        //target.GetComponent<ResourceNode>().RemoveGatherer(this);

        //movement.SetAgentActive(true);
        target = null;
    }

    private IEnumerator Gather()
    {
        Debug.Log("Start gather");
        //movement.SetAgentActive(false);

        gathering = true;

        float time = 0f;
        while (time < gatherTime)
        {
            time += Time.deltaTime;

            yield return null;
        }

        AddResource(target.GetComponent<ResourceNode>().ProvideResource(capacity - carried));
        target.GetComponent<ResourceNode>().RemoveGatherer(this);
        //onGatherComplete?.Invoke();

        //movement.SetAgentActive(true);
        target = null;

        gathering = false;
    }

    private IEnumerator Dropoff()
    {
        Debug.Log("Started dropoff");
        //movement.SetAgentActive(false);

        droppingOff = true;

        float time = 0f;
        while (time < dropoffTime)
        {
            time += Time.deltaTime;

            yield return null;
        }

        homeBase.AddResource(carried);
        carried = 0;

        //movement.SetAgentActive(true);
        target = null;

        droppingOff = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform == target && other.CompareTag("Resource"))
        {
            ResourceNode node = other.GetComponent<ResourceNode>();
            if (!gathering && node.Amount > 0 && node.AddGatherer(this))
            {
                Debug.Log("Found node");
                StartCoroutine(Gather());
            }
        }
        else if (!droppingOff && carried > 0 && other.transform.parent == homeBase.transform && other.CompareTag("Base"))
        {
            Debug.Log("Found base");
            StartCoroutine(Dropoff());
        }

        //if (movement.DestinationReached())
        //{
        //    Debug.Log("reached destination");

        //}
    }
}
