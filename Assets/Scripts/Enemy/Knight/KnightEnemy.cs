public class KnightEnemy : Enemy
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


    // public override IEnumerator Attack()
    // {
    //     return base.Attack();
    // }
}
