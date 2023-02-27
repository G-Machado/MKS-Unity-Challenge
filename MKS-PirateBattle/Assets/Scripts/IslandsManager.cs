using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.AI;

public class IslandsManager : MonoBehaviour
{
    public GameObject[] islandGroups;

    public void SpawnRandomGroup()
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
