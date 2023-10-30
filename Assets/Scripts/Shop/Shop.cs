using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public GameObject[] slots;
    public GameObject inventory;

    private bool _isInventoryOn;
    [SerializeField] private GameObject slotPrefab;

    private void Start()
    {
        _isInventoryOn = false;
        inventory.SetActive(false);

        for (var i = 0; i < slots.Length; i++)
        {
            // slots[i] = GameObject.Instantiate(slotPrefab, Vector3.zero, Quaternion.identity);
            // slots[i].transform.SetParent(inventory.transform);
            //
            // Vector3 offsetX = inventory.transform.position;
            // offsetX.x -= i * 64;
            // slots[i].transform.position = offsetX;
            // slots[i].GetComponent<Slot>().SetIndex(i);
        }
    }


    public void Bug()
    {
        _isInventoryOn = !_isInventoryOn;
        inventory.SetActive(_isInventoryOn);
        ChangeBugImage(_isInventoryOn);
    }

    void ChangeBugImage(bool isOpen)
    {
        string imageName = isOpen ? "OpenChest" : "CloseChest";
        Sprite newSprite = Resources.Load<Sprite>(imageName);
        Image image = GetComponent<Image>();
        image.sprite = newSprite;
    }
    
}
