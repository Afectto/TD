using System.Collections.Generic;
using System.Linq;

public class Base : IBuffable, IDamageable
{
    public BaseStats BaseStats { get; }
    public BaseStats CurrentStats { get; private set; }
    
    private readonly List<IBuff> _buffs = new List<IBuff>();
    
    private readonly List<IBuff> appliedBuffs = new List<IBuff>();

    public Base(BaseStats baseStats)
    {
        BaseStats = baseStats;
        CurrentStats = baseStats;
    }
    
    public void AddBuff(IBuff buff)
    {
        _buffs.Add(buff);
        ApplyBuffs();
    }

    private void ApplyBuffs()
    {
        foreach (var buff in _buffs.Where(buff => !appliedBuffs.Contains(buff)))
        {
            CurrentStats = buff.ApplyBuff(CurrentStats);
            appliedBuffs.Add(buff);
        }
    }

    public float health
    {
        get => CurrentStats.Health;
        set =>
            CurrentStats = new BaseStats
            {
                Health = value,
                Armor = CurrentStats.Armor,
                MaxHealth = CurrentStats.MaxHealth,
                HealthRegen = CurrentStats.HealthRegen,
                Income = CurrentStats.Income
            };
    }

    public float maxHealth
    {
        get => CurrentStats.MaxHealth;
        set =>
            CurrentStats = new BaseStats
            {
                Health = CurrentStats.Health,
                Armor = CurrentStats.Armor,
                MaxHealth = value,
                HealthRegen = CurrentStats.HealthRegen,
                Income = CurrentStats.Income
            };
    }

    public void TakeDamage(float aDamage)
    {
        if (aDamage < 0 && health >= maxHealth)
        {
            health = maxHealth;
        }
        else if (health <= 0 && aDamage > 0)
        {
            health = 0;
        }
        else
        {	
            health -= aDamage;
        }
    }
}