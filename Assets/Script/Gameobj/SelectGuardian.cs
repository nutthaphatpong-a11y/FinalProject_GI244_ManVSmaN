using UnityEngine;
using UnityEngine.UI;

public class SelectGuardian : MonoBehaviour
{
    public PackGuardian placer;
    public GameObject guardianPrefab;

    public Image buttonImage;

    public Color normalColor = Color.white;
    public Color selectedColor = Color.green;

public void Select()
{
   // if (placer == null)
//    {
      //  Debug.LogError("ยังไม่ได้ใส่ Placer");
       // return;
    //}

    // ถ้ากดปุ่มเดิมซ้ำ = ยกเลิก
    if (placer.selectedGuardian == guardianPrefab)
    {
        placer.selectedGuardian = null;

        if (buttonImage != null)
        {
            buttonImage.color = normalColor;
        }

        return;
    }

    // reset สีทุกปุ่ม
    SelectGuardian[] buttons =
        FindObjectsByType<SelectGuardian>(FindObjectsSortMode.None);

    foreach (SelectGuardian btn in buttons)
    {
        if (btn.buttonImage != null)
        {
            btn.buttonImage.color = btn.normalColor;
        }
    }

    // เลือกตัวใหม่
    placer.selectedGuardian = guardianPrefab;

    if (buttonImage != null)
    {
        buttonImage.color = selectedColor;
    }
}
}