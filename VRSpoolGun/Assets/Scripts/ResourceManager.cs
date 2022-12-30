using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public int playerResources = 0;
    public  List<Vector3> spawnPoints;

    [SerializeField] private GameObject resourcePrefab;

    private List<GameObject> resourceObjects;


    void Start()
    {
        for (int i = 0; i < spawnPoints.Count; i++)
        {
            GameObject go = Instantiate(resourcePrefab, spawnPoints[i], Quaternion.identity, gameObject.transform);
            resourceObjects.Add(go);
        }
    }

    // Random, unique points
    public void SpawnResources(int amount)
    {
        ShufflePositions();
        for (int i = 0; i < amount; i++)
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
