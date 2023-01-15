using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceNode : MonoBehaviour
{
    public List<GameObject> gatherers;
    public int maxGatherers = 1;
    public int capacity = 5;

    private int amount = 0;

    public int Amount
    {
        get { return amount; }
    }


    void Start()
    {
        ResetNode();
    }

    public void ResetNode()
    {
        amount = capacity;
    }

    public bool AddGatherer(GameObject gatherer)
    {
        if (gatherers.Contains(gatherer)) 
            return false;

        if (gatherers.Count < maxGatherers) 
            gatherers.Add(gatherer);

        return true;
    }

    public void RemoveGatherer(GameObject gatherer)
    {
        if (!gatherers.Contains(gatherer))
            return;

        gatherers.Remove(gatherer);
    }

    public int ProvideResource(int maxValue)
    {
        int value = maxValue - (maxValue - amount);

        amount = Mathf.Max(0, amount - value);

        if (amount == 0)
            gameObject.SetActive(false);

        return value;
    }
}
