using System.Collections;
using UnityEngine;

public abstract class ShooterWeapon : Weapon, IShooter
{
    public Transform _shootElement;
    public GameObject _bulletPrefab;
    public Transform shootElement { get => _shootElement; set => _shootElement = value; }
    public GameObject bullet { get => _bulletPrefab; set => _bulletPrefab = value; }

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
            var BulletObjectPool = FindObjectsByType<BulletObjectPool>(FindObjectsSortMode.None);
            for (int i = 0; i < BulletObjectPool.Length; i++)
            {
                if (BulletObjectPool[i].bulletPrefab == bullet)
                {
                    var pool = BulletObjectPool[i].bulletObjectPool;
                    var mBullet = pool.Get();
                    var bulletController = mBullet.GetComponent<Bullet>();
                    bulletController.target = target;
                    bulletController.firedBy = this;
                    bulletController.transform.position = shootElement.position;
                    
                    void ONDestroyAction(GameObject thisBullet)
                    {
                        if (thisBullet == mBullet)
                        {
                            pool.Return(thisBullet);
                            Bullet.IsOnDestroy -= ONDestroyAction;
                        }
                    }

                    Bullet.IsOnDestroy += ONDestroyAction;
                }
            }
        }
    }
}
