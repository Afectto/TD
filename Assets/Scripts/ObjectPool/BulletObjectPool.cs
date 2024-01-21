using UnityEngine;

public class BulletObjectPool : MonoBehaviour
{
    public GameObjectPool bulletObjectPool;
    public GameObject bulletPrefab;
    private void Awake()
    {
        bulletObjectPool = new GameObjectPool(bulletPrefab, gameObject);
    }

}
