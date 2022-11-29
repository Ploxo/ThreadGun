using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PatchType
{
    Slippery,
    Bouncy
}

public class Patch : MonoBehaviour
{
    public PatchType patchType;
    public IPatchEffector effect;
    public Vector3[] points;
    public Transform threadObject;

    private AnimatePath anim;
    private MeshRenderer rend;

    private bool building = true;


    public void Start()
    {
        anim = GetComponent<AnimatePath>();
        //anim.target = threadObject;

        rend = GetComponent<MeshRenderer>();

        //GetComponent<MeshRenderer>().enabled = false;
        //StartCoroutine(BuildPatchCoroutine());
    }

    private IEnumerator BuildPatchCoroutine()
    {
        Debug.Log("Started buildpatchcoroutine");
        yield return anim.AnimateCoroutine(points);

        //building = false;
        //rend.enabled = true;
    }

    public void ApplyEffect(GameObject go)
    {
        if (building)
            return;

        effect.ApplyEffect(go);
    }
}
