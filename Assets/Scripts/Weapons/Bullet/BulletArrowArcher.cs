using UnityEngine;

public class BulletArrowArcher : Bullet
{
    private void Awake()
    {
        Initialize();
    }
    
    protected override void SetDamage()
    {
        var tower = target.GetComponentInParent<Tower>();
        if(tower) tower.TakeDamage(firedBy.damage);
        InvokeOnDestroyBullet();
    }
}
