using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerPen : MonoBehaviour
{
    [SerializeField] private Transform origin;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float distance;
    [SerializeField] private Transform indicator;

    [SerializeField] private MeshRenderer lightRenderer;
    [SerializeField] private Color emissionColor;
    [SerializeField] private float emissionIntensity;

    [SerializeField] private ParticleSystem particles;

    Transform hitTarget;
    ResourceManager resourceManager;

    private void Start()
    {
        resourceManager = GameObject.FindGameObjectWithTag("Base").GetComponent<ResourceManager>();
    }

    public void Fire()
    {
        indicator.gameObject.SetActive(false);

        RaycastHit hit;
        if (Physics.Raycast(origin.position, origin.forward, out hit, distance, layerMask))
        {
            if (hit.transform.CompareTag("Resource"))
            {
                if (hitTarget == null || hitTarget != hit.transform)
                {
                    indicator.position = hit.transform.position;
                    indicator.parent = hit.transform;
                    indicator.Rotate(new Vector3(Random.Range(-30, 30), Random.Range(-30, 30), Random.Range(-30, 30)));

                    hitTarget = hit.transform;
                }
            }
            else
            {
                indicator.position = hit.point;
                indicator.parent = hit.transform;
                hitTarget = null;
            }

            indicator.gameObject.SetActive(true);
        }
        else
        {
            hitTarget = null;
            Hide();
        }

        if (hitTarget != null)
        {
            resourceManager.resourceTarget = hitTarget;
            particles.Play();
        }
        else
        {
            resourceManager.resourceTarget = null;
            particles.Stop();
        }
    }

    public void SetActiveMaterial(bool value)
    {
        if (value)
            lightRenderer.material.SetColor("_EmissionColor", emissionColor * emissionIntensity);
        else
            lightRenderer.material.SetColor("_EmissionColor", Color.black);
    }

    public void Hide()
    {
        if (hitTarget == null)
        {
            indicator.parent = transform;
            indicator.position = transform.position;
            indicator.rotation = Quaternion.identity;
            indicator.gameObject.SetActive(false);
        }
    }
}
