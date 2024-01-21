using UnityEngine;

public class BulletChain : Bullet
{
    [SerializeField] private int chainCount;
    private int _maxChainCount;
    [SerializeField] private float chainRadius;
    private CircleCollider2D _chainCollider;

    private void Awake()
    {
        Initialize();
        _chainCollider = GetComponentInChildren<CircleCollider2D>();
        _chainCollider.radius = chainRadius;
        _chainCollider.isTrigger = true;
        _maxChainCount = chainCount;
    }

    protected override void setTargetDamage(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy")) return;
        if(!target) return;
		
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
        var trigger = _chainCollider.GetComponent<ChainTriger>();
        trigger.ClearEnemies();
        
        base.InvokeOnDestroyBullet();
        
        chainCount = _maxChainCount;
    }

    private void FindNewTarget()
    {
        var trigger = _chainCollider.GetComponent<ChainTriger>();
        var enemy = trigger.GetRandomEnemyInRadius();
        if (enemy && enemy.activeSelf )
        {
            target = enemy.transform;
        }
    }
}