using System.Collections.Generic;
using UnityEngine;

public sealed class BoostersMenu : MonoBehaviour, IGameStartHandler, IGameOverHandler
{
    [Header("Settings")]
    [SerializeField] private List<Booster> _boosters;
    [SerializeField] private BoosterPrefab _boosterPrefab;
    [SerializeField] private Transform _boostersParent;
    
    private List<BoosterPrefab> _currentBoosters = new List<BoosterPrefab>();
    private bool _onGame;

    private void Awake()
    {
        EventBus.Subscribe(this);

        foreach(Booster booster in _boosters)
        {
            BoosterPrefab bp = Instantiate(_boosterPrefab, _boostersParent);
            bp.Init(booster);

            _currentBoosters.Add(bp);
        }

        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (_onGame)
            foreach (BoosterPrefab bp in _currentBoosters)
                bp.UpdateTimer();
    }

    public void OnGameStart()
    {
        foreach (BoosterPrefab bp in _currentBoosters)
            bp.Init();

        _onGame = true;
    }

    public void OnGameOver()
    {
        _onGame = false;
    }
}
