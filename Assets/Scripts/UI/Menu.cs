using UnityEngine;

public sealed class Menu : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Records _records;
    [SerializeField] private GameObject _credits;
    [SerializeField] private NewRecord _newRecord;
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _gameMenu;
    [SerializeField] private GameObject _worldBuilder;
    [SerializeField] private GameObject _gameControls;
    [SerializeField] private GameObject _startGameButton;

    #region Button functions
    public void StartGame()
    {
        _gameControls.SetActive(true);
        _worldBuilder.SetActive(true);
        _gameMenu.SetActive(true);
        _startGameButton.SetActive(true);
        _mainMenu.SetActive(false);
    }

    public void OpenMainMenu()
    {
        _gameControls.SetActive(false);
        _worldBuilder.SetActive(false);
        _startGameButton.SetActive(false);
        _mainMenu.SetActive(true);
        _gameMenu.SetActive(false);
        gameObject.SetActive(false);
    }

    public void OpenRecords()
    {
        _records.gameObject.SetActive(true);
        _records.Init();
    }

    public void CheckRecords(float time, int score)
    {
        _newRecord.gameObject.SetActive(_newRecord.CheckRecord(time, score));
    }

    public void OpenCredits()
    {
        _credits.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
    #endregion
}
