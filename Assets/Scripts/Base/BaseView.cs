using System.Collections;
using UnityEngine;
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
        CoinManager.Instance.ChangeCoins(500000);
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
        float armor = _myBase.CurrentStats.Armor;
        float rez = 0.95f - 0.95f * Mathf.Exp(-0.01f * armor);
        // float a = 0.95f / (1 - Mathf.Log(1000f));
        // rez = a * (1 - Mathf.Log( armor + 1));
        // //0.95 - 0.95 * Mathf.Exp(-0.015*x)
        // if(armor > 60)
        // {
        //     rez = rez * armor / 60;
        // }
        return aDamage * (1 - rez);
    }

    public float GetCurrentDamageRedaction()
    {
        return 0.95f - 0.95f * Mathf.Exp(-0.01f * _myBase.CurrentStats.Armor);
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

    public BaseStats GetAllBaseStats()
    {
        return _myBase.CurrentStats;
    }

    private void OnDestroy()
    {
        StopCoroutine(RegenerateHealth());
        
        _myBase.OnHealthChanged -= OnHealthChanged;
    }


}
