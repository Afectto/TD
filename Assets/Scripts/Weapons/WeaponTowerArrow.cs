using UnityEngine;

public class WeaponTowerArrow : ShooterWeapon
{
    private void Awake()
    {
        shootElement = GameObject.FindGameObjectWithTag("TowerFirePoint").transform;
    }
}
