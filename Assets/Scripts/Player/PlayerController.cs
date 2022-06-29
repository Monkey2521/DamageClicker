using System.Collections.Generic;
using UnityEngine;

public sealed class PlayerController : MonoBehaviour
{
    [Header("Debug settings")]
    [SerializeField] bool _isDebug;

    [Header("Settings")]
    [SerializeField][Range(0.01f, 0.2f)] float _difficultyUpgradePerMonster;
    [SerializeField] Damage _damage;

    static float _difficultyMultiplier;
    public static float DifficultyMultiplier => _difficultyMultiplier;

    [Header("UI settings")]
    [SerializeField] GameObject _startGameButton;
    [SerializeField] Shop _shopMenu;
    [SerializeField] GameObject _boostersMenu;
    [SerializeField] Menu _pauseMenu;
    [SerializeField] Menu _gameOverMenu;

    Events _events;

    void Start()
    {
        _events = Events.GetInstance;

        _events.OnEnemyKilled.AddListener(UpdateDifficulty);
    }

    public void Restart()
    {

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
