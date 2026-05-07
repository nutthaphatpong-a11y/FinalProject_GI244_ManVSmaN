using UnityEngine;
using UnityEngine.SceneManagement;
public class Buttom : MonoBehaviour
{
    public void PlayA()
    {
        SceneManager.LoadScene("GameSettings");
    }
}
