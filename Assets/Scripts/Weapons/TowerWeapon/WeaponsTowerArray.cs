using System.Collections.Generic;
using UnityEngine;

public class WeaponsTowerArray : MonoBehaviour
{
    [SerializeField] private List<Weapon> WeaponsPrefab;
    [HideInInspector]public List<Weapon> Weapons;

    public void CreateArrowWeapon() 
    {
        Weapons.Add(Instantiate(WeaponsPrefab.Find(weapon => weapon.name == "WeaponTowerArrow"), this.transform));
    }
    public void CreateMultiArrowWeapon() 
    {
        Weapons.Add(Instantiate(WeaponsPrefab.Find(weapon => weapon.name == "WeaponTowerMultiArrow"), this.transform));
    }
    public void CreateCannonballWeapon() 
    {
        Weapons.Add(Instantiate(WeaponsPrefab.Find(weapon => weapon.name == "WeaponTowerСannonball"), this.transform));
    }
    public void CreateCannonballShellBarrageWeapon() 
    {
        Weapons.Add(Instantiate(WeaponsPrefab.Find(weapon => weapon.name == "WeaponTowerСannonballShellBarrage"), this.transform));
    }
}
