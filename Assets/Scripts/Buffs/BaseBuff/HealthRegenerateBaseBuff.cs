public class HealthRegenerateBaseBuff : IBuff
{
    private readonly float _value;
        
    public HealthRegenerateBaseBuff(float value)
    { 
        _value = value;
    }
        
    public BaseStats ApplyBuff(BaseStats baseStats)
    {
        baseStats.HealthRegen += _value;
        return baseStats;
    } 
}
