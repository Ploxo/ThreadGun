using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public ThreadType currentType = ThreadType.None;

    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float raycastRange = 1.1f;

    public IPatchEffector currentEffect;


    void FixedUpdate()
    {
        CheckCurrentPatch();

        ApplyCurrentEffect();
    }

    private void CheckCurrentPatch()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, raycastRange, layerMask))
        {
            Patch patch = hit.collider.gameObject.GetComponent<Patch>();
            if (patch != null)
            {
                if (patch.patchType != currentType)
                {
                    //SwapEffect(patch.patchType, patch.effect);
                }
            }
            else
            {
                currentType = ThreadType.None;
            }
        }
    }

    private void ApplyCurrentEffect()
    {
        if (currentType != ThreadType.None)
        {
            currentEffect.ApplyEffect(gameObject);
        }
    }

    private void SwapEffect(ThreadType newType, IPatchEffector newEffect)
    {
        if (currentType != ThreadType.None)
        {
            //currentEffect.RemoveEffect(gameObject);
        }

        currentType = newType;
        currentEffect = newEffect;
    }
}
