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

    protected override void setTargetDamage(Collider2D collision)
    {
        if(!collision.CompareTag("Enemy")) return;
        if(!target) return;
		
        var enemy = target.GetComponentInParent<Enemy>();
        if(enemy) enemy.TakeDamage(firedBy.damage);
        Debug.Log($"Chain count  = {chainCount}");
        if (chainCount > 0)
        {
            chainCount--;
            FindNewTarget();
            return;
        }
        Debug.Log("CHAIN > 0");
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
        for (int i = 0; i < allColliders.Length; i++)
        {
            if (allColliders[i].CompareTag("Enemy"))
            {
                var enemy = allColliders[i].GetComponentInParent<Enemy>();
                if (enemy != target.GetComponentInParent<Enemy>())
                {
                    return enemy;
                }
            }
        }

        return null;
    }
}