public class IncomeBuff : IBuff
{
    private readonly float _value;
        
    public IncomeBuff(float value)
    { 
        _value = value;
    }

    public BaseStats ApplyBuff(BaseStats baseStats)
    {
        baseStats.Income += _value;
        return baseStats;
    }
}