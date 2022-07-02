using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class EnemySpawner : MonoBehaviour, IGameStartHandler, IGameOverHandler
{
    [Header("Debug settings")]
    [SerializeField] private bool _isDebug;

    [Header("Settings")]
    [SerializeField][Range(0.1f, 10f)] private float _spawningTime;
    [SerializeField][Range(0f, 1f)] private float _spawningSpreadingTime;
    [SerializeField][Range(0.01f, 0.1f)] private float _difficultyTimeReduce;

    [Space(5)]
    [SerializeField] private List<EnemyPool> _enemiesPools;

    [Space(5)]
    [SerializeField] private Transform _enemiesParent;

    private ObjectPool _pool;

    private bool _isSpawning;

    private void Start()
    {
        Init();

        EventBus.Subscribe(this);
    }

    private void Init()
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

    private void FixedUpdate()
    {
        foreach(Enemy enemy in _pool.PulledObjects)
        {
            if (enemy.OnTarget)
                enemy.SetTargetPosition(GetRandomPosition());

            enemy.Move();
        }
    }

    public void OnGameStart()
    {
        if (_isDebug) Debug.Log(name + " start game");

        EnableSpawning();
    }

    public void OnGameOver()
    {
        if (_isDebug) Debug.Log(name + " game over");

        DisableSpawning();

        _pool.ReturnAllToPool();
    }

    public void EnableSpawning()
    {
        if (_isSpawning) StopAllCoroutines();

        _isSpawning = true;

        Spawn();
    }

    public void DisableSpawning() 
    {
        _isSpawning = false;

        StopAllCoroutines();
    }

    private void Spawn()
    {
        Enemy enemy = _pool.PullObject() as Enemy;

        if (enemy == null)
        {
            EventBus.Publish<IGameOverHandler>(handler => handler.OnGameOver());
            return;
        }
            
        enemy.transform.position = GetRandomPosition();
        enemy.Init(PlayerController.DifficultyMultiplier);
        enemy.SetTargetPosition(GetRandomPosition());

        EventBus.Publish<IEnemySpawnedHandler>(handler => handler.OnEnemySpawned(enemy));

        StartCoroutine(WaitSpawn());
    }

    private IEnumerator WaitSpawn()
    {
        yield return new WaitForSeconds(Random.Range
            (
                _spawningTime - _spawningSpreadingTime,
                _spawningTime + _spawningSpreadingTime
            ) - _difficultyTimeReduce * (PlayerController.DifficultyMultiplier));

        if (_isSpawning) Spawn();
    }

    private Vector3 GetRandomPosition() => new Vector3
        (
            Random.Range(-WorldBuilder.MAX_SPAWN_POSITION, WorldBuilder.MAX_SPAWN_POSITION),
            1f,
            Random.Range(-WorldBuilder.MAX_SPAWN_POSITION, WorldBuilder.MAX_SPAWN_POSITION)
        );
}
