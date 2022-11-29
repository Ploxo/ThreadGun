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
    [SerializeField]
    private Material thread;
    [SerializeField]
    private Material lining;
    [SerializeField]
    private ParticleSystem particleMouse;
    [SerializeField]
    private ParticleSystem particlePatch;
    private IPatchEffector effector;


    private void Start()
    {
        setMaterial(0, thread, lining, particleMouse, particlePatch);
    }

    public void setMaterial(int newType, Material thread, Material lining, ParticleSystem particleMouse, ParticleSystem particlePatch)
    {
        patchType = newType;
        this.thread = thread;
        this.lining = lining;
        this.particleMouse = particleMouse;
        this.particlePatch = particlePatch;
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

        patch.particlePatch = particlePatch;

        //TrailRenderer trail = Instantiate(threadPrefab).GetComponent<TrailRenderer>();
        //patch.threadObject = trail.transform;
        //trail.material = thread;
        //trail.startWidth = width;
        //trail.endWidth = width;

        //meshObject.AddComponent<AnimatePath>();

        patches.Add(meshObject);
    }
}
