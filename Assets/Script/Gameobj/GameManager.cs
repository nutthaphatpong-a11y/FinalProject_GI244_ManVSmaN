using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int money;
    public Text moneyText;

    void Awake()
    {
        instance = this;
    }

void Start()
{
    money = GameSettings.startMoney;
    UpdateUI();
}

    public bool SpendMoney(int amount)
    {
        if (money >= amount)
        {
            money -= amount;
            UpdateUI();
            return true;
        }

        return false;
    }

    public void AddMoney(int amount)
    {
        money += amount;
        UpdateUI();
    }

    void UpdateUI()
    {
        moneyText.text = money.ToString();
    }
}