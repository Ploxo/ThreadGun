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

    //[SerializeField]
    //private PatchData[] patchData;

    private float offset = 0.00025f;

    private int patchType = 0;
    private Material thread;
    private Material lining;
    private PhysicMaterial physic;
    public GameObject particleMouse;
    private GameObject particlePatch;

    //private IPatchEffector effector;


    public void setMaterial(int newType, Material thread, Material lining, PhysicMaterial physic, GameObject particleMouse,
        GameObject particlePatch)
    {
        patchType = newType;
        this.thread = thread;
        this.lining = lining;
        this.physic = physic;
        this.particleMouse = particleMouse;
        this.particlePatch = particlePatch;
    }

    public void CreatePatch(Vector3[] points)
    {
        // Zero the mesh on its centroid before generating, so we get the transform in the center
        // There probably is a ProBuilder method for this somewhere
        Vector3 center = CenterPolygon(points);
        GameObject meshObject = MeshUtils.CreatePolygon(points);
        meshObject.transform.position = center;

        meshObject.transform.position += Vector3.up * offset * (patches.Count + 1);
        meshObject.name = "Patch" + patches.Count;
        meshObject.layer = LayerMask.NameToLayer(patchLayerName);

        MeshCollider collider = meshObject.AddComponent<MeshCollider>();
        collider.material = physic;

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

        // We need to store the actual structs and modify them for ParticleSystem properties
        ParticleSystem ps = Instantiate(particlePatch, meshObject.transform, false).GetComponent<ParticleSystem>();
        ParticleSystem.ShapeModule sm = ps.shape;
        sm.meshRenderer = renderer;

        //TrailRenderer trail = Instantiate(threadPrefab).GetComponent<TrailRenderer>();
        //patch.threadObject = trail.transform;
        //trail.material = thread;
        //trail.startWidth = width;
        //trail.endWidth = width;

        //meshObject.AddComponent<AnimatePath>();

        patches.Add(meshObject);
    }

    // Center a polygon on the origin
    private Vector3 CenterPolygon(Vector3[] points)
    {
        Vector3 center = Vector3.zero;
        for (int i = 0; i < points.Length; i++)
        {
            center += points[i];
        }

        center /= points.Length;

        for (int i = 0; i < points.Length; i++)
        {
            points[i] -= center;
        }

        return center;
    }
}
