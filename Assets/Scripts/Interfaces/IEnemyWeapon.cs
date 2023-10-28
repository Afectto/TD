using UnityEngine;

public interface IEnemyWeapon
{
    public Animator enemyAnimator { get; }
    void PlayAttackAnimation();
}