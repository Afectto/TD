using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] public List<EnemyInfo> enemyInfos;

    private float _startSpawnDelay;
    public float spawnDelay;
    [SerializeField]public EnemyFactory enemyFactory;
    public bool isNeedSpawn = true;
    private const float DelayToReduce = 30f;

    private void Start()
    {
        _startSpawnDelay = spawnDelay;
        if (isNeedSpawn)
        {
            StartCoroutine(Spawn());
            StartCoroutine(ReduceSpawnDelay());
            StartCoroutine(IncreasedEnemyStatsMultiplayer());
        }
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

    private IEnumerator ReduceSpawnDelay()
    {
        while(true)
        {
            yield return new WaitForSeconds(DelayToReduce);
            var currentMult = EnemyStatsMultiplayer.GetMultiplayer(MultiplayerType.SpawnTime);
            if (currentMult < 15)
            {
                EnemyStatsMultiplayer.IncreasedMultiplayer(MultiplayerType.SpawnTime, 1.07f);
            }

            spawnDelay = _startSpawnDelay / (currentMult * 1.07f);
        }
        // ReSharper disable once IteratorNeverReturns
    }

    private IEnumerator IncreasedEnemyStatsMultiplayer()
    {
        while (true)
        {
            yield return  new WaitForSeconds(15f);
        
            EnemyStatsMultiplayer.IncreasedMultiplayer(MultiplayerType.Damage, 1.05f);
            EnemyStatsMultiplayer.IncreasedMultiplayer(MultiplayerType.Health, 1.05f);
            EnemyStatsMultiplayer.IncreasedMultiplayer(MultiplayerType.Reward, 1.04f);
            EnemyStatsMultiplayer.IncreasedMultiplayer(MultiplayerType.AttackRate, 1.025f);
        }
        // ReSharper disable once IteratorNeverReturns
    }
    
    private void SpawnRandomEnemyGroup()
    {
        // var enemyInfo = enemyInfos[Random.Range(0, enemyInfos.Count)];
        var enemyInfo = enemyInfos[(int)EnemyType.Knight];//DEBUG
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