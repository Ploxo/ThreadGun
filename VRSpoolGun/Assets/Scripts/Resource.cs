using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    public int amount = 5;


    void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Gatherer"))
        {
            other.GetComponent<ResourceCollector>().AddResource(amount);

            Destroy(gameObject);
        }
    }
}
