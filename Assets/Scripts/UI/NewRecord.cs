using UnityEngine;
using UnityEngine.UI;

public sealed class NewRecord : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Text _timeText;
    [SerializeField] private Text _scoreText;
    [SerializeField] private BestScores _bestScores;

    public bool CheckRecord(float time, int score)
    {
        if (_bestScores.CheckScore(time, score))
        {
            BestScore bestScore = new BestScore(time, score);

            _timeText.text = "TIME: " + bestScore.Time.GetTime();
            _scoreText.text = "TOTAL SCORE: " + bestScore.Score.ToString();

            return true;
        }

        else return false;
    }
}
