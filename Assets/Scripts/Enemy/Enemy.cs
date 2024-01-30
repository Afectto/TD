using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Enemy : MonoBehaviour, IDamageable, IMovable
{
    protected Weapon _weapon;
    [SerializeField, Min(1)]private float _health = 1;
    [SerializeField, Min(1)]private float _speed = 1;
    [SerializeField] private Image healthBar;
    
    public float health { get => _health; set => _health = value; }
    public float maxHealth { get; set; }
    public float baseMaxHealth { get; set; }

    public float speed {  get => _speed; set => _speed = value; }
    public bool isNeedMove;
    
    protected Transform _target;
    private Rigidbody2D _rigidbody2D;
    [HideInInspector]public Animator _animation;
    
    public float rewardValue;
    private float baseRewardValue;

    private static readonly Dictionary<GameObject, Action<GameObject>> OnDestroyActions = new Dictionary<GameObject, Action<GameObject>>();

    public static void AddOnDestroyAction(GameObject enemy, Action<GameObject> action)
    {
        if (!OnDestroyActions.ContainsKey(enemy))
        {
            OnDestroyActions.Add(enemy, null);
        }
        OnDestroyActions[enemy] += action;
    }

    public static void RemoveOnDestroyAction(GameObject enemy, Action<GameObject> action)
    {
        if (OnDestroyActions.ContainsKey(enemy))
        {
            OnDestroyActions[enemy] -= action;
            if (OnDestroyActions[enemy] == null)
            {
                OnDestroyActions.Remove(enemy);
            }
        }
    }
    
    protected void Initialize()
    {
        baseMaxHealth = health;
        baseRewardValue = rewardValue;
        maxHealth = health;
        healthBar.fillAmount = 1;
        _target = FindObjectOfType<Tower>().transform;
        _rigidbody2D = GetComponent<Rigidbody2D>();
        isNeedMove = true;
        _animation = GetComponentInChildren<Animator>();
        _weapon = GetComponent<Weapon>();
    }
    
   protected void OnUpdate()
    {
        if (health <= 0)
        {
            CoinManager.Instance.ChangeCoins(rewardValue);
            
            if (OnDestroyActions.ContainsKey(gameObject))
            {
                OnDestroyActions[gameObject]?.Invoke(gameObject);
                OnDestroyActions.Remove(gameObject);
            }
        }
        
        if(_target)
        { 
            Move();
        }
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


    public void Move()
    {
        if(isNeedMove && gameObject.activeSelf)
        {
            Vector2 direction = _target.position - transform.position;
            direction.Normalize();
            _rigidbody2D.velocity = direction * speed;
            transform.localScale = new Vector3(direction.x > 0 ? -1 : 1, 1, 1);
            
            _animation.Play("walk");
        }
        else
        {
            _rigidbody2D.velocity = Vector2.zero;
            // _animation.Play("idle");
        }
    }

    private void OnEnable()
    {
        health = baseMaxHealth * EnemyStatsMultiplayer.GetMultiplayer(MultiplayerType.Health);
        maxHealth = health;
        rewardValue = baseRewardValue * EnemyStatsMultiplayer.GetMultiplayer(MultiplayerType.Reward);
        _weapon.AddMultiplayer(true);
    }

    private void OnDisable()
    {
        health = maxHealth;
        healthBar.fillAmount = 1;
        isNeedMove = true;
    }
}
