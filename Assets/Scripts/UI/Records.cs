using UnityEngine;

public sealed class Records : MonoBehaviour
{
    [Header("Debug settings")]
    [SerializeField] private bool _isDebug;

    [Header("Settings")]
    [SerializeField] private BestScores _bestScores;
    [SerializeField] private Record _recordPrefab;

    public void Init()
    {

    }
}
