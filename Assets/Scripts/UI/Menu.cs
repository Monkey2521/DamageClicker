using UnityEngine;

public sealed class Menu : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Records _records;
    [SerializeField] private GameObject _credits;

    #region Scenes indexes
    private readonly static int MAIN_MENU_SCENE = 0;
    private readonly static int GAME_SCENE = 1;
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

    public void OpenCredits()
    {

    }

    public void ExitGame()
    {
        Application.Quit();
    }
    #endregion

    private void LoadScene(int index)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(index);
    }
}
