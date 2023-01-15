using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutter : MonoBehaviour
{
    public ResourceManager resourceManager;

    public Transform origin;
    public LayerMask layers;
    public OVRInput.Button activateButton;
    public OVRInput.Button cancelButton;

    private bool tracking;
    private Vector3 pointerPosition;
    private LineRenderer lineRenderer;

    Patch targetPatch; // the patch we have targeted for deletion
 

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        if (tracking && OVRInput.GetDown(cancelButton))
        {
            tracking = false;
            ClearPoints();
        }

        if (OVRInput.GetDown(activateButton))
        {
            if (!tracking)
            {
                tracking = true;

                RaycastHit hit;
                if (Physics.Raycast(origin.position, origin.forward, out hit, 100f, layers))
                {
                    pointerPosition = hit.point;

                    AddToLineRenderer(pointerPosition);
                    AddToLineRenderer(pointerPosition);
                }
            }
            else
            {
                tracking = false;
                ClearPoints();

                if (targetPatch != null)
                {
                    RemovePatch(targetPatch);
                }
            }
        }

        if (tracking)
        {
            UpdateLineRenderer(1, pointerPosition);
        }
    }

    private void FixedUpdate()
    {
        if (tracking)
        {
            RaycastHit hit;
            if (Physics.Raycast(origin.position, origin.forward, out hit, 100f, layers))
            {
                pointerPosition = hit.point;

                Patch patch = hit.transform.GetComponent<Patch>();

                if (patch != null)
                {
                    targetPatch = patch;

                    if (GetIntersectionPoint(patch.points, pointerPosition, out Vector3 intersectionPoint))
                    {
                        lineRenderer.material.color = Color.red;
                    }
                }
            }
        }
    }

    // Update the line renderer visuals with a new point
    private void AddToLineRenderer(Vector3 position)
    {
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, position);
    }

    private void UpdateLineRenderer(int index, Vector3 position)
    {
        lineRenderer.SetPosition(index, position);
    }

    private void ClearPoints()
    {
        lineRenderer.positionCount = 0;
    }

    private void RemovePatch(Patch patch)
    {
        Destroy(patch.gameObject);

        if (patch.patchType == ThreadType.Lava)
        {
            // Rebake navmesh for allies
            NavMeshGenerator.Instance.GenerateHumanoid();
        }
    }

    // Find an intersection if it exists and return this new position or the original value if not
    private bool GetIntersectionPoint(Vector3[] points, Vector3 position, out Vector3 intersectionPoint)
    {
        intersectionPoint = Vector3.zero;
        Vector2 intersection;
        for (int i = points.Length - 2; i > 0; i--)
        {
            if (GeometryUtils.LineSegmentIntersection2D(points[i - 1], points[i], points[points.Length - 1],
                position, out intersection))
            {
                intersectionPoint.x = intersection.x;
                intersectionPoint.z = intersection.y;

                return true;
            }
        }

        return false;
    }
}
