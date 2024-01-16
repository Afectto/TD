using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
	public float speed;
	[HideInInspector]public Transform target;
	private Vector3 lastEnemyPosition;
	
	public IAttacker firedBy;
	private float distance;
	
	public delegate void OnDestroyAction(GameObject enemy);
	public static event OnDestroyAction IsOnDestroy;
	
	public void Initialize()
	{
		lastEnemyPosition = Vector3.zero;
	}

	public void Update()
	{
		if (!target.gameObject.activeSelf)
		{
			transform.position = Vector3.MoveTowards(transform.position, lastEnemyPosition, Time.deltaTime * speed);
			if (transform.position == lastEnemyPosition)
			{
				IsOnDestroy?.Invoke(gameObject);
			}
			return;
		}
		
		var targetPosition = target.position;
		var transformPosition = transform.position;


		transformPosition = Vector3.MoveTowards(transformPosition, targetPosition, Time.deltaTime * speed);
		transform.position = transformPosition;
		lastEnemyPosition = targetPosition;
		
		
		var diference = target.transform.position - transformPosition;
		var rotationZ = Mathf.Atan2(diference.y, diference.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		setTargetDamage(collision);
	}

	protected virtual void setTargetDamage(Collider2D collision)
	{
		if (!collision.CompareTag("Enemy")) return;
		if(!target) return;
		
		var enemy = target.GetComponentInParent<Enemy>();
		if(enemy) enemy.TakeDamage(firedBy.damage);
		IsOnDestroy?.Invoke(gameObject);
	}
}
