using System;
using System.Collections;
using UnityEngine.PlayerLoop;

public class PirceTowerWeaponType : WeaponType
{
    private void Awake()
    {
        Initialize();
        PierceWeaponStatsMultiplayer.Instance.MultiplayerChanged += OnMultiplayerChanged;
    }

    protected override void OnMultiplayerChanged(MultiplayerType type, float mult)
    {
        switch (type)
        {
            case MultiplayerType.Damage:
                Weapon.damage = BaseDamage * PierceWeaponStatsMultiplayer.Instance.GetMultiplayer(MultiplayerType.Damage);
                break;
            case MultiplayerType.AttackRate:
                Weapon.attackRate = BaseAttackRate / PierceWeaponStatsMultiplayer.Instance.GetMultiplayer(MultiplayerType.AttackRate);
                break;
        }
    }
    
    private void OnDestroy()
    {
        PierceWeaponStatsMultiplayer.Instance.MultiplayerChanged -= OnMultiplayerChanged;
    }
}