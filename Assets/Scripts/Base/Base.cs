using System;
using System.Collections.Generic;
public interface ITimeBasedIncome
{
    float IncomePerSecond { get; } 

}
public class Base : IBuffable, IDamageable, ITimeBasedIncome
{
    public BaseStats BaseStats { get; }
    public BaseStats CurrentStats { get; private set; }

    private readonly List<IBuff> _buffs = new List<IBuff>();

    private readonly List<IBuff> appliedBuffs = new List<IBuff>();
    public event Action<float> OnHealthChanged;
    public float IncomePerSecond { get => CurrentStats.Income; }
    
    public Base(BaseStats baseStats)
    {
        BaseStats = baseStats;
        CurrentStats = baseStats;
    }

    public void AddBuff(IBuff buff)
    {
        CoinManager.Instance.ChangeCoins(-buff.price);
        _buffs.Add(buff);
        ApplyBuffs();
    }

    private void ApplyBuffs()
    {
        for (int i = appliedBuffs.Count; i < _buffs.Count; i++)
        {
            CurrentStats = _buffs[i].ApplyBuff(CurrentStats);
            appliedBuffs.Add(_buffs[i]);
        }
    }

    public float health
    {
        get => CurrentStats.Health;
        set
        {
            CurrentStats = new BaseStats
            {
                Health = value,
                Armor = CurrentStats.Armor,
                MaxHealth = CurrentStats.MaxHealth,
                HealthRegen = CurrentStats.HealthRegen,
                Income = CurrentStats.Income
            };
            OnHealthChanged?.Invoke(value);
        }

    }

    public float maxHealth
    {
        get => CurrentStats.MaxHealth;
        set
        {
            CurrentStats = new BaseStats
            {
                Health = CurrentStats.Health,
                Armor = CurrentStats.Armor,
                MaxHealth = value,
                HealthRegen = CurrentStats.HealthRegen,
                Income = CurrentStats.Income
            };
            OnHealthChanged?. Invoke(value);
        }
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