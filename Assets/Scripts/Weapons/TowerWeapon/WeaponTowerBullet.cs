using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using Random = UnityEngine.Random;

public class WeaponTowerBullet : ShooterWeapon, ITowerWeapon
{
    public GameObject currentTarget { get; set; }

    private void Awake()
    {
        baseDamage = damage;
        baseAttackRate = attackRate;

        shootElement = GameObject.FindGameObjectWithTag("TowerFirePoint").transform;
        StartCoroutine(onShoot());
    }

    private IEnumerator onShoot()
    {
        while (true)
        {
            UpdateNewTarget();
            yield return new WaitForSeconds(attackRate);
        }
    }

    private void Update()
    {
        if (currentTarget)
        {
            target = currentTarget.GetComponentInParent<Enemy>()?.transform;
            ShootIfNeed();
        }
    }

    public void UpdateNewTarget()
    {
        if (!currentTarget?.GetComponentInParent<Enemy>())
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, attackRange);
            List<GameObject> enemies = new List<GameObject>();

            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("Enemy"))
                {
                    enemies.Add(collider.gameObject);
                }
            }

            if (enemies.Count > 0)
            {
                currentTarget = enemies[Random.Range(0, enemies.Count)];
            }

        }
    }

}

