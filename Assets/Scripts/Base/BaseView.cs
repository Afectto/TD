using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class BaseView : MonoBehaviour
{
    [SerializeField, Min(1)]private float _health = 1;
    private Base _myBase;
    private BaseHealth _baseHealth;
    [SerializeField]private Text timerText; 
    private Timer _timer;
    
    [SerializeField] private Shop _shop;
    public void Init(Base myBase)
    {
        _myBase = myBase;
        StartCoroutine(RegenerateHealth());
        
        StartCoroutine(Income());
        CoinManager.Instance.ChangeCoins(2000);
    }

    private void Awake()
    {
        var stats = new BaseStats
        {
            Health = _health,
            MaxHealth = _health,
            HealthRegen = 0,
            Armor = 0,
            Income = 1000f/60f
        };
        
        Init(new Base(stats));
        _myBase.OnHealthChanged += OnHealthChanged;
        _baseHealth = FindObjectOfType<BaseHealth>();
        
        _baseHealth.health = _baseHealth.maxHealth = _health;
        
        _timer = new Timer();
        _timer.Start();
        
    }
    
    private void Update()
    {
        _timer.Update();
        timerText.text = _timer.GetTimeFormatted();
    }

    private void OnHealthChanged(float newHealth)
    {
        _baseHealth.UpdateHealth(_myBase.CurrentStats);
    }
    
    public void TakeDamage(float aDamage)
    {
        _myBase.TakeDamage(CalculateDamageRedaction(aDamage));
    }

    private float CalculateDamageRedaction(float aDamage)
    {
        var armor = _myBase.CurrentStats.Armor;
        float rez = 1;
        float a = 0.95f / (1 - Mathf.Log(1000f));
        rez = a * (1 - Mathf.Log( armor + 1));
        
        if(armor > 60)
        {
            rez = rez * armor / 60;
        }

        return aDamage * (1 - Mathf.Clamp(rez, 0f, 0.95f));
    }
    
    private IEnumerator RegenerateHealth()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.2f);
            var newHealth = _myBase.health + _myBase.CurrentStats.HealthRegen / 5;
            _myBase.health = Mathf.Clamp(newHealth, 0, _myBase.maxHealth);
        }
        // ReSharper disable once IteratorNeverReturns
    }

    public void AddBuff(IBuff buff)
    {
        _myBase.AddBuff(buff);
    }

    
    private IEnumerator Income()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            
            CoinManager.Instance.ChangeCoins(_myBase.IncomePerSecond);
        }
        // ReSharper disable once IteratorNeverReturns
    }

    private void OnDestroy()
    {
        StopCoroutine(RegenerateHealth());
    }


}
