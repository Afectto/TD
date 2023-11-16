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

    protected CircleCollider2D CircleCollider2D;
    public float attackRange;


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

    public virtual IEnumerator Attack()
    {
        _isShoot = true;
        yield return new WaitForSeconds(attackRate);
        _isShoot = false;
    }
}
