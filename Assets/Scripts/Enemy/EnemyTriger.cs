using UnityEngine;

public abstract class EnemyTriger : MonoBehaviour
{
    [SerializeField]private Enemy _Enemy;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Tower")) return;
        
        _Enemy.isNeedMove = false;

        var weapon = _Enemy.GetComponent<Weapon>();
        weapon.target = collision.transform;
        weapon._isShoot = false;
    }
}
