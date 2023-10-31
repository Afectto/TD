public interface IDamageable
{
    public float health { get; set; }
    public float maxHealth { get; set; }
    
    void TakeDamage(float aDamage);
}