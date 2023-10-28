using UnityEngine;

public interface IShooter: IAttacker
{
    public Transform shootElement { get; set; }
    public GameObject bullet { get; set; }
}