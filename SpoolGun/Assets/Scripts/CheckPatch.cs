using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPatch : MonoBehaviour
{
    [SerializeField]
    private LayerMask layerMask;
    [SerializeField]
    private float raycastRange = 1.1f;

    private ParticleSystem[] particles;


    private void Start()
    {
        particles = transform.GetComponentsInChildren<ParticleSystem>();
    }

    void FixedUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, raycastRange, layerMask))
        {
            Patch patch = hit.collider.gameObject.GetComponent<Patch>();
            if (patch != null)
            {
                patch.effect.ApplyEffect(gameObject);

                if (patch.patchType == PatchType.Slippery)
                {
                    Debug.Log("RAN ICE");
                    particles[0].gameObject.SetActive(true);
                    particles[1].gameObject.SetActive(false);
                }
                else if (patch.patchType == PatchType.Bouncy)
                {
                    particles[1].gameObject.SetActive(true);
                    particles[0].gameObject.SetActive(false);
                }
            }
            else
            {
                for (int i = 0; i < particles.Length; i++)
                    particles[i].gameObject.SetActive(false);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * raycastRange);
    }
}
