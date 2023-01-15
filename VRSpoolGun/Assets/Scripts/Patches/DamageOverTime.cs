using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOverTime : MonoBehaviour, IRefreshable
{
    public float damage;
    public float duration;
    public float tick;
    public GameObject target;

    float time = 0f;

    public void Refresh()
    {
        //Debug.Log("Refreshing dot");

        time = 0f;
    }

    public void StartDot()
    {
        Debug.Log("Starting dot");
        StartCoroutine(DoDamageOverTime());
    }

    private IEnumerator DoDamageOverTime()
    {
        EnemyNPC health = target.GetComponent<EnemyNPC>();
        while (time < duration)
        {
            health.Damaged(damage);

            yield return new WaitForSeconds(tick);
            time += tick;
        }

        Destroy(this);
    }
}
