using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightWeapon : Weapon, IEnemyWeapon
{
    public override IEnumerator Attack()
    {
        PlayAttackAnimation();
        yield return base.Attack();
        target?.GetComponentInParent<Tower>()?.TakeDamage(damage);
    }

    public Animator enemyAnimator { get; private set; }
    private void Awake()
    {
        enemyAnimator = GetComponentInParent<Enemy>()?._animation;
    }
    
    public void PlayAttackAnimation()
    {
        enemyAnimator.Play("attack");
        enemyAnimator.speed = enemyAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.length / attackRate;
    }

}
