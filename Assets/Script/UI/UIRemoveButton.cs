using UnityEngine;
using UnityEngine.UI;

public class UIRemoveButton : MonoBehaviour
{
    public PackGuardian placer;

    public Image buttonImage;

    public void ToggleRemoveMode()
    {
        placer.isRemoveMode = !placer.isRemoveMode;

        buttonImage.color = placer.isRemoveMode ? Color.red : Color.white;
    }
}