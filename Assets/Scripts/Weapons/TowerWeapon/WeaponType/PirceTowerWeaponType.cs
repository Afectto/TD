using System;
using System.Collections;
using UnityEngine.PlayerLoop;

public class PirceTowerWeaponType : WeaponType
{
    private void Awake()
    {
        Initialize();
        PierceWeaponStatsMultiplayer.Instance.MultiplayerChanged += OnMultiplayerChanged;
        var multiInstance = PierceWeaponStatsMultiplayer.Instance;
        Weapon.damage = BaseDamage * multiInstance.GetMultiplayer(MultiplayerType.Damage);
        Weapon.attackRate = BaseAttackRate / multiInstance.GetMultiplayer(MultiplayerType.AttackRate);
    }

    protected override void OnMultiplayerChanged(MultiplayerType type, float mult)
    {
        var multi = PierceWeaponStatsMultiplayer.Instance.GetMultiplayer(type);
        switch (type)
        {
            case MultiplayerType.Damage:
                Weapon.damage = BaseDamage * multi;
                break;
            case MultiplayerType.AttackRate:
                Weapon.attackRate = BaseAttackRate / multi;
                break;
        }
    }
    
    private void OnDestroy()
    {
        PierceWeaponStatsMultiplayer.Instance.MultiplayerChanged -= OnMultiplayerChanged;
    }
}