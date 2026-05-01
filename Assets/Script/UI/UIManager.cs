using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI waveText;
    public TextMeshProUGUI hpText;

    void Update()
    {
        moneyText.text = "" + GameManager.instance.money;
    }
}