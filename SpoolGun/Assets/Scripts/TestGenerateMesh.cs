using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class TestGenerateMesh : MonoBehaviour
{
    public List<GameObject> patches = new List<GameObject>();

    [SerializeField]
    private LayerMask targetLayers;
    [SerializeField]
    private string patchLayerName;
    [SerializeField]
    private float displayRadius = 0.1f;
    [SerializeField]
    private string sortingLayerName;
    [SerializeField]
    private Material thread;

    private Camera mainCamera; // cache camera for mouse position

    private List<Vector3> points = new List<Vector3>();
    private bool tracking = false;
    private LineRenderer line;
    private float offset = 0.0002f;

    //private int sortingOrder = 2000; 


    private void Start()
    {
        mainCamera = Camera.main;
        line = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            tracking = !tracking;

            if (points.Count > 0 && !tracking)
            {
                CreatePatch();
                points.Clear();
                line.positionCount = 0;
            }

            if (tracking)
            {
                RaycastHit hit;
                if (Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out hit, targetLayers))
                {
                    line.positionCount = 1;
                    line.SetPosition(0, hit.point);
                }
            }

            Debug.Log("Tracking is " + tracking);
        }

        if (tracking)
        {
            RaycastHit hit;

            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Placed point");

                if (Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out hit, targetLayers))
                {
                    points.Add(hit.point);

                    line.positionCount++;
                    line.SetPositions(points.ToArray());
                }
            }

            if (Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out hit, targetLayers))
            {
                line.SetPosition(line.positionCount - 1, hit.point);
            }
        }

    }

    public void setMaterial(Material newThread)
    {
        thread = newThread;
    }

    private void CreatePatch()
    {
        GameObject meshObject = MeshUtils.CreatePolygon(points);
        meshObject.transform.position += Vector3.up * offset * (patches.Count + 1);
        Debug.Log(Vector3.up * offset);
        meshObject.name = "Patch" + patches.Count;
        meshObject.layer = LayerMask.NameToLayer(patchLayerName);

        MeshCollider collider = meshObject.AddComponent<MeshCollider>();
        //collider.convex = true;
        //collider.isTrigger = true;

        MeshRenderer renderer = meshObject.GetComponent<MeshRenderer>();
        renderer.material = thread;
        //renderer.material.renderQueue = sortingOrder++;

        //SortingGroup sortingGroup = meshObject.AddComponent<SortingGroup>();
        //sortingGroup.sortingLayerName = sortingLayerName;
        //sortingGroup.sortingOrder = sortingOrder++;

        Patch patch = meshObject.AddComponent<Patch>();
        patch.patchType = (PatchType)Random.Range(0, 3);

        patches.Add(meshObject);
    }

    private void OnDrawGizmosSelected()
    {
        if (points != null)
        {
            Gizmos.color = Color.red;
            for (int i = 0; i < points.Count; i++)
            {
                Gizmos.DrawWireSphere(points[i], displayRadius);
            }
        }
    }
}
