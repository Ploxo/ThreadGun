using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceCollector : MonoBehaviour
{
    public int carried = 0;

    [SerializeField] private int capacity = 5;


    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public int AddResource(int value)
    {
        int available = capacity - carried;
        int remaining = Mathf.Max(0, value - available);

        carried += Mathf.Max(available, value);

        return remaining;
    }
}
