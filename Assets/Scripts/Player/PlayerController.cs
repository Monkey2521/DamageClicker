using System.Collections.Generic;
using UnityEngine;

public sealed class PlayerController : MonoBehaviour, IGameOverHandler, IEnemyKilledHandler, IEnemyClickedHandler
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

    void Start()
    {
        EventBus.Subscribe(this);
    }

    public void Restart()
    {
        _startGameButton.gameObject.SetActive(true);
        _pauseMenu.gameObject.SetActive(false);
        _boostersMenu.gameObject.SetActive(false);
        _shopMenu.gameObject.SetActive(false);
        _gameOverMenu.gameObject.SetActive(false);
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

    public void OnEnemyClicked(Enemy enemy)
    {
        Debug.Log(enemy);
    }

    public void OnGameOver()
    {
        Debug.Log("GameOver");
        _pauseMenu.gameObject.SetActive(false);
        _boostersMenu.gameObject.SetActive(false);
        _shopMenu.gameObject.SetActive(false);
        _startGameButton.gameObject.SetActive(false);
        _gameOverMenu.gameObject.SetActive(true);
    }

}
