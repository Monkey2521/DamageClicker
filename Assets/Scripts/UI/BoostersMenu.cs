using System.Collections.Generic;
using UnityEngine;

public sealed class BoostersMenu : MonoBehaviour, IGameStartHandler
{
    [Header("Settings")]
    [SerializeField] private List<Booster> _boosters;
    [SerializeField] private BoosterPrefab _boosterPrefab;
    [SerializeField] private Transform _boostersParent;
    private List<BoosterPrefab> _currentBoosters;

    private void Start()
    {
        foreach(Booster booster in _boosters)
        {
            BoosterPrefab bp = Instantiate(_boosterPrefab, _boostersParent);
            bp.Init(booster);
        }

        gameObject.SetActive(false);
    }

    public void OnGameStart()
    {
        foreach (BoosterPrefab bp in _currentBoosters)
            bp.Init();
    }
}
