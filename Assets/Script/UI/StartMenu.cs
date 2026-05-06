using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{

    public void StartGame()
    {
        SceneManager.LoadScene("GameSettings");
    }

    public void ExitGame()
    {
        Application.Quit();

        Debug.Log("Exit Game");
    }
}