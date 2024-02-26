using System.Collections.Generic;
using UnityEngine;

public interface ITowerWeapon
{
    public GameObject currentTarget { get; set; }

    public void UpdateNewTarget();
    
}