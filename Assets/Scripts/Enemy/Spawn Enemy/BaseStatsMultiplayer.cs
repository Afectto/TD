using System;

public static class BaseStatsMultiplayer
{
    private static float _damageMultiplayer;
    private static float _attackRateMultiplayer;

    static BaseStatsMultiplayer()
    {
        _attackRateMultiplayer = _damageMultiplayer  = 1;
    }

    public static void IncreasedMultiplayer(MultiplayerType type, float mult)
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

    public static float GetMultiplayer(MultiplayerType type)
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