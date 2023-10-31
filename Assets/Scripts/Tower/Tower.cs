using UnityEngine;

public class Tower : MonoBehaviour
{
	private BaseView _baseView;
	private WeaponsTowerArray weaponsTowerArray;
	
	[HideInInspector]public Transform target;

    private void Awake()
    {
	    _baseView = FindObjectOfType<BaseView>();
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
		_baseView.TakeDamage(aDamage);
	}

}
