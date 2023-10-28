using System.Collections;

public interface IAttacker
{
    public float damage { get; set; }
    public float attackRate { get; set; }
    IEnumerator Attack();
}