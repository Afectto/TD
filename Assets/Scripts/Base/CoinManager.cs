using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    private float _coinCount;
    public Text coinCountText;

    public static CoinManager Instance { get; private set; }

    public float coinCount => _coinCount;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void ChangeCoins(float amount)
    {
        _coinCount += amount;
        coinCountText.text = Mathf.FloorToInt(_coinCount).ToString();
    }
}
