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

    [SerializeField] private LayerMask targetLayers;
    [SerializeField] private string patchLayerName;

    private float offset = 0.00025f;


    public void CreatePatch(Vector3[] points)
    {
        // Zero the mesh on its centroid before generating, so we get the transform in the center
        // There probably is a ProBuilder method for this somewhere
        Vector3 center = GeometryUtils.CenterPolygon(points);
        GameObject meshObject = MeshUtils.CreatePolygonShape(points);
        meshObject.transform.position = center;

        // Get the active thread material
        Thread thread = ThreadManager.Instance.GetActiveThread();

        meshObject.transform.position += Vector3.up * offset * (patches.Count + 1);
        meshObject.name = "Patch" + patches.Count;
        meshObject.layer = LayerMask.NameToLayer(patchLayerName);

        MeshCollider collider = meshObject.AddComponent<MeshCollider>();
        collider.material = thread.physicMaterial;

        MeshRenderer renderer = meshObject.GetComponent<MeshRenderer>();
        renderer.material = thread.threadMaterial;

        Patch patch = meshObject.AddComponent<Patch>();
        patch.effect = thread.effector;
        //if ((int)thread.threadType == 0)
        //{
        //    patch.effect = new IceEffector();
        //}
        //else if ((int)thread.threadType == 1)
        //{
        //    patch.effect = new GumEffector();
        //}
        //patch.patchType = (ThreadType)patchType;
        patch.patchType = thread.threadType;
        patch.points = points;

        // We need to store the actual structs and modify them for ParticleSystem properties
        ParticleSystem ps = Instantiate(thread.patchParticles, meshObject.transform, false).GetComponent<ParticleSystem>();
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
}
