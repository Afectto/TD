using System.Collections;
using UnityEngine;

public abstract class Weapon : MonoBehaviour, IAttacker
{
    [SerializeField, Min(1)]private float _damage;
    [SerializeField, Min(0)] private float _attackRate;
    public float damage { get => _damage; set => _damage = value; }
    public float attackRate { get => _attackRate; set => _attackRate = value; }
    
    [HideInInspector]public Transform target;
    public bool _isShoot;
	
    public void ShootIfNeed()
    {
        if(target)
        { 
            if (!_isShoot)
            {
                StartCoroutine(Attack());
            }
        }
    }
    
    public abstract IEnumerator Attack();
}
