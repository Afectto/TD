using System.Collections.Generic;
using UnityEngine;


public class ChainTriger : MonoBehaviour
{
    private List<GameObject> _enemies = new List<GameObject>();

    public GameObject GetRandomEnemyInRadius()
    {
        var enemyCount = _enemies.Count;
        if (enemyCount > 0)
        {
            return _enemies[Random.Range(0, _enemies.Count)];
        }

        return null;
    }

    public void ClearEnemies()
    {
        _enemies.Clear();
    }
    
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            _enemies.Add(collision.gameObject);
        }
    }
    
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            _enemies.Remove(collision.gameObject);
        }
    }
}
