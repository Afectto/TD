using UnityEngine;

public class BulletArrowArcher : Bullet
{
    private void Awake()
    {
        Initialize();
    }
    
    protected override void setTargetDamage(Collider2D collision)
    {
        if (!collision.CompareTag("Tower")) return;
        if(!target) return;
		
        var tower = target.GetComponentInParent<Tower>();
        if(tower) tower.TakeDamage(firedBy.damage);
        Destroy(gameObject);
    }
}
