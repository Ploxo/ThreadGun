using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPatchData", menuName = "ScriptableObject/PatchData")]
[System.Serializable]
public class PatchData : ScriptableObject
{
    public PatchType patchType;
    public IPatchEffector effect;
    public Material threadMaterial;
    public Material liningMaterial;
    public ParticleSystem particlesNpc;
    public ParticleSystem particlesPatch;
    public ParticleSystem particlesPointer;
}
