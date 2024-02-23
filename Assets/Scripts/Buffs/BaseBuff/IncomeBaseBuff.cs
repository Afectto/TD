using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IncomeBaseBuff : MonoBehaviour, IBuff
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
        _possibleValue.Add(750);
        _possibleValue.Add(1000);
        _possibleValue.Add(1500);
        _priceValue.Add(500);
        _priceValue.Add(1000);
        _priceValue.Add(2000);
        _priceValue.Add(5000);
        var index = Random.Range(0, _possibleValue.Count);
        value = _possibleValue[index];
        _price = _priceValue[index];
        GetComponentInChildren<Text>().text = _price.ToString();
    }

    public BaseStats ApplyBuff(BaseStats baseStats)
    {
        baseStats.Income += value/60;
        return baseStats;
    }

    public string getSkin()
    {
        string nameSkin = "Inventory/BaseBuff/Coin_";
        switch (value)
        {
            case 500: nameSkin += "1";
                break;
            case 750: nameSkin += "2";
                break;
            case 1000: nameSkin += "3";
                break;
            case 1500: nameSkin += "4";
                break;
            default:
                nameSkin += "1";
                value = 5;
                break;
        }

        return nameSkin;
    }
}