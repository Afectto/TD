using System;
using System.Collections;
using UnityEngine;

public class ArcherWeapon : ShooterWeapon, IEnemyWeapon
{
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

    public override IEnumerator Attack()
    {
        PlayAttackAnimation();
        
        return base.Attack();
    }
}
