using System;
using System.Collections.Generic;
using UnityEngine;

public class PoolBase<T>
{
    private readonly Func<T> _preloadFunc;
    private readonly Action<T> _getAction;
    private readonly Action<T> _returnAction;
    private Queue<T> _pool = new Queue<T>();
    public List<T> allActiveObject = new List<T>();

    public PoolBase(Func<T> preloadFunc, Action<T> getAction, Action<T> returnAction, int preloadCount = 10)
    {
        _preloadFunc = preloadFunc;
        _getAction = getAction;
        _returnAction = returnAction;

        if (preloadFunc == null)
        {
            Debug.LogError("Preload function is null");
            return;
        }

        for (int i = 0; i < preloadCount; i++)
        {
            Return(preloadFunc());
        }
    }

    public T Get()
    {
        T item = _pool.Count > 0 ? _pool.Dequeue() : _preloadFunc();
        _getAction(item);
        allActiveObject.Add(item);

        return item;
    }

    public void Return(T item)
    {
        _returnAction(item);
        _pool.Enqueue(item);
        allActiveObject.Remove(item);
    }

    public List<T> GetAllActiveObject()
    {
        return allActiveObject;
    }
}
