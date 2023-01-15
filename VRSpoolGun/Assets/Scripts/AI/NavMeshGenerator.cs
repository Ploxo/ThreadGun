using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class NavMeshGenerator : MonoBehaviour
{
    private static NavMeshGenerator instance;

    public static NavMeshGenerator Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<NavMeshGenerator>();

                if (instance == null)
                    instance = new GameObject("NavMeshGenerator").AddComponent<NavMeshGenerator>();
            }

            return instance;
        }
    }


    public NavMeshSurface navMesh;

    public void GenerateHumanoid()
    {
        Debug.Log("Rebuilding Humanoid");
        navMesh.agentTypeID = 0;
        navMesh.BuildNavMesh();
    }

    public void GenerateEnemy()
    {
        Debug.Log("Rebuilding Enemy");
        navMesh.agentTypeID = 1;
        navMesh.BuildNavMesh();
    }
}
