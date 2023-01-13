using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager2 : MonoBehaviour
{
    public ThreadType currentType = ThreadType.None;
    public IRefreshable currentEffect;

    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float raycastRange = 1.1f;
    [SerializeField] private EnemyMovement movement;

    private ParticleSystem[] particles;
    private float currentDuration = 0f;


    private void Start()
    {
        particles = transform.GetComponentsInChildren<ParticleSystem>();
        StopParticles();
    }

    void FixedUpdate()
    {
        CheckCurrentPatch();
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
                    Debug.LogWarning("Found other type");
                    SwapEffect(patch.patchType);
                }
                else
                {
                    RefreshTimer();
                }
            }
            else
            {
                currentType = ThreadType.None;
            }
        }
    }

    private void SwapEffect(ThreadType newType)
    {
        StopAllCoroutines();
        StopParticles();

        currentType = newType;
        currentDuration = ThreadManager.Instance.GetThread(currentType).effector.duration;

        StartCoroutine(EffectTimer());
    }

    private void RefreshTimer()
    {
        currentDuration = ThreadManager.Instance.GetThread(currentType).effector.duration;
        movement.SetMovementType(currentType);
    }

    // Start a timed effect
    private IEnumerator EffectTimer()
    {
        movement.SetMovementType(currentType);
        PlayParticles((int)currentType);

        while (currentDuration > 0f)
        {
            currentDuration -= Time.deltaTime;

            yield return null;
        }

        movement.SetMovementType(ThreadType.None);
        StopParticles();

        yield return null;
    }

    private void PlayParticles(int index)
    {
        for (int i = 0; i < particles.Length; i++)
        {
            particles[i].Stop();

            if (i == index)
                particles[i].Play(true);
        }
    }

    private void StopParticles()
    {
        for (int i = 0; i < particles.Length; i++)
            particles[i].Stop();
    }
}
