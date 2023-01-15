using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewLavaEffector", menuName = "ScriptableObject/LavaEffector")]
public class LavaEffector : Effector
{
    public float tick = 0.5f;
    public float damage = 1f;

    public override void ApplyEffect(GameObject go)
    {
        DamageOverTime dot = go.GetComponent<DamageOverTime>();

        if (dot != null)
        {
            dot.Refresh();
        }
        else 
        {
            dot = go.AddComponent<DamageOverTime>();
            dot.target = go;
            dot.duration = duration;
            dot.tick = tick;
            dot.damage = damage;

            dot.StartDot();
        }
    }
}
