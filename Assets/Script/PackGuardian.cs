using UnityEngine;

public class PackGuardian : MonoBehaviour
{
    public GameObject GuardianPrefab;
    public GameObject selectedGuardian;
    public LayerMask tileLayer;

    void Update()
    {
        if (selectedGuardian != null && Input.GetMouseButtonDown(0))
        {
            PlaceGuardian();
        }
    }

    void PlaceGuardian()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 100f, tileLayer))
        {
            Tile tile = hit.collider.GetComponent<Tile>();

            if (tile != null && !tile.isOccupied)
            {
                Guardian gData = selectedGuardian.GetComponent<Guardian>();
                int cost = gData.cost;

                if (GameManager.instance.SpendMoney(cost))
                {
                    GameObject plant = Instantiate(
                        selectedGuardian,
                        hit.collider.transform.position + Vector3.up * 0.5f,
                        Quaternion.identity
                    );

                    tile.isOccupied = true;

                    Guardian g = plant.GetComponent<Guardian>();
                    if (g != null)
                    {
                        g.SetTile(tile);
                    }

                    // 🔥 วางเสร็จ → ยกเลิกโหมด
                    selectedGuardian = null;
                }
                else
                {
                    Debug.Log("Not enough money!");
                }
            }
        }
    }
}