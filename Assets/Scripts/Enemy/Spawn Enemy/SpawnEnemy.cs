using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] public List<EnemyInfo> enemyInfos;
    
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
            SpawnRandomEnemyGroup();
            yield return new WaitForSeconds(spawnDelay);
        }
        // ReSharper disable once IteratorNeverReturns
    }

    private void SpawnRandomEnemyGroup()
    {
        // var enemyInfo = enemyInfos[Random.Range(0, enemyInfos.Count)];
        var enemyInfo = enemyInfos[(int)EnemyType.Archer];//DEBUG
        enemyFactory.CreateEnemyGroup(enemyInfo.enemyPrefab, enemyInfo.countInGroup);
    }

    private void SpawnEnemyGroupByPrefab(GameObject prefab)
    {
        var enemy = enemyInfos.Find(enemyInfo => enemyInfo.enemyPrefab == prefab);
        if(enemy != null)
        {
            enemyFactory.CreateEnemyGroup(enemy.enemyPrefab, enemy.countInGroup);
        }
    }
    
}