using UnityEngine;

public abstract class WeaponType : MonoBehaviour
{
    private protected Weapon Weapon;
    private protected float BaseDamage;
    private protected float BaseAttackRate;
    
    protected void Initialize()
    {
        Weapon = GetComponent<Weapon>();
        BaseDamage = Weapon.GetWeaponBaseDamage();
        BaseAttackRate = Weapon.GetWeaponBaseAttackRate();
    }

    protected abstract void OnMultiplayerChanged(MultiplayerType type, float mult);
    
}