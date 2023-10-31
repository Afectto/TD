using System.Collections.Generic;
using UnityEngine;

public class TowerTriger : MonoBehaviour
{
	public Tower tower;
	public GameObject currentTarget;
	public Queue<GameObject> AllTarget;

	private void Start()
	{
		AllTarget = new Queue<GameObject>();
	}

	void Update()
	{
		if (!currentTarget && AllTarget.Count > 0)
        {
			currentTarget = AllTarget.Dequeue();
			tower.target = currentTarget.transform;
		}
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Enemy"))
		{
			AllTarget.Enqueue(collision.gameObject);
		}
	}
}
