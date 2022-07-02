using UnityEngine;
using UnityEngine.UI;

public sealed class MonsterCounter : MonoBehaviour, IGameStartHandler, IEnemyKilledHandler, IEnemySpawnedHandler
{
    [Header("Settings")]
    [SerializeField] private Text _currentMonsterCount;
    [SerializeField] private Text _maxMonsterCount;
    [SerializeField] private Animator _animator;
    private int _monsterCount;

    [SerializeField] private Text _scoreCount;
    private int _score;
    public int TotalScore { get; private set; }

    private void Start()
    {
        EventBus.Subscribe(this);
        gameObject.SetActive(false);
    }

    public void OnGameStart()
    {
        _monsterCount = 0;

        _currentMonsterCount.text = "0";
        _maxMonsterCount.text = " / 10";

        _currentMonsterCount.color = Color.green;
        _maxMonsterCount.color = Color.green;

        _score = 0;
        TotalScore = 0;
        _scoreCount.text = "0";

        _animator.SetBool("OnDanger", false);
    }

    public void OnEnemyKilled(Enemy enemy)
    {
        _monsterCount--;

        UpdateMonsterCounter();
        AddScore(enemy.ScoreReward);
    }

    public void OnEnemySpawned(Enemy enemy)
    {
        _monsterCount++;

        UpdateMonsterCounter();
    }

    public void UpdateMonsterCounter()
    {
        if (_monsterCount < 4)
        {
            _currentMonsterCount.color = Color.green;
            _maxMonsterCount.color = Color.green;
            _animator.SetBool("OnDanger", false);
        }
        else if (_monsterCount < 8)
        {
            _currentMonsterCount.color = Color.yellow;
            _maxMonsterCount.color = Color.yellow;
            _animator.SetBool("OnDanger", false);
        }
        else
        {
            _currentMonsterCount.color = Color.red;
            _maxMonsterCount.color = Color.red;

            _animator.SetBool("OnDanger", true);
        }

        _currentMonsterCount.text = _monsterCount.ToString();
    }

    private void AddScore(int score)
    {
        _score += score;
        TotalScore += score;

        _scoreCount.text = _score.ToString();
    }
}
