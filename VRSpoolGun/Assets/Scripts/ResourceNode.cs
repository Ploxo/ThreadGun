using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceNode : MonoBehaviour
{
    [SerializeField] private int capacity = 5;

    private int amount = 0;


    void Start()
    {
        ResetNode();
    }

    public void ResetNode()
    {
        amount = capacity;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Gatherer"))
        {
            // Give resource to collectors, if they have capacity
            amount = other.GetComponent<ResourceCollector>().AddResource(amount);

            if (amount == 0)
                gameObject.SetActive(false);
        }
    }
}
