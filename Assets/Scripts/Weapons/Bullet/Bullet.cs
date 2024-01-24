using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
	public float speed;
	[HideInInspector]public Transform target;
	private Vector3 lastEnemyPosition;
	
	public IAttacker firedBy;
	private float distance;

	private static readonly Dictionary<GameObject, Action<GameObject>> OnDestroyActions = new Dictionary<GameObject, Action<GameObject>>();
	
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
				InvokeOnDestroyBullet();
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
		InvokeOnDestroyBullet();
	}
	
	protected virtual void InvokeOnDestroyBullet()
	{
		if (OnDestroyActions.ContainsKey(gameObject))
		{
			OnDestroyActions[gameObject]?.Invoke(gameObject);
			OnDestroyActions.Remove(gameObject);
		} 
	}
		
	public static void AddOnDestroyAction(GameObject bullet, Action<GameObject> action)
	{
		if (!OnDestroyActions.ContainsKey(bullet))
		{
			OnDestroyActions.Add(bullet, null);
		}
		OnDestroyActions[bullet] += action;
	}

	public static void RemoveOnDestroyAction(GameObject bullet, Action<GameObject> action)
	{
		if (OnDestroyActions.ContainsKey(bullet))
		{
			OnDestroyActions[bullet] -= action;
			if (OnDestroyActions[bullet] == null)
			{
				OnDestroyActions.Remove(bullet);
			}
		}
	}
}
