using UnityEngine;

public class Raycaster : MonoBehaviour
{
    [SerializeField] private Transform origin;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Transform indicator;
    [SerializeField] private float distance;


    void FixedUpdate()
    {
        if (indicator != null)
        {
            RaycastHit hit;
            if (Physics.Raycast(origin.position, origin.forward, out hit, distance, layerMask))
            {
                indicator.position = hit.point;
            }
        }
    }
}
