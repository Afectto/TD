using System;
using UnityEngine;
using UnityEngine.UI;

public class BaseHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private Image healthBar;
    public float health { get ; set ; }
    public float maxHealth { get; set; }

    private Text _health;

    private void Awake()
    {
        _health = GetComponentInChildren<Text>();
    }

    private void FixedUpdate()
    {
        if (health <= 0)
        {
            //TODO:вывести сообщение о конце игры
        }

        UpdateText();
    }

    public void TakeDamage(float aDamage)
    {
        if (aDamage < 0 && health >= maxHealth)
        {
            health = maxHealth;
        }
        else if (health <= 0 && aDamage > 0)
        {
            health = 0;
        }
        else
        {	
            health -= aDamage;
        }

        if (healthBar) healthBar.fillAmount = health / maxHealth;
    }

    private void UpdateText()
    {
        _health.text = health.ToString() + " / " + maxHealth.ToString();
    }
}
