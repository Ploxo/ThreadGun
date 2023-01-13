using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSpawner : MonoBehaviour
{

    private int buffer = 40;
    public GameObject eBase;
    private bool canSpawn;
    private int bases = 0;

    [SerializeField] LayerMask spawnLayer;
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
        Vector3 minimumRange = new Vector3(9.3f, 0, 9.3f);
        Vector3 maximumRange = new Vector3(10, 0, 10);
        Vector3 testBoxPosition = new Vector3(4.1f, 0, 7.3f);

        for (int i = 0; i < 100; i++)
        {
            Vector3 boxPosition = new Vector3(Random.Range(9.3f, 9.3f), Random.Range(0, 0), Random.Range(9.3f, 9.3f));

            if (!Physics.CheckBox(boxPosition, eBase.transform.localScale, Quaternion.identity, spawnLayer))
            {
                GameObject newBase = Instantiate(eBase, boxPosition, Quaternion.identity);
                newBase.GetComponent<EnemyBase>().creator = this;
                break;
            }
          Debug.Log("Target hit.");
        }

        Debug.Log("YES!");
    }
}
