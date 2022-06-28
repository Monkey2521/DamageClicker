using System.Collections.Generic;
using UnityEngine;

public sealed class EnemySpawner : MonoBehaviour
{
    [Header("Debug settings")]
    [SerializeField] bool _isDebug;

    [Header("Settings")]
    [SerializeField] List<EnemyPool> _enemiesPools;

    [Space(5)]
    [SerializeField] Transform _enemiesParent;

    ObjectPool _pool;

    void Start()
    {
        Init();
    }

    void Init()
    {
        _pool = new ObjectPool(_isDebug);
        
        foreach(EnemyPool enemyPool in _enemiesPools)
            for (int i = 0; i < enemyPool.Size; i++)
            {
                Enemy enemy = Instantiate(enemyPool.Prefab, _enemiesParent);

                _pool.AddObject(enemy);
            }
    }

    [ContextMenu("Spawn enemy")]
    void Spawn()
    {
        Enemy enemy = _pool.PullObject() as Enemy;

        if (enemy == null) return; // TODO 

        enemy.transform.position = GetRandomPosition();
    }

    Vector3 GetRandomPosition() => new Vector3
        (
            Random.Range(-WorldBuilder.MAX_SPAWN_POSITION, WorldBuilder.MAX_SPAWN_POSITION),
            1f,
            Random.Range(-WorldBuilder.MAX_SPAWN_POSITION, WorldBuilder.MAX_SPAWN_POSITION)
        );
}
