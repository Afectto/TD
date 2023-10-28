using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierEnemy : Enemy
{
    private void Awake()
    {
        Initialize();
        _weapon._isShoot = true;
    }

    private void Update()
    {
        OnUpdate();
        _weapon.ShootIfNeed();
    }

}
