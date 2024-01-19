using System.Collections;
using UnityEngine;

public abstract class ShooterWeapon : Weapon, IShooter
{
    public Transform _shootElement;
    public GameObject _bulletPrefab;
    public Transform shootElement { get => _shootElement; set => _shootElement = value; }
    public GameObject bullet { get => _bulletPrefab; set => _bulletPrefab = value; }

    private GameObjectPool _pool;

    public override IEnumerator Attack()
    {
        _isShoot = true;
        yield return new WaitForSeconds(attackRate);
        Shoot();

        _isShoot = false;
    }

    protected void Shoot()
    {
        if (target)
        {
            FindBulletPoolIfNeeded();
            
            CreateBullet();

        }
    }

    void FindBulletPoolIfNeeded()
    {
        if (_pool == null)
        {
            var bulletObjectPool = FindObjectsByType<BulletObjectPool>(FindObjectsSortMode.None);
            for (int i = 0; i < bulletObjectPool.Length; i++)
            {
                if (bulletObjectPool[i].bulletPrefab == bullet)
                {
                    _pool = bulletObjectPool[i].bulletObjectPool;
                }
            }
        } 
    }

    void CreateBullet()
    {
        var mBullet = _pool.Get();
        var bulletController = mBullet.GetComponent<Bullet>();
        bulletController.target = target;
        bulletController.firedBy = this;
        bulletController.transform.position = shootElement.position;
                    
        void OnDestroyAction(GameObject thisBullet)
        {
            _pool.Return(thisBullet);
            Bullet.RemoveOnDestroyAction(thisBullet, OnDestroyAction);
        }

        Bullet.AddOnDestroyAction(mBullet, OnDestroyAction);
    }
    
}
