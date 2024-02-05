using System;
using UnityEngine;

public abstract class BasedWeaponStatsMultiplayer : MonoBehaviour
{
    public static BasedWeaponStatsMultiplayer Instance { get; private set; }
    private float _damageMultiplayer;
    private float _attackRateMultiplayer;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            _attackRateMultiplayer = _damageMultiplayer  = 1;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void IncreasedMultiplayer(MultiplayerType type, float mult)
    {
        switch (type)
        {
            case MultiplayerType.Damage:
                _damageMultiplayer *= mult;
                break;
            case MultiplayerType.AttackRate:
                _attackRateMultiplayer *= mult;
                break;
            case MultiplayerType.All:
                _damageMultiplayer *= mult;
                _attackRateMultiplayer *= mult;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
    }

    public float GetMultiplayer(MultiplayerType type)
    {
        float value = 0;
        switch (type)
        {
            case MultiplayerType.Damage:
                value = _damageMultiplayer;
                break;
            case MultiplayerType.AttackRate:
                value = _attackRateMultiplayer;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
        return value;
    }

}