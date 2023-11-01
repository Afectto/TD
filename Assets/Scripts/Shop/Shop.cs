using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public GameObject[] slots;
    public GameObject inventory;
    public List<GameObject> BaseBuffPrefab;

    private bool _isInventoryOn;
    private BaseView _baseView;
    
    
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
        
        if(transform.childCount <= 1) return;
        
        var obj = transform.GetChild(1);
        var buffs = transform.GetComponentInChildren<IBuff>();
        _baseView.AddBuff(buffs);
        Destroy(obj.gameObject);
    }
    
}
