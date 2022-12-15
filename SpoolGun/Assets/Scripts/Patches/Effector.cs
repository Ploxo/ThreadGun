using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Effector : ScriptableObject, IPatchEffector
{
    public abstract void ApplyEffect(GameObject go);
}
