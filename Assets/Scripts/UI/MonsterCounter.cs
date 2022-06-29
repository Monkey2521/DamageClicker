using UnityEngine;
using UnityEngine.UI;

public class MonsterCounter : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] Text _currentMonsterCount;
    [SerializeField] Text _maxMonsterCount;
    [SerializeField] Animator _animator;
    int _monsterCount;

    [SerializeField] Text _scoreCount;
    int _score;
    public int TotalScore { get; private set; }

    Events _events;

    void Start()
    {
        Init();

        _events = Events.GetInstance;

        _events.OnGameStart.AddListener(Init);
        _events.OnEnemyKilled.AddListener(OnKilledUpdate);
        _events.OnEnemySpawned.AddListener(OnSpawnedUpdate);
    }

    public void Init()
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

    void OnSpawnedUpdate(Enemy enemy)
    {
        _monsterCount++;

        UpdateMonsterCounter();
    }

    void OnKilledUpdate(Enemy enemy)
    {
        _monsterCount--;

        UpdateMonsterCounter();
        AddScore(enemy.ScoreReward);
    }

    void AddScore(int score)
    {
        _score += score;
        TotalScore += score;

        _scoreCount.text = _score.ToString();
    }
}
