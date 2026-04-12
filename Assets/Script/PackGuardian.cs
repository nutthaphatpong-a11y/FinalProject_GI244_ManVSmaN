using UnityEngine;
using UnityEngine.Tilemaps;

public class PackGuardian : MonoBehaviour
{
    public GameObject GuardianPrefab;
    public LayerMask tileLayer;

    private Tile myTile;

    public void SetTile(Tile tile)
    {
        myTile = tile;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
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
                // 🔥 1. สร้าง Guardian
                GameObject Guardian = Instantiate(
                    GuardianPrefab,
                    hit.collider.transform.position + Vector3.up * 0.5f,
                    Quaternion.identity
                );

                // 🔥 2. ล็อคช่อง
                tile.isOccupied = true;

                // 🔥 3. ใส่ตรงนี้ 👇 (สำคัญมาก)
                Guardian g = Guardian.GetComponent<Guardian>();
                if (g != null)
                {
                    g.SetTile(tile);
                }
            }
        }
    }
}
