using System.Collections.Generic;
using UnityEngine;

public sealed class PlayerController : MonoBehaviour
{
    [Header("Debug settings")]
    [SerializeField] bool _isDebug;

    [Header("Settings")]
    [SerializeField] Damage _damage;

    static float _difficultyMultiplier;
    public static float DifficultyMultiplier => _difficultyMultiplier;

    Events _events;

    void Start()
    {
        _events = Events.GetInstance;

        _events.OnGameStart.AddListener(Init);
        _events.OnEnemyKilled.AddListener(UpdateDifficulty);
    }

    void Init()
    {
        _difficultyMultiplier = 1f;
    }

    void UpdateDifficulty(Enemy enemy)
    {

    }
}
