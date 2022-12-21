using UnityEngine;

public class Raycaster : MonoBehaviour
{
    [SerializeField] private Transform origin;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Transform indicator;
    [SerializeField] private float distance;

    public OVRInput.Button button;
    public bool held = false;


    void FixedUpdate()
    {
        if (indicator == null)
            return;

        //if (held)
        //{
            if (OVRInput.Get(button))
            {
                RaycastHit hit;
                if (Physics.Raycast(origin.position, origin.forward, out hit, distance, layerMask))
                {
                    indicator.position = hit.point;
                }
            }
            else
            {
                indicator.position = transform.position;
            }
        //}

    }
}
