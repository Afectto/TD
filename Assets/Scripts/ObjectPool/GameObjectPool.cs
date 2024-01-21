using UnityEngine;
using Object = UnityEngine.Object;

public class GameObjectPool : PoolBase<GameObject>
{
    public GameObjectPool(GameObject prefab, GameObject parent, int preloadCount = 10) :
        base(() => Preload(prefab, parent), GetAction, ReturnAction, preloadCount)
    { }

    public static GameObject Preload(GameObject prefab, GameObject parent) => Object.Instantiate(prefab, parent.transform);
    public static void GetAction(GameObject @gameObject) => gameObject.SetActive(true);
    public static void ReturnAction(GameObject @gameObject) => gameObject.SetActive(false);
    
}
