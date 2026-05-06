using UnityEngine;
using UnityEngine.SceneManagement;

public class DifficultyMenu : MonoBehaviour
{
    public void SelectEasy()
    {
        GameSettings.difficulty = 0;
GameSettings.ApplyDifficulty();
        SceneManager.LoadScene("GamePlay");
    }

    public void SelectMedium()
    {
        GameSettings.difficulty = 1;
GameSettings.ApplyDifficulty();
        SceneManager.LoadScene("GamePlay");
    }

    public void SelectHard()
    {
        GameSettings.difficulty = 2;
GameSettings.ApplyDifficulty();
        SceneManager.LoadScene("GamePlay");
    }
}