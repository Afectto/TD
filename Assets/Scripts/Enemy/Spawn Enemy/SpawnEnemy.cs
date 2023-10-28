using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] public List<GameObject> enemyPrefabs;
    public float spawnDelay;
    [SerializeField]public EnemyFactory enemyFactory;
    public bool isNeedSpawn = true;
    private void Start()
    {
        if(isNeedSpawn) StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        while(true)
        {
            enemyFactory.CreateEnemyGroup(enemyPrefabs[0], 1);
            yield return new WaitForSeconds(spawnDelay);
        }
        // ReSharper disable once IteratorNeverReturns
    }

}