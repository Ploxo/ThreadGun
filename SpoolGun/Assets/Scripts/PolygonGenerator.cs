using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PolygonGenerator : MonoBehaviour
{
    public List<GameObject> patches = new List<GameObject>();

    public float segmentSpeed = 3f;
    public float stopTime = 0.05f;
    public float width = 0.5f;

    [SerializeField]
    private LayerMask targetLayers;
    [SerializeField]
    private string patchLayerName;
    [SerializeField]
    private IPatchEffector patchEffect;
    [SerializeField]
    private Material thread;
    [SerializeField]
    private Material threadMaterial;
    [SerializeField]
    private GameObject threadPrefab;

    private int patchType = 0;
    private float offset = 0.00025f;


    private void Start()
    {
        setMaterial(thread, threadMaterial, 0);
    }

    public void setMaterial(Material newThread, Material newThreadMaterial, int newType)
    {
        thread = newThread;
        threadMaterial = newThreadMaterial;
        patchType = newType;
    }

    public void CreatePatch(Vector3[] points)
    {
        GameObject meshObject = MeshUtils.CreatePolygon(points);
        meshObject.transform.position += Vector3.up * offset * (patches.Count + 1);
        meshObject.name = "Patch" + patches.Count;
        meshObject.layer = LayerMask.NameToLayer(patchLayerName);

        MeshCollider collider = meshObject.AddComponent<MeshCollider>();

        MeshRenderer renderer = meshObject.GetComponent<MeshRenderer>();
        renderer.material = thread;

        Patch patch = meshObject.AddComponent<Patch>();
        if (patchType == 0)
        {
            patch.effect = new IceEffector();
        }
        else if (patchType == 1)
        {
            patch.effect = new BubbleGumEffector();
        }
        patch.patchType = (PatchType)patchType;
        patch.points = points;

        //TrailRenderer trail = Instantiate(threadPrefab).GetComponent<TrailRenderer>();
        //patch.threadObject = trail.transform;
        //trail.material = thread;
        //trail.startWidth = width;
        //trail.endWidth = width;

        //meshObject.AddComponent<AnimatePath>();

        patches.Add(meshObject);
    }
}
