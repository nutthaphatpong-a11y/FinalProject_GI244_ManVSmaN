using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public PowerUpType type;
    public float value = 1f;
    public float duration = 5f;

    private bool isSelected = false;

    Renderer[] renderers;

    void Start()
    {
        renderers = GetComponentsInChildren<Renderer>();

     
        Destroy(gameObject, 10f);
    }

    void Update()
    {
        transform.Rotate(Vector3.up * 100f * Time.deltaTime);

        if (!isSelected) return;

        if (Input.GetMouseButtonDown(0))
        {
            TryApplyToGuardian();
        }
    }

    void OnMouseDown()
    {
        isSelected = true;
        SetColor(Color.cyan);
        Debug.Log("เลือก PowerUp แล้ว");
    }


    void TryApplyToGuardian()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Guardian g = hit.collider.GetComponentInParent<Guardian>();

            if (g != null)
            {
                g.ApplyPowerUp(type, value, duration);
                Destroy(gameObject);
            }
        }
    }

    void SetColor(Color color)
    {
        foreach (Renderer r in renderers)
        {
            r.material.color = color;
        }
    }
}