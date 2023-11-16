using System.Collections.Generic;
using UnityEngine;

public interface ITowerWeapon
{
    public GameObject currentTarget { get; set; }
    public Queue<GameObject> allTarget{ get; set; }

    public void UpdateNewTarget();
    public void OnTriggerEnter2D(Collider2D collision);
    
}