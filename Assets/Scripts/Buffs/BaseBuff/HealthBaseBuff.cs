using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class HealthBaseBuff : MonoBehaviour, IBuff
{
    public float value { get; private set; }
    private readonly List<int> _possibleValue = new List<int>();
    private readonly List<int> _priceValue = new List<int>();
    private float _price = 500;
    public float price { get => _price; }

    private void Awake()
    {
        Initialize();
        GetComponent<Image>().sprite = Resources.Load<Sprite>(getSkin());
    }

    public void Initialize()
    {
        _possibleValue.Add(500);
        _possibleValue.Add(1000);
        _possibleValue.Add(2000);
        _possibleValue.Add(5000);
        _priceValue.Add(500);
        _priceValue.Add(1000);
        _priceValue.Add(2500);
        _priceValue.Add(5000);
        var index = Random.Range(0, _possibleValue.Count);
        value = _possibleValue[index];
        _price = _priceValue[index];
        GetComponentInChildren<Text>().text = _price.ToString();
    }

    public BaseStats ApplyBuff(BaseStats baseStats)
    {
        baseStats.Health = Mathf.Max(baseStats.Health + value, 0);
        baseStats.MaxHealth = Mathf.Max(baseStats.MaxHealth + value, 0);
        
        return baseStats;
    }

    public string getSkin()
    {
        string nameSkin = "Inventory/BaseBuff/Health_";
        switch (value)
        {
            case 500: nameSkin += "1";
                break;
            case 1000: nameSkin += "2";
                break;
            case 2000: nameSkin += "3";
                break;
            case 5000: nameSkin += "4";
                break;
            default:
            {
                nameSkin += "1";
                value = 5;
            }
                break;
        }

        return nameSkin;
    }
}
