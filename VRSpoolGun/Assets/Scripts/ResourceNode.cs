using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceNode : MonoBehaviour
{
    private ResourceManager resourceManager;

    public List<ResourceCollector> gatherers;
    public int maxGatherers = 1;
    public int capacity = 5;

    private int amount = 0;

    public int Amount
    {
        get { return amount; }
    }


    void Start()
    {
        resourceManager = GameObject.FindGameObjectWithTag("Base").GetComponent<ResourceManager>();
        ResetNode();
    }

    public void ResetNode()
    {
        amount = capacity;
    }

    public bool AddGatherer(ResourceCollector gatherer)
    {
        if (gatherers.Contains(gatherer)) 
            return false;

        if (gatherers.Count < maxGatherers) 
            gatherers.Add(gatherer);

        return true;
    }

    public void RemoveGatherer(ResourceCollector gatherer)
    {
        if (!gatherers.Contains(gatherer))
            return;

        gatherers.Remove(gatherer);
    }

    public int ProvideResource(int maxValue)
    {
        int returnValue;
        if (maxValue <= amount)
            returnValue = maxValue;
        else
            returnValue = amount;

        amount = amount - returnValue;

        if (amount == 0)
        {
            resourceManager.resourceTarget = null;

            for (int i = 0; i < gatherers.Count; i++)
            {
                gatherers[i].StopGathering();
            }

            gatherers.Clear();
            gameObject.SetActive(false);
        }

        return returnValue;
    }
}
