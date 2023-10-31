public class ArmorBuff : IBuff
{
    private readonly float _value;
        
    public ArmorBuff(float value)
    { 
        _value = value;
    }

    public BaseStats ApplyBuff(BaseStats baseStats)
    {
        baseStats.Armor += _value;
        return baseStats;
    }
}
