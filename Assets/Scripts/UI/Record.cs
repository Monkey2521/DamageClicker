using UnityEngine;
using UnityEngine.UI;

public sealed class Record : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Text _positionText;
    [SerializeField] private Text _timeText;
    [SerializeField] private Text _scoreText;

    public void Init(BestScore bestScore)
    {
        _positionText.text = bestScore.Position.ToString();
        _timeText.text = bestScore.Time.GetTime();
        _scoreText.text = bestScore.Score.ToString();
    }
}
