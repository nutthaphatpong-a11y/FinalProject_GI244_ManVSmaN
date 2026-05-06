using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.SceneManagement;

public class Base : MonoBehaviour
{
    public float maxHP = 20f;
    private float currentHP;
    private float targetHP;

    [Header("UI")]
    public Slider hpBar;
    public Image fillImage;

    void Start()
    {
        targetHP = maxHP;
        currentHP = maxHP;
        
    }

    private void Update()
    {
        currentHP = Mathf.Lerp(currentHP, targetHP, Time.deltaTime * 5f);

        UpdateUI();
    }

    public void TakeDamage(float dmg)
    {
        targetHP -= dmg;

        if (targetHP <= 0)
        {
            Die();
        }
    }

    void UpdateUI()
    {
        if (hpBar != null)
        {
            float percent = currentHP / maxHP;
            hpBar.value = percent;

            if (fillImage != null)
            {
                if (percent > 0.6f)
                    fillImage.color = Color.green;
                else if (percent > 0.3f)
                    fillImage.color = Color.yellow;
                else
                    fillImage.color = Color.red;
            }
        }
    }

    void Die()
    {

        Time.timeScale = 0f;
SceneManager.LoadScene("Lose");
    }
}