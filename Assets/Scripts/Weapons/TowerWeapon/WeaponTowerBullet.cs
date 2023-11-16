using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class WeaponTowerBullet : ShooterWeapon, ITowerWeapon
{
    public GameObject currentTarget { get; set; }
    public Queue<GameObject> allTarget { get; set; }

    private void Awake()
    {
        CircleCollider2D = GetComponentInChildren<CircleCollider2D>();
        CircleCollider2D.radius = attackRange;
        CircleCollider2D.isTrigger = true;
        
        allTarget = new Queue<GameObject>();
        
        shootElement = GameObject.FindGameObjectWithTag("TowerFirePoint").transform;
    }

    private void Update()
    {
        UpdateNewTarget();
        if (currentTarget)
        {
            target = currentTarget.GetComponentInParent<Enemy>().transform;
            ShootIfNeed();
        }
    }

    public void UpdateNewTarget()
    {
        if (!currentTarget && allTarget.Count > 0)
        {
            currentTarget = allTarget.Dequeue();
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            allTarget.Enqueue(collision.gameObject);
        }
    }
}