﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthRegenBaseBuff : MonoBehaviour, IBuff
{
    public float value { get; private set; }
    private readonly List<int> _possibleValue = new List<int>();

    private void Awake()
    {
        Initialize();
        GetComponent<Image>().sprite = Resources.Load<Sprite>(getSkin());
    }

    public void Initialize()
    {
        _possibleValue.Add(5);
        _possibleValue.Add(10);
        _possibleValue.Add(20);
        value = _possibleValue[Random.Range(0, _possibleValue.Count)];
    }

    public BaseStats ApplyBuff(BaseStats baseStats)
    {
        baseStats.HealthRegen += value;
        return baseStats;
    }

    public string getSkin()
    {
        string nameSkin = "Inventory/BaseBuff/Regen_";
        switch (value)
        {
            case 5: nameSkin += "1";
                break;
            case 10: nameSkin += "2";
                break;
            case 20: nameSkin += "3";
                break;
            default:
                nameSkin += "1";
                value = 5;
                break;
        }

        return nameSkin;
    }
}
