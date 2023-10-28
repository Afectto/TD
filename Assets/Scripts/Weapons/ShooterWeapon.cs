using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public abstract class ShooterWeapon : Weapon, IShooter
{
    protected Bullet bulletController;
    
    public override IEnumerator Attack()
    {
        _isShoot = true;
        
        yield return new WaitForSeconds(attackRate);
        var mBullet = GameObject.Instantiate(bullet, shootElement.position, Quaternion.identity) as GameObject;
        bulletController = mBullet.GetComponent<Bullet>();
        bulletController.target = target;
        bulletController.firedBy = this;
        _isShoot = false;
    }

    public Transform _shootElement;
    public GameObject _bulletPrefab;
    public Transform shootElement { get => _shootElement; set => _shootElement = value; }
    public GameObject bullet { get => _bulletPrefab; set => _bulletPrefab = value; }
}
