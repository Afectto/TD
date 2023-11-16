using UnityEngine;
using UnityEngine.UI;

public class WeaponBuff : MonoBehaviour, ITowerWeaponBuff
{
    [SerializeField]private Tower _tower;
    [SerializeField]private WeaponTowerBullet _weaponPrefab;
    [SerializeField]private float _price;
    public float price { get => _price; set => _price = value; }
    public ITowerWeapon weaponPrefab => _weaponPrefab;

    Tower ITowerWeaponBuff.tower
    {
        get => _tower;
        set => _tower = value;
    }

    private void Awake()
    {
        _tower = GameObject.FindWithTag("Tower").GetComponentInParent<Tower>();
        
        GetComponentInChildren<Text>().text = _price.ToString();
    }

    public void AddWeapon()
    {
        CoinManager.Instance.ChangeCoins(-price);
        _tower.AddWeapon(weaponPrefab);
    }
}
