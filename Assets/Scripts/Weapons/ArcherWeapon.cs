
using System.Collections;

public class ArcherWeapon : ShooterWeapon
{
    public override IEnumerator Attack()
    {
        playAttackAnimation();
        
        return base.Attack();
    }

    private void playAttackAnimation()
    {
        var enemy = GetComponentInParent<Enemy>();
        // ReSharper disable once Unity.NoNullPropagation
        var animator = enemy?._animation;
        animator.Play("attack");
        animator.speed = animator.GetCurrentAnimatorClipInfo(0)[0].clip.length / attackRate;
    }
    
}
