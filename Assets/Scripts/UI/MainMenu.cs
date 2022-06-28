using UnityEngine;

public class MainMenu : MonoBehaviour
{
    readonly static int MAIN_MENU_SCENE = 0;
    readonly static int GAME_SCENE = 1;

    public void StartGame()
    {
        LoadScene(GAME_SCENE);
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
    
    void LoadScene(int index)
    {

    }
}
