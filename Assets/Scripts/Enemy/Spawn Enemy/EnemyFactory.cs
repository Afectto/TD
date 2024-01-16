using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyFactory  : MonoBehaviour
{
    [SerializeField] private float xMin = -13f;
    [SerializeField] private float xMax = 13f;
    [SerializeField] private float yMin = -6f;
    [SerializeField] private float yMax = 6f;
    
    [SerializeField] private float avoidanceRadius = 7f;

    [SerializeField] private List<EnemyObjectPool> listEnemyObjectPools;
    
    public void CreateEnemyGroup(GameObject enemyPrefab, int count)
    {
        if (enemyPrefab == null || count <= 0)
        {
            Debug.LogError("Invalid parameters for CreateEnemyGroup.");
            return;
        }

        Vector3 groupPosition = GenerateRandomPosition();
        for (int i = 0; i < listEnemyObjectPools.Count; i++)
        {
            if (listEnemyObjectPools[i].enemyPrefab == enemyPrefab)
            {
                var pool = listEnemyObjectPools[i].enemyObjectPool;
                for (int j = 0; j < count; j++)
                {
                    CreateEnemy(pool, groupPosition);
                }
            }
        }
    }

    void CreateEnemy(GameObjectPool pool, Vector3 groupPosition)
    {
        GameObject enemy = pool.Get();
        Vector3 offset = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f).normalized;
        Vector3 spawnPosition = groupPosition + offset;
        enemy.transform.position = spawnPosition;

        void ONDestroyAction(GameObject thisEnemy)
        {
            if (thisEnemy == enemy)
            {
                pool.Return(thisEnemy);
                Enemy.IsOnDestroy -= ONDestroyAction;
            }
        }

        Enemy.IsOnDestroy += ONDestroyAction;
    }

    private Vector3 GenerateRandomPosition()
    {
        float randX, randY;
        do
        {
            randX = Random.Range(xMin, xMax);
            randY = Random.Range(yMin, yMax);
        } while (Vector2.Distance(new Vector2(randX, randY), Vector2.zero) < avoidanceRadius);

        return new Vector3(randX, randY, 0);
    }
}