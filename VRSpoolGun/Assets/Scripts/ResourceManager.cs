using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public int playerResources = 0;
    public  List<Transform> spawnPoints;

    public TextMeshProUGUI resourceText;

    [SerializeField] private GameObject resourcePrefab;

    private List<GameObject> resourceObjects = new List<GameObject>();


    private void Awake()
    {
        for (int i = 0; i < spawnPoints.Count; i++)
        {
            GameObject go = Instantiate(resourcePrefab, spawnPoints[i].position, Quaternion.identity, gameObject.transform);
            resourceObjects.Add(go);
        }

        SpawnResources(3);
    }

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
        playerResources = Mathf.Min(100, playerResources + amountToAdd);
        resourceText.text = "Resources: " + playerResources;
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
