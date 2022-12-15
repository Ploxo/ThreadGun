using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewThread", menuName = "ScriptableObject/Thread")]
public class Thread : ScriptableObject
{
    public ThreadType threadType;
    public Material threadMaterial;
    public Material liningMaterial;
    public PhysicMaterial physicMaterial;
    public GameObject patchParticles;
    public GameObject mouseParticles;
    [Tooltip("The effect that runs on entities in the game")]
    public Effector effector;
}
