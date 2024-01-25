using UnityEngine;
using UnityEngine.Serialization;

public class BulletChain : Bullet
{
    [SerializeField] private int chainCount;
    private int _maxChainCount;
    [SerializeField] private float chainRadius;

    private void Awake()
    {
        Initialize();
        _maxChainCount = chainCount;
    }

    protected override void SetDamage()
    {
        var enemy = target.GetComponentInParent<Enemy>();
        if(enemy) enemy.TakeDamage(firedBy.damage);
        if (chainCount > 0)
        {
            chainCount--;
            FindNewTarget();
            return;
        }
        InvokeOnDestroyBullet();
    }

    protected override void InvokeOnDestroyBullet()
    {
        base.InvokeOnDestroyBullet();
        
        chainCount = _maxChainCount;
    }

    private void FindNewTarget()
    {
        var enemy = FindEnemyInRadius(gameObject.transform.position, chainRadius);
        if (enemy && enemy.gameObject.activeSelf )
        {
            target = enemy.transform;
        }
        else
        {
            InvokeOnDestroyBullet();
        }
    }

    private Enemy FindEnemyInRadius(Vector3 center, float radius)
    {
        Collider2D[] allColliders = Physics2D.OverlapCircleAll(center, radius);
        var targetEnemy = target.GetComponentInParent<Enemy>();
        var targetEnemyID = targetEnemy.GetInstanceID();
        
        for (int i = 0; i < allColliders.Length; i++)
        {
            if (allColliders[i].CompareTag("Enemy"))
            {
                var enemy = allColliders[i].GetComponentInParent<Enemy>();
                if (enemy.GetInstanceID() != targetEnemyID)
                {
                    return enemy;
                }
            }
        }

        return null;
    }
}