using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] public List<GameObject> enemyPrefabs;
    public float spawnDelay;
    [SerializeField]public EnemyFactory enemyFactory;
    public bool isNeedSpawn = true;
    public int countEnemyInGroup = 1;
    private void Start()
    {
        if(isNeedSpawn) StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        while(true)
        {
            enemyFactory.CreateEnemyGroup(enemyPrefabs[Random.Range(0, enemyPrefabs.Count)], countEnemyInGroup);
            yield return new WaitForSeconds(spawnDelay);
        }
        // ReSharper disable once IteratorNeverReturns
    }

}