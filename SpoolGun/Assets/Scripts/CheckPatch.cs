using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPatch : MonoBehaviour
{
    [SerializeField]
    private LayerMask layerMask;

    public PatchType currentPatchType;


    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1f, layerMask))
        {
            Patch patch = hit.collider.gameObject.GetComponent<Patch>();
            if (patch)
            {
                currentPatchType = patch.patchType;
                Debug.Log("Set current patchtype");
            }
            else
            {
                currentPatchType = PatchType.None;
            }
        }

        if (currentPatchType == PatchType.None)
        {
            GetComponent<MeshRenderer>().material.color = Color.white;
        }
        else if (currentPatchType == PatchType.Bouncy)
        {
            GetComponent<MeshRenderer>().material.color = Color.green;
        }
        else if (currentPatchType == PatchType.Slippery)
        {
            GetComponent<MeshRenderer>().material.color = Color.cyan;
        }
    }
}
