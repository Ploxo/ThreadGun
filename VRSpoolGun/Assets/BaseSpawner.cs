using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSpawner : MonoBehaviour
{

    private int buffer = 40;
    public GameObject eBase;
    private bool canSpawn;
    private int bases = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ((int)Time.time % buffer > 30 && canSpawn && bases < 2)
        {
            SpawnBase();
            bases++;
            canSpawn = false;
        }
        else if ((int)Time.time % buffer < 29 && !canSpawn) canSpawn = true;
    }

    public void destoryedBase()
    {
        bases--;
    }

    void SpawnBase()
    {
        GameObject newBase = Instantiate(eBase, new Vector3(Random.Range(3,7),0,Random.Range(3,7)), Quaternion.identity);
        newBase.GetComponent<EnemyBase>().creator = this; 
    }
}
