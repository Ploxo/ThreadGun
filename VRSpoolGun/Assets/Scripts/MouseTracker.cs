using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(LineRenderer))]
public class MouseTracker : MonoBehaviour
{
    public List<Vector3> points;
    public float distance = 0.1f;
    public bool tracking = false;
    public LayerMask layers;

    [SerializeField] private PolygonGenerator generator;
    [SerializeField] private AnimatePath animatePath;
    [SerializeField] private Transform emitter;

    private LineRenderer lineRenderer;
    private Camera mainCamera;
    private ParticleSystem[] mouseParticle;

    private Vector3 mousePosition;
    private int intersectionIndex = -1; // this will be non-negative for intersections
    private int patchType;


    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 0;

        mouseParticle = GetComponentsInChildren<ParticleSystem>();
        for (int i = 0; i < mouseParticle.Length; i++)
            mouseParticle[i].Stop();

        mainCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            if (!tracking)
                tracking = true;

            if (!mouseParticle[patchType].isPlaying)
                mouseParticle[patchType].Play();

            AddPoint();
            UpdateLineRenderer(lineRenderer.positionCount - 1, mousePosition);
        }
        if (Input.GetButtonUp("Fire1") && tracking)
        {
            tracking = false;

            mouseParticle[patchType].Stop();

            FinalizePoints();
        }
    }

    private void FixedUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out hit, 100f, layers))
        {
            mousePosition = hit.point;

            if (tracking)
                emitter.position = hit.point;
        }
    }

    public void SetMaterial(int type)
    {
        patchType = type;
    }

    // Add a point for the generation of a polygon, and update the line renderer to show it
    private void AddPoint()
    {
        if (points.Count == 0)
        {
            points.Add(mousePosition);
            AddToLineRenderer(mousePosition);
            AddToLineRenderer(mousePosition);
        }
        else if (Vector3.Distance(mousePosition, points[points.Count - 1]) >= distance && intersectionIndex == -1)
        {
            Vector3 position = GetIntersectionPoint(mousePosition);

            points.Add(position);
            UpdateLineRenderer(lineRenderer.positionCount - 1, position);
            AddToLineRenderer(position);
        }
    }

    // Complete a polygon or discard points if conditions are not met
    private void FinalizePoints()
    {
        if (points.Count < 3)
        {
            ClearPoints();

            return;
        }

        // Remove extra points if there is an intersection
        if (intersectionIndex >= 0)
        {
            List<Vector3> subList = points.GetRange(intersectionIndex, points.Count - intersectionIndex);
            Vector3[] copy = new Vector3[subList.Count];
            subList.CopyTo(copy);
            generator.CreatePatch(copy);
        }
        else
        {
            Vector3[] copy = new Vector3[points.Count];
            points.CopyTo(copy);
            generator.CreatePatch(copy);
        }

        ClearPoints();
    }

    private void ClearPoints()
    {
        points.Clear();
        lineRenderer.positionCount = 0;
        intersectionIndex = -1;
    }

    // Find an intersection if it exists and return this new position or the original value if not
    private Vector3 GetIntersectionPoint(Vector3 position)
    {
        // 4 points for triangle
        if (points.Count < 4)
            return position;

        Vector2 intersection;
        for (int i = points.Count - 2; i > 0; i--)
        {
            if (GeometryUtils.LineSegmentIntersection2D(points[i - 1], points[i], points[points.Count - 1], 
                position, out intersection))
            {
                intersectionIndex = i;

                position.x = intersection.x;
                position.z = intersection.y;

                break;
            }
        }

        return position;
    }

    private void AddToLineRenderer(Vector3 position)
    {
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, position);
    }

    private void UpdateLineRenderer(int index, Vector3 position)
    {
        lineRenderer.SetPosition(index, position);
    }

    private void OnDrawGizmos()
    {
        foreach (var p in points)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(p, 0.15f);
        }
    }
}