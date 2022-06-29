using System.Collections.Generic;
using UnityEngine;

public sealed class PlayerController : MonoBehaviour
{
    [Header("Debug settings")]
    [SerializeField] bool _isDebug;

    [Header("Settings")]
    [SerializeField] float _difficultyUpgradePerMonster;
    [SerializeField] Damage _damage;

    static float _difficultyMultiplier;
    public static float DifficultyMultiplier => _difficultyMultiplier;

    [Header("UI settings")]
    [SerializeField] GameObject _startGameButton;

    Events _events;

    void Start()
    {
        _events = Events.GetInstance;

        _events.OnEnemyKilled.AddListener(UpdateDifficulty);
    }

    public void StartGame()
    {
        _difficultyMultiplier = 1f;

        _events.OnGameStart?.Invoke();

        _startGameButton.SetActive(false);
    }

    void UpdateDifficulty(Enemy enemy)
    {
        _difficultyMultiplier += _difficultyUpgradePerMonster;
    }
}
