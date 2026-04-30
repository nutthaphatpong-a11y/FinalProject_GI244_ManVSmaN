using UnityEngine;
using UnityEngine.UI;

public class PackGuardian : MonoBehaviour
{
    [Header("Select")]
    public GameObject selectedGuardian;

    [Header("Layer")]
    public LayerMask tileLayer;
    public LayerMask groundLayer; // 🔥 เพิ่ม

    [Header("Mode")]
    public bool isRemoveMode = false;

    [Header("UI")]
    public Image buttonImage;

    [Header("Preview")]
    public GameObject previewPrefab;
    private GameObject previewObj;

    void Start()
    {
        if (previewPrefab != null)
        {
            previewObj = Instantiate(previewPrefab);
        }
    }

    void Update()
    {
        UpdatePreview();

        if (Input.GetMouseButtonDown(0))
        {
            if (isRemoveMode)
            {
                TryRemoveGuardian();
            }
            else if (selectedGuardian != null)
            {
                PlaceGuardian();
            }
        }
    }

    // =========================
    // 🎯 PLACE
    // =========================
    void PlaceGuardian()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (!Physics.Raycast(ray, out RaycastHit hit, 100f, tileLayer))
            return;

        Tile tile = hit.collider.GetComponent<Tile>();
        if (tile == null) return;

        Guardian data = selectedGuardian.GetComponent<Guardian>();
        if (data == null) return;

        if (!CanPlace(tile, data))
        {
            Debug.Log("วางไม่ได้");
            return;
        }

        // 💰 เช็คเงินตรงนี้
        if (!GameManager.instance.SpendMoney(data.cost))
        {
            Debug.Log("เงินไม่พอ!");
            return;
        }

        Vector3 pos = GetCenterPosition(tile, data);

        GameObject g = Instantiate(selectedGuardian, pos, Quaternion.identity);

        OccupyTiles(tile, data);

        g.GetComponent<Guardian>().SetTile(tile);
    }

    // =========================
    // ❌ REMOVE
    // =========================
    void TryRemoveGuardian()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Guardian g = hit.collider.GetComponentInParent<Guardian>();

            if (g != null)
            {
                g.RemoveSelf();
            }
        }
    }

    public void ToggleRemoveMode()
    {
        isRemoveMode = !isRemoveMode;

        if (buttonImage != null)
            buttonImage.color = isRemoveMode ? Color.red : Color.white;
    }

    // =========================
    // 📐 CENTER
    // =========================
    Vector3 GetCenterPosition(Tile startTile, Guardian data)
    {
        Tile endTile = GridManager.instance.GetTile(
            startTile.x + data.sizeX - 1,
            startTile.z + data.sizeZ - 1
        );

        if (endTile == null) return startTile.transform.position;

        return (startTile.transform.position + endTile.transform.position) / 2f;
    }

    // =========================
    // 🔥 SNAP GROUND
    // =========================
    Vector3 SnapToGround(Vector3 position)
    {
        Ray ray = new Ray(position + Vector3.up * 5f, Vector3.down);

        if (Physics.Raycast(ray, out RaycastHit hit, 10f, groundLayer))
        {
            return hit.point;
        }

        return position;
    }

    float GetHeightOffset(GameObject obj)
    {
        Renderer r = obj.GetComponentInChildren<Renderer>();

        if (r != null)
            return r.bounds.extents.y;

        return 0.5f;
    }

    // =========================
    // 🧠 CHECK
    // =========================
    bool CanPlace(Tile startTile, Guardian data)
    {
        for (int x = 0; x < data.sizeX; x++)
        {
            for (int z = 0; z < data.sizeZ; z++)
            {
                Tile t = GridManager.instance.GetTile(startTile.x + x, startTile.z + z);

                if (t == null || t.isOccupied)
                    return false;
            }
        }
        return true;
    }

    // =========================
    // 🧱 OCCUPY
    // =========================
    void OccupyTiles(Tile startTile, Guardian data)
    {
        for (int x = 0; x < data.sizeX; x++)
        {
            for (int z = 0; z < data.sizeZ; z++)
            {
                Tile t = GridManager.instance.GetTile(startTile.x + x, startTile.z + z);

                if (t != null)
                    t.isOccupied = true;
            }
        }
    }

    // =========================
    // 👁 PREVIEW
    // =========================
    void UpdatePreview()
    {
        if (previewObj == null)
            return;

        if (selectedGuardian == null || isRemoveMode)
        {
            previewObj.SetActive(false);
            return;
        }

        previewObj.SetActive(true);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 100f, tileLayer))
        {
            Tile tile = hit.collider.GetComponent<Tile>();

            if (tile != null)
            {
                Guardian data = selectedGuardian.GetComponent<Guardian>();

                Vector3 pos = GetCenterPosition(tile, data);

                // 🔥 SNAP preview ด้วย
                pos = SnapToGround(pos);
                pos.y += GetHeightOffset(selectedGuardian);

                previewObj.transform.position = pos;

                bool canPlace = CanPlace(tile, data);
                SetPreviewColor(canPlace);
            }
        }
    }

    void SetPreviewColor(bool canPlace)
    {
        Renderer[] rends = previewObj.GetComponentsInChildren<Renderer>();

        Color c = canPlace ? Color.green : Color.red;

        foreach (var r in rends)
        {
            r.material.color = c;
        }
    }
}