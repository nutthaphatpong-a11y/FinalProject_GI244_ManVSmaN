using UnityEngine;

public class SelectGuardian : MonoBehaviour
{
    public GameObject guardianPrefab;

    public void Select()
    {
        PackGuardian placer = Object.FindFirstObjectByType<PackGuardian>();
        placer.selectedGuardian = guardianPrefab;
    }
}