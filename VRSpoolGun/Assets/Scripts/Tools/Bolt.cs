using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;

public class Bolt : MonoBehaviour
{
    [SerializeField] private SpoolGun gun;

    [SerializeField] private float minPointZ;
    [SerializeField] private float maxPointZ;
    [SerializeField] private OneGrabTranslateTransformer translator;
    [SerializeField] private float indicatorRadius = 0.01f;

    [SerializeField, Range(0f, 1f)] private float activationRange = 0.25f; // percentage of area from min that activates on enter
    [SerializeField] private float springBackSpeed = 0.1f;

    public bool active = false;

    private float activationLimit;


    // Make sure we update values when editing inspector
    private void OnValidate()
    {
        translator.Constraints.MinZ.Value = minPointZ;
        translator.Constraints.MaxZ.Value = maxPointZ;

        activationLimit = minPointZ + (Mathf.Abs(minPointZ - maxPointZ) * activationRange);
    }

    void Start()
    {
        OnValidate();

        gun = GetComponentInParent<SpoolGun>();
    }

    private void Update()
    {
        // If it's in the activation zone and not alread yactive, start the springback
        if (!active && transform.localPosition.z < activationLimit)
        {
            Debug.Log("ACTIVATE BOLT");
            active = true;
            StartCoroutine(BoltSpringbackCoroutine());
        }
    }

    // Shoots back the bolt to the original position over time
    private IEnumerator BoltSpringbackCoroutine()
    {
        Vector3 target = transform.localPosition;
        target.z = maxPointZ;
        while (transform.localPosition.z < maxPointZ)
        {
            Vector3 localPosition = transform.localPosition;
            localPosition.z = Mathf.MoveTowards(localPosition.z, maxPointZ, springBackSpeed * Time.deltaTime);
            transform.localPosition = localPosition;

            yield return null;
        }

        active = false;
        gun.Reload();
        Debug.Log("DEACTIVATE BOLT");

        yield return null;
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 min = transform.localPosition;
        min.z = minPointZ;
        Vector3 max = transform.localPosition;
        max.z = maxPointZ;

        Gizmos.color = (active) ? Color.red : Color.white;
        Gizmos.DrawWireCube(transform.parent.TransformPoint(min), Vector3.one * indicatorRadius);
        Gizmos.DrawLine(transform.parent.TransformPoint(min), transform.parent.TransformPoint(max));
        Gizmos.DrawWireCube(transform.parent.TransformPoint(max), Vector3.one * indicatorRadius);
    }
}
