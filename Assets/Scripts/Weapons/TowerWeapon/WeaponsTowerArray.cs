using System;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsTowerArray : MonoBehaviour
{
    private List<Weapon> WeaponsPrefab;
    [HideInInspector]public List<Weapon> Weapons;

    private void Start()
    {
        WeaponsPrefab = new List<Weapon>();
        Weapon[] weaponsPrefabs = Resources.LoadAll<Weapon>("Prefabs/TowerPrefabs/TowerWeaponPrefab");
        foreach (var weapon in weaponsPrefabs)
        { 
            WeaponsPrefab.Add(weapon);
        }
    }

    public void CreateWeapon(string name)
    {
        Weapons.Add(Instantiate(WeaponsPrefab.Find(weapon => weapon.name == name), this.transform));
    }
    
    public void CreateArrowWeapon() 
    {
        Weapons.Add(Instantiate(WeaponsPrefab.Find(weapon => weapon.name == "WeaponTowerArrowShellBarrage"), this.transform));
        Weapons.Add(Instantiate(WeaponsPrefab.Find(weapon => weapon.name == "WeaponTowerArrow"), this.transform));
        Weapons.Add(Instantiate(WeaponsPrefab.Find(weapon => weapon.name == "WeaponTowerChainArrow"), this.transform));
        Weapons.Add(Instantiate(WeaponsPrefab.Find(weapon => weapon.name == "WeaponTowerMultiArrow"), this.transform));
        Weapons.Add(Instantiate(WeaponsPrefab.Find(weapon => weapon.name == "WeaponTowerMultiArrowShellBarrage"), this.transform));
    }
}
