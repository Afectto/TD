using System;
using System.Collections;
using UnityEngine;

public class PirceTowerWeapon : WeaponTowerBullet
{
    private void OnEnable()
    {
        StartCoroutine(UpdateMultiplayer());
    }

    private IEnumerator UpdateMultiplayer()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            damage = baseDamage * PierceWeaponStatsMultiplayer.Instance.GetMultiplayer(MultiplayerType.Damage);
            attackRate = baseAttackRate / PierceWeaponStatsMultiplayer.Instance.GetMultiplayer(MultiplayerType.AttackRate);
        }
        // ReSharper disable once IteratorNeverReturns
    }
}