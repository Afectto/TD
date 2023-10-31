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
