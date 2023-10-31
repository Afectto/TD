using System.Collections;
using UnityEngine;

public class BaseView : MonoBehaviour
{
    [SerializeField, Min(1)]private float _health = 1;
    private Base _myBase;
    private BaseHealth _baseHealth;
    private void Awake()
    {
        var stats = new BaseStats
        {
            Health = _health,
            MaxHealth = _health,
            HealthRegen = 0,
            Armor = 0,
            Income = 50
        };
        
        Init(new Base(stats));
        _myBase.OnHealthChanged += OnHealthChanged;
        _baseHealth = FindObjectOfType<BaseHealth>();
        
        _baseHealth.health = _baseHealth.maxHealth = _health;
    }
    
    private void OnHealthChanged(float newHealth)
    {
        _baseHealth.UpdateHealth(_myBase.CurrentStats);
    }
    
    public void Init(Base myBase)
    {
        _myBase = myBase;
        StartCoroutine(RegenerateHealth());
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

    private void Update()
    {
        // if (Input.GetKeyDown(KeyCode.R))
        // {
        //     _myBase.AddBuff(new HealthBaseBuff(-50));
        //     _baseHealth.UpdateHealth(_myBase.CurrentStats);
        // }
        // if (Input.GetKeyDown(KeyCode.H))
        // {
        //     _myBase.AddBuff(new HealthRegenerateBaseBuff(20));
        // }
        //
        // if (Input.GetKeyDown(KeyCode.J))
        // {
        //     _myBase.AddBuff(new HealthRegenerateBaseBuff(-50));
        // }
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

    private void OnDestroy()
    {
        StopCoroutine(RegenerateHealth());
    }

}
