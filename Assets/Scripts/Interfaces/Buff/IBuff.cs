public interface IBuff 
{
    float value { get;}

    public void Initialize();
    BaseStats ApplyBuff(BaseStats baseStats);
    public string getSkin();

}

