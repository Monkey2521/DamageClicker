using System.Collections.Generic;
using UnityEngine;

public sealed class ObjectPool
{
    bool _isDebug;

    List<IPoolable> _pool;
    List<IPoolable> _pulledObjects;
    public List<IPoolable> PulledObjects => _pulledObjects;

    public bool IsEmpty => _pool.Count == 0;

    public ObjectPool(bool isDebug = false)
    {
        _pool = new List<IPoolable>();
        _pulledObjects = new List<IPoolable>();

        _isDebug = isDebug;
    }

    public List<IPoolable> Reset()
    {
        ReturnAllToPool();

        List<IPoolable> pool = new List<IPoolable>(_pool);
        _pool.Clear();

        return pool;
    }

    public void AddObject(IPoolable poolable)
    {
        poolable.Pool = this;
        poolable.ReturnToPool();
    }

    public void ReturnToPool(IPoolable poolable)
    {
        if (_pulledObjects.Contains(poolable))
            _pulledObjects.Remove(poolable);
        else if (_isDebug)
            Debug.Log("New object in pool: " + poolable);

        _pool.Add(poolable);
    }

    public void ReturnAllToPool()
    {
        if (_isDebug) Debug.Log("Return all object to pool...");

        while (_pulledObjects.Count > 0)
            _pulledObjects[0].ReturnToPool();
    }

    public IPoolable PullObject()
    {
        if (_pool.Count == 0)
        {
            if (_isDebug) Debug.Log("Pool is empty!");

            return null;
        }

        int index = Random.Range(0, _pool.Count);
        IPoolable poolable = _pool[index];
        poolable.PullFromPool();

        _pool.Remove(poolable);
        _pulledObjects.Add(poolable);

        return poolable;
    }
}
