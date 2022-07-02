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
    [SerializeField] private MonsterCounter _monsterCounter;
    [SerializeField] private Menu _pauseMenu;
    [SerializeField] private Menu _gameOverMenu;
    [SerializeField] private GameObject _boostersMenu;

    private float _timer;

    private void Start()
    {
        EventBus.Subscribe(this);
    }

    private void Update()
    {
        _timer += Time.deltaTime;
    }

    public void Restart()
    {
        _startGameButton.gameObject.SetActive(true);
        _monsterCounter.gameObject.SetActive(false);
        _pauseMenu.gameObject.SetActive(false);
        _boostersMenu.gameObject.SetActive(false);
        _gameOverMenu.gameObject.SetActive(false);
    }

    public void StartGame()
    {
        _difficultyMultiplier = 1f;

        _monsterCounter.gameObject.SetActive(true);
        _startGameButton.SetActive(false);

        _timer = 0;

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
        _startGameButton.gameObject.SetActive(false);

        _gameOverMenu.gameObject.SetActive(true);
        _gameOverMenu.CheckRecords(_timer, _monsterCounter.TotalScore);
    }

}
