using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSpawner : MonoBehaviour
{
    private Transform parent;

    private int buffer = 4; 
    public GameObject eBase;
    private bool canSpawn;
    private int bases = 0;
    private int resetTime;
    private int spawnTime;
    private int maxbases = 30;
    private float minRange = 3.4f;

    [SerializeField] LayerMask spawnLayer;
    // Start is called before the first frame update
    void Start()
    {
        parent = GameObject.FindGameObjectWithTag("NavMesh").transform;

        if (buffer < 4) buffer = 4;
        spawnTime = buffer - (int)buffer / 4;
        resetTime = spawnTime - 1;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("st=" + spawnTime + " rt=" + resetTime);
        if ((int)Time.time % buffer >= spawnTime && canSpawn && bases < maxbases)
        {
            //Debug.Log("Spawning");
            SpawnBase();
            bases++;
            canSpawn = false;
        }
        else if ((int)Time.time % buffer < resetTime && !canSpawn) canSpawn = true;
    }

    public void destoryedBase()
    {
        bases--;
    }

    void SpawnBase()
    {

        for (int i = 0; i < 100; i++)
        {
            //Debug.Log("in da loop");
            float x = Random.Range(-9.5f, 9.5f);
            float z = Random.Range(-9.5f, 9.5f);
            while ((x > -minRange && x < minRange) && (z > -minRange && z < minRange)) 
            {
                x = Random.Range(-9.5f, 9.5f);
                z = Random.Range(-9.5f, 9.5f);
                Debug.Log("while loop x=" + x);
            }
            Debug.Log("x=" + x + " z=" +z);

            Vector3 boxPosition = new Vector3(x, 0,z);
            Debug.Log("box position x=" + x + " box position z=" + z);

            if (!Physics.CheckBox(boxPosition, eBase.transform.localScale, Quaternion.identity, spawnLayer))
            {
                GameObject newBase = Instantiate(eBase, boxPosition, Quaternion.identity, parent);
                newBase.GetComponent<EnemyBase>().creator = this;
                break;
            }
          //Debug.Log("Target hit.");
        }

        //Debug.Log("YES!");
    }
}
