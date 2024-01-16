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

    public float speed {  get => _speed; set => _speed = value; }
    public bool isNeedMove;
    
    protected Transform _target;
    private Rigidbody2D _rigidbody2D;
    [HideInInspector]public Animator _animation;
    
    public float rewardValue;
    
    public delegate void OnDestroyAction(GameObject enemy);
    public static event OnDestroyAction IsOnDestroy;
    
    protected void Initialize()
    {
        maxHealth = health;
        healthBar.fillAmount = 1;
        _target = FindObjectOfType<Tower>().transform;
        _rigidbody2D = GetComponent<Rigidbody2D>();
        isNeedMove = true;
        _animation = GetComponent<Animator>();
        _weapon = GetComponent<Weapon>();
    }
    
   protected void OnUpdate()
    {
        if (health <= 0)
        {
            CoinManager.Instance.ChangeCoins(rewardValue);
            IsOnDestroy?.Invoke(gameObject);
            health = maxHealth;
            Initialize();
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
    
}
