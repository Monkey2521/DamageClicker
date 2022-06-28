using UnityEngine;

public interface IPoolable
{
    public ObjectPool Pool { get; set; }

    public void ReturnToPool();

    public void PullFromPool();
}
