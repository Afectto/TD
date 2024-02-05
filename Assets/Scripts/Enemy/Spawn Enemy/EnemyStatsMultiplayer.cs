using System;

public enum MultiplayerType
{
    All,
    Health,
    Damage,
    AttackRate,
    Reward,
    SpawnTime
}

public static class EnemyStatsMultiplayer
{
    private static float _healthMultiplayer;
    private static float _damageMultiplayer;
    private static float _attackRateMultiplayer;
    private static float _rewardMultiplayer;
    private static float _spawnTimeMultiplayer;

    static EnemyStatsMultiplayer()
    {
        _attackRateMultiplayer = _damageMultiplayer = _healthMultiplayer = _rewardMultiplayer = _spawnTimeMultiplayer = 1;
    }

    public static void IncreasedMultiplayer(MultiplayerType type, float mult)
    {
        switch (type)
        {
            case MultiplayerType.Health:
                _healthMultiplayer *= mult;
                break;
            case MultiplayerType.Damage:
                _damageMultiplayer *= mult;
                break;
            case MultiplayerType.Reward:
                _rewardMultiplayer *= mult;
                break;
            case MultiplayerType.AttackRate:
                _attackRateMultiplayer *= mult;
                break;
            case MultiplayerType.SpawnTime:
                _spawnTimeMultiplayer *= mult;
                break;
            case MultiplayerType.All:
                _healthMultiplayer *= mult;
                _damageMultiplayer *= mult;
                _attackRateMultiplayer *= mult;
                _rewardMultiplayer *= mult;
                _spawnTimeMultiplayer *= mult;
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
            case MultiplayerType.Health:
                value = _healthMultiplayer;
                break;
            case MultiplayerType.Damage:
                value = _damageMultiplayer;
                break;
            case MultiplayerType.AttackRate:
                value = _attackRateMultiplayer;
                break;
            case MultiplayerType.Reward:
                value = _rewardMultiplayer;
                break;
            case MultiplayerType.SpawnTime:
                value = _spawnTimeMultiplayer;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
        return value;
    }

}