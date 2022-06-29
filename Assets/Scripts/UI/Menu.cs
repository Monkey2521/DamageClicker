using UnityEngine;


public class Menu : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] Records _records;
    [SerializeField] GameObject _settings;
    [SerializeField] GameObject _credits;

    #region Scenes indexes
    readonly static int MAIN_MENU_SCENE = 0;
    readonly static int GAME_SCENE = 1;
    #endregion

    #region Button functions
    public void StartGame()
    {
        LoadScene(GAME_SCENE);
    }

    public void OpenMainMenu()
    {
        LoadScene(MAIN_MENU_SCENE);
    }

    public void OpenRecords()
    {

    }

    public void OpenSettings()
    {

    }

    public void OpenCredits()
    {

    }

    public void ExitGame()
    {
        Application.Quit();
    }
    #endregion

    void LoadScene(int index)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(index);
    }
}
