using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewLavaEffector", menuName = "ScriptableObject/LavaEffector")]
public class LavaEffector : Effector
{


    public override void ApplyEffect(GameObject go)
    {
        go.GetComponent<EnemyNPC>().Damaged(1);
    }
}
