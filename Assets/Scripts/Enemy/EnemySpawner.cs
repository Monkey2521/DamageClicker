using System.Collections;
using System.Collections.Generic;
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
    Events _events;

    bool _isSpawning;
    public bool IsSpawning => _isSpawning;

    void Start()
    {
        Init();

        _events = Events.GetInstance;
        _events.OnGameStart.AddListener(EnableSpawning);
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

    [ContextMenu("Enable spawning")]
    public void EnableSpawning()
    {
        if (_isSpawning) StopAllCoroutines();

        _isSpawning = true;

        Spawn();
    }

    [ContextMenu("Disable spawning")]
    public void DisableSpawning() 
    {
        _isSpawning = false;

        StopAllCoroutines();
    }

    void Spawn()
    {
        Enemy enemy = _pool.PullObject() as Enemy;

        if (enemy == null)
        {
            _events.OnGameOver?.Invoke();
            return;
        }
            
        enemy.transform.position = GetRandomPosition();
        enemy.Init(PlayerController.DifficultyMultiplier);
        enemy.SetTargetPosition(GetRandomPosition());

        _events.OnEnemySpawned?.Invoke(enemy);

        StartCoroutine(WaitSpawn());
    }

    IEnumerator WaitSpawn()
    {
        yield return new WaitForSeconds(Random.Range
            (
                _spawningTime - _spawningSpreadingTime,
                _spawningTime + _spawningSpreadingTime
            ) - _difficultyTimeReduce * (PlayerController.DifficultyMultiplier));

        if (IsSpawning) Spawn();
    }

    Vector3 GetRandomPosition() => new Vector3
        (
            Random.Range(-WorldBuilder.MAX_SPAWN_POSITION, WorldBuilder.MAX_SPAWN_POSITION),
            1f,
            Random.Range(-WorldBuilder.MAX_SPAWN_POSITION, WorldBuilder.MAX_SPAWN_POSITION)
        );
}
