using UnityEngine;

public class HealthBaseBuff : IBuff
{
    private readonly float _health;

    public HealthBaseBuff(float health)
    {
        _health = health;
    }
    
    public BaseStats ApplyBuff(BaseStats baseStats)
    {
        baseStats.Health = Mathf.Max(baseStats.Health + _health, 0);
        baseStats.MaxHealth = Mathf.Max(baseStats.MaxHealth + _health, 0);
        
        return baseStats;
    }
}
