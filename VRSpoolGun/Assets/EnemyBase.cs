using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    private int bChance = 0;
    private int difficulty = 1;
    private int buffer = 4;
    private bool canSpawn = true;
    public BaseSpawner creator;
    private GameObject newEnemy = null;

    private Transform enemyTarget;

    public GameObject enemy;

    // Start is called before the first frame update
    void Start()
    {
        enemyTarget = GameObject.FindGameObjectWithTag("Base").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (canSpawn && (int) Time.time%buffer < 1) //spawns enemy
        {
            canSpawn = false;
            SpawnEnemy();
            if (Random.value*bChance > 1)
            {
                if (creator != null) creator.destoryedBase();
                Destroy(gameObject);
            }
            bChance++;
            return;
        }
        if ((int)Time.time % buffer > 2)
        {
            canSpawn = true;
            return;
        }

    }

    void SpawnEnemy()
    {
        newEnemy = Instantiate(enemy, gameObject.transform.position, Quaternion.identity);
        newEnemy.transform.GetChild(0).GetComponent<NavMeshTest>().SetTargetTransform(enemyTarget);
    }
}
