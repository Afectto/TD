public interface IBuff 
{
    float value { get;}
    float price { get; }

    public void Initialize();
    BaseStats ApplyBuff(BaseStats baseStats);
    public string getSkin();

}

