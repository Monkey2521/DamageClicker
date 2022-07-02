using System.Collections.Generic;
using UnityEngine;

public sealed class Records : MonoBehaviour
{
    [Header("Debug settings")]
    [SerializeField] private bool _isDebug;

    [Header("Settings")]
    [SerializeField] private BestScores _bestScores;
    [SerializeField] private Record _recordPrefab;
    [SerializeField] private Transform _recordsParent;
    [SerializeField] private GameObject _noGamesPanel;

    private List<Record> _records = new List<Record>();

    public void Init()
    {
        if (_bestScores.IsEmpty)
        {
            _noGamesPanel.SetActive(true);
        }
        else
        {
            _noGamesPanel.SetActive(false);

            while (_records.Count > 0)
            {
                Destroy(_records[0].gameObject);
                _records.Remove(_records[0]);
            }
            
            foreach(BestScore score in _bestScores.Scores)
            {
                Record record = Instantiate(_recordPrefab, _recordsParent);
                record.Init(score);
            }
        }
    }
}
