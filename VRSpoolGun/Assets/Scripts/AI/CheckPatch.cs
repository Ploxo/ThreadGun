using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPatch : MonoBehaviour
{
    public ThreadType currentType = ThreadType.None;

    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float raycastRange = 1.1f;

    private ParticleSystem[] particles;


    private void Start()
    {
        particles = transform.GetComponentsInChildren<ParticleSystem>();

        StopParticles();
    }

    void FixedUpdate()
    {
        CheckCurrentPatch();
    }

    private void CheckCurrentPatch()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, raycastRange, layerMask))
        {
            Patch patch = hit.collider.gameObject.GetComponent<Patch>();
            if (patch != null)
            {
                if (patch.patchType != currentType)
                {
                    currentType = patch.patchType;

                    if (currentType == ThreadType.Ice)
                    {
                        PlayParticles(0);
                    }
                    else if (currentType == ThreadType.Gum)
                    {
                        PlayParticles(1);
                    }
                }

                patch.effect.ApplyEffect(gameObject);
            }
            else
            {
                currentType = ThreadType.None;
                StopParticles();
            }
        }
    }

    private void PlayParticles(int index)
    {
        for (int i = 0; i < particles.Length; i++)
        {
            particles[i].Stop();

            if (i == index)
                particles[i].Play(true);
        }
    }

    private void StopParticles()
    {
        for (int i = 0; i < particles.Length; i++)
            particles[i].Stop();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * raycastRange);
    }
}
