using System;
using UnityEngine;

public abstract class BasedWeaponStatsMultiplayer : MonoBehaviour
{
    public static BasedWeaponStatsMultiplayer Instance { get; private set; }
    protected float DamageMultiplayer;
    protected float AttackRateMultiplayer;

    public event Action<MultiplayerType, float> MultiplayerChanged;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            AttackRateMultiplayer = DamageMultiplayer  = 1;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public virtual void IncreasedMultiplayer(MultiplayerType type, float mult)
    {
        switch (type)
        {
            case MultiplayerType.Damage:
                DamageMultiplayer *= mult;
                break;
            case MultiplayerType.AttackRate:
                AttackRateMultiplayer *= mult;
                break;
            case MultiplayerType.All:
                DamageMultiplayer *= mult;
                AttackRateMultiplayer *= mult;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }

        InvokeChangeMult(type, mult);
    }

    protected void InvokeChangeMult(MultiplayerType type, float mult)
    {
        MultiplayerChanged?.Invoke(type, mult);
    }
    
    public float GetMultiplayer(MultiplayerType type)
    {
        float value = 0;
        switch (type)
        {
            case MultiplayerType.Damage:
                value = DamageMultiplayer;
                break;
            case MultiplayerType.AttackRate:
                value = AttackRateMultiplayer;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
        return value;
    }

}