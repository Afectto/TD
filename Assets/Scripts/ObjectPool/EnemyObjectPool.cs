using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObjectPool : MonoBehaviour
{
    public GameObjectPool enemyObjectPool;
    public GameObject enemyPrefab;
    private void Awake()
    {
        enemyObjectPool = new GameObjectPool(enemyPrefab, gameObject);
    }
}
