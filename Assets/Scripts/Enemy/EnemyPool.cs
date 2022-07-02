using UnityEngine;

[System.Serializable]
public sealed class EnemyPool
{
    [SerializeField] private Enemy _prefab;
    [SerializeField] private int _size;

    public Enemy Prefab => _prefab;
    public int Size => _size;
}
