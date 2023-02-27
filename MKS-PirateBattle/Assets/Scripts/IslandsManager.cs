using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.AI;

public class IslandsManager : MonoBehaviour
{
    public GameObject[] islandGroups;

    // Start is called before the first frame update
    void Start()
    {
        SpawnGroup(Random.Range(0, islandGroups.Length));
    }

    private void SpawnGroup(int index)
    {
        GameObject groupClone = Instantiate(islandGroups[index], transform);
        NavMeshBuilder.ClearAllNavMeshes();
        NavMeshBuilder.BuildNavMesh();
    }
}
