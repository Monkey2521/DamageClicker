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
    int _totalScore;

    Events _events;

    public void Init()
    {
        _monsterCount = 0;

        _currentMonsterCount.text = "0";
        _maxMonsterCount.text = " / 10";

        _currentMonsterCount.color = Color.green;
        _maxMonsterCount.color = Color.green;

        _score = 0;
        _totalScore = 0;
        _scoreCount.text = "0";

        _animator.SetBool("OnDanger", false);

        if (_events == null)
        {
            _events = Events.GetInstance;
            _events.OnEnemyKilled.AddListener(UpdateCounters);
        } 
    }

    public void UpdateMonsterCounter(int count)
    {
        if (count < 4)
        {
            _currentMonsterCount.color = Color.green;
            _maxMonsterCount.color = Color.green;
            _animator.SetBool("OnDanger", false);
        }
        else if (count < 8)
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

        _currentMonsterCount.text = count.ToString();
        _monsterCount = count;
    }

    void UpdateCounters(Enemy enemy)
    {
        _monsterCount--;

        UpdateMonsterCounter(_monsterCount);
        AddScore(enemy.ScoreReward);
    }

    void AddScore(int score)
    {
        _score += score;
        _totalScore += score;

        _scoreCount.text = _score.ToString();
    }
}
