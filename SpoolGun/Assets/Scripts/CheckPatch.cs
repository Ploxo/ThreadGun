using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPatch : MonoBehaviour
{
    [SerializeField]
    private LayerMask layerMask;
    [SerializeField]
    private float raycastRange = 1.1f;

    //public PatchType currentPatchType;
    public IPatchEffector currentEffector;


    void FixedUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, raycastRange, layerMask))
        {
            Patch patch = hit.collider.gameObject.GetComponent<Patch>();
            if (patch != null)
            {
                patch.effect.ApplyEffect(this.gameObject);
            }
        }

        //if (currentPatchType == PatchType.None)
        //{
        //    GetComponent<MeshRenderer>().material.color = Color.white;
        //}
        //else if (currentPatchType == PatchType.Bouncy)
        //{
        //    GetComponent<MeshRenderer>().material.color = Color.green;
        //}
        //else if (currentPatchType == PatchType.Slippery)
        //{
        //    GetComponent<MeshRenderer>().material.color = Color.cyan;
        //}
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * raycastRange);
    }
}
