using System;

public class PierceWeaponStatsMultiplayer : BasedWeaponStatsMultiplayer
{
    public override void IncreasedMultiplayer(MultiplayerType type, float mult)
    {
        switch (type)
        {
            case MultiplayerType.Damage:
                DamageMultiplayer += mult;
                break;
            case MultiplayerType.AttackRate:
                AttackRateMultiplayer += mult;
                break;
            case MultiplayerType.All:
                DamageMultiplayer += mult;
                AttackRateMultiplayer += mult;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
        InvokeChangeMult(type, mult);
    }
}
