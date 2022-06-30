using System.Collections.Generic;
using UnityEngine;

public sealed class PlayerController : MonoBehaviour, IGameOverHandler, IEnemyKilledHandler, ISubscriber
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
    [SerializeField] Enemy enemy;

    void Start()
    {
        EventBus.Subscribe(this);
    }

    public void Restart()
    {

    }

    public void StartGame()
    {
        _difficultyMultiplier = 1f;

        EventBus.Publish<IGameStartHandler>(handler => handler.OnGameStart());

        _startGameButton.SetActive(false);
    }

    public void OnEnemyKilled(Enemy enemy) // увеличение сложности
    {
        _difficultyMultiplier += _difficultyUpgradePerMonster;
    }

    public void OnGameOver()
    {
        Debug.Log("GameOver");
    }

}
