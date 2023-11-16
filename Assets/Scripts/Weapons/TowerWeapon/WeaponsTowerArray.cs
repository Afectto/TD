using System.Collections.Generic;
using UnityEngine;

public class WeaponsTowerArray : MonoBehaviour
{
    [SerializeField] private List<Weapon> WeaponsPrefab;
    [HideInInspector]public List<Weapon> Weapons;

    public void CreateWeapon(string name)
    {
        Weapons.Add(Instantiate(WeaponsPrefab.Find(weapon => weapon.name == name), this.transform));
    }
}
