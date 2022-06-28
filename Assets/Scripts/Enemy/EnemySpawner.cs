using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public sealed class EnemySpawner : MonoBehaviour
{
    [Header("Debug settings")]
    [SerializeField] bool _isDebug;

    [Header("Settings")]
    [SerializeField][Range(0.1f, 10f)] float _spawningTime;
    [SerializeField][Range(0f, 1f)] float _spawningSpreadingTime;
    [SerializeField][Range(0.01f, 0.1f)] float _difficultyTimeReduce;

    [Space(5)]
    [SerializeField] List<EnemyPool> _enemiesPools;

    [Space(5)]
    [SerializeField] Transform _enemiesParent;

    ObjectPool _pool;
    bool _isSpawning;


    void Start()
    {
        Init();
    }

    void Init()
    {
        if (_pool != null)
            foreach (Enemy enemy in _pool.Reset())
                Destroy(enemy);

        _pool = new ObjectPool(_isDebug);
        
        foreach(EnemyPool enemyPool in _enemiesPools)
            for (int i = 0; i < enemyPool.Size; i++)
            {
                Enemy enemy = Instantiate(enemyPool.Prefab, _enemiesParent);

                _pool.AddObject(enemy);
            }
    }

    void FixedUpdate()
    {
        foreach(Enemy enemy in _pool.PulledObjects)
        {
            if (enemy.OnTarget)
                enemy.SetTargetPosition(GetRandomPosition());

            enemy.Move();
        }
    }

    [ContextMenu("Spawn enemy")]
    public void ChangeSpawning()
    {
        _isSpawning = !_isSpawning;

        if (_isSpawning) Spawn();
    }

    void Spawn()
    {
        Enemy enemy = _pool.PullObject() as Enemy;

        if (enemy == null) return; // TODO 

        enemy.transform.position = GetRandomPosition();
        enemy.Init(1f);
        enemy.SetTargetPosition(GetRandomPosition());

        WaitSpawn();
    }

    async void WaitSpawn()
    {
        await Task.Delay((int)(Random.Range(_spawningTime - _spawningSpreadingTime, _spawningTime + _spawningSpreadingTime) *
            1000 - _difficultyTimeReduce));

        if (_isSpawning) Spawn();
    }

    Vector3 GetRandomPosition() => new Vector3
        (
            Random.Range(-WorldBuilder.MAX_SPAWN_POSITION, WorldBuilder.MAX_SPAWN_POSITION),
            1f,
            Random.Range(-WorldBuilder.MAX_SPAWN_POSITION, WorldBuilder.MAX_SPAWN_POSITION)
        );
}
