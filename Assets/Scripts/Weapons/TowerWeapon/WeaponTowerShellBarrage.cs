using System.Collections;
using UnityEngine;

public class WeaponTowerShellBarrage : WeaponTowerBullet
{
    public int barrageCount = 3;
    public float delay = 0.3f;

    public override IEnumerator Attack()
    {
        _isShoot = true;
        yield return new WaitForSeconds(attackRate);
        
        if (target)
        {
            for (var i = 0; i < barrageCount; i++)
            {
                yield return new WaitForSeconds(delay);
                var mBullet = GameObject.Instantiate(bullet, shootElement.position, Quaternion.identity) as GameObject;
                bulletController = mBullet.GetComponent<Bullet>();
                bulletController.target = target;
                bulletController.firedBy = this;
            }
        }
        
        _isShoot = false;
    }
}
