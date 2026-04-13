using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int money = 100;

    void Awake()
    {
        instance = this;
    }

    public bool SpendMoney(int amount)
    {
        if (money >= amount)
        {
            money -= amount;
            return true;
        }

        return false;
    }

    public void AddMoney(int amount)
    {
        money += amount;
    }
}