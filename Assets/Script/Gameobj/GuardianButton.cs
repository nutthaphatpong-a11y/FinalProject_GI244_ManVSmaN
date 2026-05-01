using UnityEngine;
using UnityEngine.UI;

public class GuardianButton : MonoBehaviour
{
    public GameObject guardianPrefab;
    public PackGuardian placer;

    private Button button;
    private Guardian data;

    void Start()
    {
        button = GetComponent<Button>();
        data = guardianPrefab.GetComponent<Guardian>();
    }

    void Update()
    {
        // 🔥 ปิดปุ่มถ้าเงินไม่พอ
        button.interactable = (GameManager.instance.money >= data.cost);
    }

    // 👇 ใช้ตอนกดปุ่ม
    public void SelectGuardian()
    {
        placer.selectedGuardian = guardianPrefab;
    }
}