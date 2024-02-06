using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(CircleCollider2D))]
public class WeaponTowerBullet : ShooterWeapon, ITowerWeapon
{
    public GameObject currentTarget { get; set; }
    public List<GameObject> allTarget { get; set; }

    private void Awake()
    {
        baseDamage = damage;
        baseAttackRate = attackRate;
        CircleCollider2D = GetComponentInChildren<CircleCollider2D>();
        CircleCollider2D.radius = attackRange;
        CircleCollider2D.isTrigger = true;
        
        allTarget = new List<GameObject>();
        
        shootElement = GameObject.FindGameObjectWithTag("TowerFirePoint").transform;
    }

    private void Update()
    {
        UpdateNewTarget();
        if (currentTarget)
        {
            target = currentTarget.GetComponentInParent<Enemy>()?.transform;
            ShootIfNeed();
        }
    }

    public void UpdateNewTarget()
    {
        if (!currentTarget?.GetComponentInParent<Enemy>() && allTarget.Count > 0)
        {
            currentTarget = allTarget[Random.Range(0, allTarget.Count)];
            allTarget.Remove(currentTarget);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            allTarget.Add(collision.gameObject);
        }
    }
    
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            allTarget.Remove(collision.gameObject);
        }
    }
}