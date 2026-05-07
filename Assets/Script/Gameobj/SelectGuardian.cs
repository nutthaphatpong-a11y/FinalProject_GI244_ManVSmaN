using UnityEngine;
using UnityEngine.UI;

public class SelectGuardian : MonoBehaviour
{
    public PackGuardian placer;
    public GameObject guardianPrefab;

    public Image buttonImage;

    public Color normalColor = Color.white;
    public Color selectedColor = Color.green;

    private static SelectGuardian current;

    public void Select()
    {
        if (placer == null) return;

        // ถ้ากดปุ่มเดิมซ้ำ = ยกเลิก
        if (current == this)
        {
            placer.selectedGuardian = null;

            if (buttonImage != null)
                buttonImage.color = normalColor;

            current = null;
            return;
        }

        // ปิด remove mode
        placer.isRemoveMode = false;

        if (placer.removeButtonImage != null)
            placer.removeButtonImage.color = Color.white;

        // คืนสีปุ่มเก่า
        if (current != null && current.buttonImage != null)
        {
            current.buttonImage.color = current.normalColor;
        }

        // เลือกปุ่มใหม่
        placer.selectedGuardian = guardianPrefab;

        if (buttonImage != null)
            buttonImage.color = selectedColor;

        current = this;
    }

    public static void ClearSelection()
    {
        if (current != null && current.buttonImage != null)
        {
            current.buttonImage.color = current.normalColor;
        }

        current = null;
    }
}