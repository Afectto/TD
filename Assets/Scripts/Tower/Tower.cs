using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class Tower : MonoBehaviour
{
    [SerializeField, Min(1)]private float _health = 1;
    private BaseHealth _baseHealth;
	private WeaponsTowerArray weaponsTowerArray;
	
	[HideInInspector]public Transform target;

    private void Awake()
    {
	    _baseHealth = FindObjectOfType<BaseHealth>();
	    _baseHealth.health = _baseHealth.maxHealth = _health;
	    weaponsTowerArray = GetComponentInChildren<WeaponsTowerArray>();
	    weaponsTowerArray.CreateArrowWeapon();
    }

    private void Update()
    {
	    foreach (var weapon in weaponsTowerArray.Weapons)
	    {
		    weapon.target = target;
		    weapon.ShootIfNeed();
	    }
    }

    public void TakeDamage(float aDamage)
	{
		_baseHealth.TakeDamage(aDamage);
	}

}
