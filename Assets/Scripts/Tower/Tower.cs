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
    }

    private void Update()
    {
	    foreach (var weapon in weaponsTowerArray.Weapons)
	    {
		    weapon.target = target;
		    weapon.AddMultiplayer(false);
		    weapon.ShootIfNeed();
	    }
    }

    public void TakeDamage(float aDamage)
	{
		_baseView.TakeDamage(aDamage);
	}

    public void AddWeapon(ITowerWeapon weapon)
    {
	    if (weapon is MonoBehaviour monoBehaviour)
	    {
		    GameObject weaponGameObject = monoBehaviour.gameObject;
		    weaponsTowerArray.CreateWeapon(weaponGameObject.name);
	    }
    }
}
