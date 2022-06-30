using UnityEngine;
using UnityEngine.UI;

public class MonsterCounter : MonoBehaviour, IGameStartHandler, IEnemyKilledHandler, IEnemySpawnedHandler
{
    [Header("Settings")]
    [SerializeField] Text _currentMonsterCount;
    [SerializeField] Text _maxMonsterCount;
    [SerializeField] Animator _animator;
    int _monsterCount;

    [SerializeField] Text _scoreCount;
    int _score;
    public int TotalScore { get; private set; } 

    void Start()
    {
        EventBus.Subscribe(this);
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

    void AddScore(int score)
    {
        _score += score;
        TotalScore += score;

        _scoreCount.text = _score.ToString();
    }
}
