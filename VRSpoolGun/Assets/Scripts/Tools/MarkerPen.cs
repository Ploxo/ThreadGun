using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerPen : MonoBehaviour
{
    [SerializeField] private Transform origin;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float distance;
    [SerializeField] private Transform indicator;

    public OVRInput.Button button;


    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (indicator == null)
            return;

        if (OVRInput.Get(button))
        {
            RaycastHit hit;
            if (Physics.Raycast(origin.position, origin.forward, out hit, distance, layerMask))
            {
                indicator.position = hit.transform.position;
            }
        }
        else
        {
            indicator.position = transform.position;
        }
    }
}
