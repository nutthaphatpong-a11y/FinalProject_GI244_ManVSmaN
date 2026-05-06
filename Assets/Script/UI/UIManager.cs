using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI moneyText;

    void Update()
    {
        moneyText.text = "" + GameManager.instance.money;
    }
}