using System.Collections;
using UnityEngine;

public abstract class MeleeWeapon : Weapon, IEnemyWeapon
{
    public override IEnumerator Attack()
    {
        PlayAttackAnimation();
        yield return base.Attack();
        target?.GetComponentInParent<Tower>()?.TakeDamage(damage);
    }

    public Animator enemyAnimator { get; private set; }

    protected void Initialize()
    {
        enemyAnimator = GetComponent<Enemy>()?._animation;
        baseDamage = damage;
        baseAttackRate = attackRate;
    }

    public void PlayAttackAnimation()
    {
        enemyAnimator.Play("attack");
        enemyAnimator.speed = enemyAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.length / attackRate;
    }
}