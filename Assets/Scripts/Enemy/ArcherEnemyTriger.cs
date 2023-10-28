using UnityEngine;

public class ArcherEnemyTriger : MonoBehaviour
{
    [SerializeField]private ArcherEnemy _archerEnemy;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Tower")) return;
        
        _archerEnemy.isNeedMove = false;

        var weapon = _archerEnemy.GetComponent<Weapon>();
        weapon.target = collision.transform;
        weapon._isShoot = false;
    }
}