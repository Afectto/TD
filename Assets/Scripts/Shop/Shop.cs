using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Shop : MonoBehaviour
{
    public GameObject[] slots;
    public GameObject inventory;
    public List<GameObject> BaseBuffPrefab;

    private bool _isInventoryOn;
    private BaseView _baseView;
    
    private int _refreshCount = 1;
    [SerializeField] private Text _refreshValueText;

    private void Start()
    {
        _isInventoryOn = false;
        inventory.SetActive(false);
        
        _baseView = FindObjectOfType<BaseView>();
        
        GenerateBaseBuffRow();
    }
    
    public void Bug()
    {
        _isInventoryOn = !_isInventoryOn;
        inventory.SetActive(_isInventoryOn);
        ChangeBugImage(_isInventoryOn);
    }

    void ChangeBugImage(bool isOpen)
    {
        string imageName = "Inventory/" + (isOpen ? "OpenChest" : "CloseChest");
        Sprite newSprite = Resources.Load<Sprite>(imageName);
        GetComponent<Image>().sprite = newSprite;
    }


    public void GenerateBaseBuffRow()
    {
        for (int i = 0; i < 3; i++)
        {
            var buff = BaseBuffPrefab[Random.Range(0, BaseBuffPrefab.Count)];
            Instantiate(buff, slots[i].transform);
        }
    }

    public void AddBuff(int slotIndex)
    {
        var transform = slots[slotIndex].transform;
        
        if(transform.childCount <= 0) return;
        var obj = transform.GetChild(0);
        var buffs = transform.GetComponentInChildren<IBuff>();
            
        if(CoinManager.Instance.coinCount > buffs.price)
        { 
            _baseView.AddBuff(buffs);
            Destroy(obj.gameObject);
        }
        
    }

    public void UpdateShop()
    {
        var refreshValue = 100 + 50 * _refreshCount;
        if (CoinManager.Instance.coinCount > refreshValue)
        {
            ClearShop();

            GenerateBaseBuffRow();
            _refreshValueText.text = refreshValue.ToString();
            _refreshCount++;
            CoinManager.Instance.ChangeCoins(-refreshValue);
        }
    }

    private void ClearShop()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            var transform = slots[i].transform;
            if(transform.childCount <= 0) return;
            var obj = transform.GetChild(0);
            Destroy(obj.gameObject);
        } 
    }
}
