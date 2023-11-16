public interface ITowerWeaponBuff
{
    public float price { get; set; }
    public ITowerWeapon weaponPrefab { get; }
    protected Tower tower { get; set; }
    public void AddWeapon();
}