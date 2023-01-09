using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ThreadType
{
    Ice,
    Gum,
    Lava,
    None
}

public class Patch : MonoBehaviour
{
    public ThreadType patchType;
    public IPatchEffector effect;
    public Vector3[] points;
    public ParticleSystem particlePatch;

    private MeshRenderer rend;

    //public Transform threadObject;
    //private AnimatePath anim;

    //private bool building = true;


    public void Start()
    {
        rend = GetComponent<MeshRenderer>();

        //particlePatch.Play();

        //anim = GetComponent<AnimatePath>();
        //anim.target = threadObject;

        //GetComponent<MeshRenderer>().enabled = false;
        //StartCoroutine(BuildPatchCoroutine());
    }

    private IEnumerator WaitPlayParticles()
    {
        yield return new WaitForEndOfFrame();

        particlePatch.Play();
    }

    //private IEnumerator BuildPatchCoroutine()
    //{
    //    Debug.Log("Started buildpatchcoroutine");
    //    yield return anim.AnimateCoroutine(points);

    //building = false;
    //rend.enabled = true;
    //}

    public void ApplyEffect(GameObject go)
    {
        //if (building)
        //    return;

        effect.ApplyEffect(go);
    }
}
