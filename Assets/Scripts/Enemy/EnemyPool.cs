using UnityEngine;

[System.Serializable]
public sealed class EnemyPool
{
    [SerializeField] Enemy _prefab;
    [SerializeField] int _size;

    public Enemy Prefab => _prefab;
    public int Size => _size;
}
