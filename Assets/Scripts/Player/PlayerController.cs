using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField] private Menu _gameOverMenu;
    [SerializeField] private GameObject _boostersMenu;
    [SerializeField] private Text _timerText;
    [SerializeField] private Text _gameOverScore; 

    private float _timer;

    private void Start()
    {
        EventBus.Subscribe(this);
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        _timerText.text = new GameTime(_timer).GetTime();
    }

    public void Restart()
    {
        _startGameButton.gameObject.SetActive(true);
        _timerText.gameObject.SetActive(false);
        _monsterCounter.gameObject.SetActive(false);
        _boostersMenu.gameObject.SetActive(false);
        _gameOverMenu.gameObject.SetActive(false);
    }

    public void StartGame()
    {
        _difficultyMultiplier = 1f;

        _monsterCounter.gameObject.SetActive(true);
        _startGameButton.SetActive(false);
        _timerText.gameObject.SetActive(true);

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

        _boostersMenu.gameObject.SetActive(false);
        _startGameButton.gameObject.SetActive(false);
        _timerText.gameObject.SetActive(false);

        _gameOverMenu.gameObject.SetActive(true);
        _gameOverScore.text = "GAME OVER!\nTotal score: " + _monsterCounter.TotalScore.ToString();
        _gameOverMenu.CheckRecords(_timer, _monsterCounter.TotalScore);
        _monsterCounter.gameObject.SetActive(false);
    }
}
