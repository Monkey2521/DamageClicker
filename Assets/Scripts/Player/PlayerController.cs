using System.Collections.Generic;
using UnityEngine;

public sealed class PlayerController : MonoBehaviour, IGameOverHandler, IEnemyKilledHandler, IEnemyClickedHandler
{
    [Header("Debug settings")]
    [SerializeField] private bool _isDebug;

    [Header("Settings")]
    [SerializeField][Range(0.01f, 0.2f)] private float _difficultyUpgradePerMonster;
    [SerializeField] private Damage _damage;

    private static float _difficultyMultiplier;
    public static float DifficultyMultiplier => _difficultyMultiplier;

    [Header("UI settings")]
    [SerializeField] private GameObject _startGameButton;
    [SerializeField] private GameObject _monsterCounter;
    [SerializeField] private Menu _pauseMenu;
    [SerializeField] private Menu _gameOverMenu;
    [SerializeField] private Shop _shopMenu;
    [SerializeField] private GameObject _boostersMenu;

    private void Start()
    {
        EventBus.Subscribe(this);
    }

    public void Restart()
    {
        _startGameButton.gameObject.SetActive(true);
        _monsterCounter.SetActive(false);
        _pauseMenu.gameObject.SetActive(false);
        _boostersMenu.gameObject.SetActive(false);
        _shopMenu.gameObject.SetActive(false);
        _gameOverMenu.gameObject.SetActive(false);
    }

    public void StartGame()
    {
        _difficultyMultiplier = 1f;

        _monsterCounter.SetActive(true);
        _startGameButton.SetActive(false);

        EventBus.Publish<IGameStartHandler>(handler => handler.OnGameStart());
    }

    public void OnEnemyKilled(Enemy enemy) // увеличение сложности
    {
        _difficultyMultiplier += _difficultyUpgradePerMonster;
    }

    public void OnEnemyClicked(Enemy enemy)
    {
        if (_isDebug) Debug.Log(enemy);

        _damage.MakeDamage(enemy);
    }

    public void OnGameOver()
    {
        if (_isDebug) Debug.Log("GameOver");

        _pauseMenu.gameObject.SetActive(false);
        _boostersMenu.gameObject.SetActive(false);
        _shopMenu.gameObject.SetActive(false);
        _startGameButton.gameObject.SetActive(false);
        _gameOverMenu.gameObject.SetActive(true);
    }

}
