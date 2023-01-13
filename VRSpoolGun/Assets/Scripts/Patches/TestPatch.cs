using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Exists only to test patches by creating them on Start
public class TestPatch : MonoBehaviour
{
    [SerializeField]
    private ThreadType type;


    void Start()
    {
        Thread thread = ThreadManager.Instance.GetThread(type);

        gameObject.layer = LayerMask.NameToLayer("Patches");

        MeshCollider collider = gameObject.AddComponent<MeshCollider>();
        collider.material = thread.physicMaterial;
        MeshRenderer renderer = gameObject.GetComponent<MeshRenderer>();
        renderer.material = thread.threadMaterial;

        Patch patch = gameObject.AddComponent<Patch>();
        //patch.effect = thread.effector;
        patch.patchType = thread.threadType;

        ParticleSystem ps = Instantiate(thread.patchParticles, gameObject.transform, false).GetComponent<ParticleSystem>();
        ParticleSystem.ShapeModule sm = ps.shape;
        sm.meshRenderer = renderer;
    }
}
