using UnityEngine;

public class SoundButton : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AudioManager.instance.ToggleSound();
        }
    }
}