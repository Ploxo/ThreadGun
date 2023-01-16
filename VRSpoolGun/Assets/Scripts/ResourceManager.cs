using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public int playerResources = 0;

    public LayerMask layers;
    public  List<Transform> spawnPoints;
    public float timer = 40f;
    public int spawnCount = 5;

    public Transform resourceTarget;

    public TextMeshProUGUI resourceText;

    [SerializeField] private GameObject resourcePrefab;
    [SerializeField] private List<ResourceCollector> gatherers;

    private List<GameObject> resourceObjects = new List<GameObject>();



    private void Awake()
    {
        for (int i = 0; i < spawnPoints.Count; i++)
        {
            GameObject go = Instantiate(resourcePrefab, spawnPoints[i].position, Quaternion.identity, gameObject.transform);
            resourceObjects.Add(go);
            go.SetActive(false);
        }

        InvokeRepeating("SpawnResources", 0f, timer);
    }

    private void Start()
    {
        resourceText.text = "Resources: " + playerResources;
    }

    public void SetResourceTarget(Transform target)
    {
        resourceTarget = target;
        foreach (var gatherer in gatherers)
        {
            gatherer.SetTarget(target);
        }
    }

    //private Vector3 RandomPosition(Vector3 min, Vector3 max)
    //{
    //    Vector3 halfExtents = resourcePrefab.GetComponent<MeshRenderer>().bounds.extents;
    //    do
    //    {
    //        Vector3 position = new Vector3(Random.Range(min.x, max.x), 0f, Random.Range(min.z, max.z));
    //    } while (Physics.CheckBox(position, halfExtents, resourcePrefab.transform.rotation, layers));


    //}

    public bool GetRandomNode(out Transform target)
    {
        target = null;

        ShufflePositions();

        int i = 0;
        while (i < resourceObjects.Count)
        {
            if (resourceObjects[i].activeSelf)
            {
                target = resourceObjects[i].transform;
                return true;
            }
            i++;
        }

        return false;
    }

    public void AddResource(int amountToAdd)
    {
        playerResources = Mathf.Min(Mathf.Max(0, playerResources + amountToAdd), 150);
        resourceText.text = "Resources: " + playerResources;
    }

    [SerializeField] Collider reloadZone;
    public int RetrieveResource(Vector3 position, int desiredAmount)
    {
        Vector3 closestPoint = reloadZone.ClosestPoint(position);
        if (closestPoint != position)
            return 0;

        int amount;
        if (desiredAmount <= playerResources)
            amount = desiredAmount;
        else
            amount = playerResources;

        playerResources = playerResources - amount;

        return amount;
    }

    // Random, unique points
    public void SpawnResources()
    {
        ShufflePositions();
        for (int i = 0; i < spawnCount; i++)
        {
            resourceObjects[i].SetActive(true);
            resourceObjects[i].GetComponent<ResourceNode>().ResetNode();
        }
    }

    private void ShufflePositions()
    {
        for (int i = resourceObjects.Count-1; i > 0; i--)
        {
            int index = Random.Range(0, i+1);

            GameObject tmp = resourceObjects[index];
            resourceObjects[index] = resourceObjects[i];
            resourceObjects[i] = tmp;
        }
    }
}
