using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class EnemyEvoker : MonoBehaviour
{
    public EnemyController enemyController;
    public Transform spawner;
    public Transform[] spawnLocations;
    public GameObject[] minions;
    public GameObject enemyPrefab;
    public LayerMask obstaclesLayer;

    private void Awake()
    {
        enemyController = GetComponent<EnemyController>();
        spawnLocations = GetComponentsOnlyInChildren(spawner);
        minions = new GameObject[spawnLocations.Length];
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            enemyController.enemyAnim.SetTrigger("Attack");
        }
    }
    public void SpawnSkeleton()
    {
        for (int i = 0; i < spawnLocations.Length; i++)
        {
            Transform skeletonSpawn = spawnLocations[i];
            float distance = Vector3.Distance(spawner.position, skeletonSpawn.position);
            Vector3 direction = (skeletonSpawn.position - spawner.position).normalized;
            Vector3 spawnPosition = skeletonSpawn.position;
            Ray ray = new Ray(spawner.position, direction);

            if (!Physics.Raycast(ray, distance, obstaclesLayer))
            {
                minions[i] = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity, skeletonSpawn);
            }
        }
    }

    public void ReleaseMinios()
    {
        for (int i = 0; i < minions.Length; i++)
        {
            if(minions[i])
            {
                minions[i].transform.parent = null;
            }
        }
    }

    private Transform[] GetComponentsOnlyInChildren(Transform parent)
    {
        Transform[] children = parent.GetComponentsInChildren<Transform>();
        Transform[] firstChildren = new Transform[parent.childCount];
        int index = 0;
        foreach (Transform child in children)
        {
            if (child.parent == parent)
            {
                firstChildren[index] = child;
                index++;
            }
        }
        return firstChildren;
    }
}
