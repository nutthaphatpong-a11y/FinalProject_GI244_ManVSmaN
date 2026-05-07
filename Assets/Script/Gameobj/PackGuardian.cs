using UnityEngine;
using UnityEngine.UI;

public class PackGuardian : MonoBehaviour
{
    [Header("Selection")]
    public GameObject selectedGuardian;

    [Header("Raycast Layers")]
    public LayerMask tileLayer;
    public LayerMask groundLayer;

    [Header("Modes")]
    public bool isRemoveMode = false;

    [Header("UI")]
    public Image removeButtonImage;

    [Header("Preview")]
    public GameObject previewPrefab;
    private GameObject previewInstance;

    void Start()
    {
        if (previewPrefab != null)
        {
            previewInstance = Instantiate(previewPrefab);
        }
    }

    void Update()
    {
        UpdatePreview();

        if (!Input.GetMouseButtonDown(0)) return;

        if (isRemoveMode)
        {
            TryRemoveGuardian();
        }
        else if (selectedGuardian != null)
        {
            TryPlaceGuardian();
        }
    }

    void TryPlaceGuardian()
    {
        if (!TryGetTileUnderMouse(out Tile tile)) return;

        Guardian data = selectedGuardian.GetComponent<Guardian>();
        if (data == null) return;

        if (!CanPlace(tile, data))
        {
            Debug.Log("วางไม่ได้");
            return;
        }

        if (!GameManager.instance.SpendMoney(data.cost))
        {
            Debug.Log("เงินไม่พอ");
            return;
        }

        Vector3 position = GetPlacementPosition(tile, data);

        GameObject guardian = Instantiate(
            selectedGuardian,
            position,
            Quaternion.Euler(0, 90, 0)
        );

        OccupyTiles(tile, data);
        guardian.GetComponent<Guardian>().SetTile(tile);

        // วางครั้งเดียวแล้วยกเลิกเลือก
        selectedGuardian = null;
        SelectGuardian.ClearSelection();
    }

    void TryRemoveGuardian()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Guardian guardian = hit.collider.GetComponentInParent<Guardian>();

            if (guardian != null)
            {
                guardian.RemoveSelf();
            }
        }
    }
public void ToggleRemoveMode() 
{ 
isRemoveMode = !isRemoveMode; 
if (removeButtonImage != null) 
{ 
removeButtonImage.color = isRemoveMode ? Color.red : Color.white; 
} 
}
    //public void ToggleRemoveMode()
    //{
        //isRemoveMode = !isRemoveMode;

        //if (isRemoveMode)
        //{
            //selectedGuardian = null;
            //SelectGuardian.ClearSelection();
       // }

       // if (removeButtonImage != null)
       // {
           // removeButtonImage.color = isRemoveMode ? Color.red : Color.white;
       // }
    //}

    Vector3 GetPlacementPosition(Tile startTile, Guardian data)
    {
        Vector3 center = GetCenterPosition(startTile, data);
        center = SnapToGround(center);
        center.y += GetHeightOffset(selectedGuardian);

        return center;
    }

    Vector3 GetCenterPosition(Tile startTile, Guardian data)
    {
        int endX = startTile.x + data.sizeX - 1;
        int endZ = startTile.z + data.sizeZ - 1;

        Tile endTile = GridManager.instance.GetTile(endX, endZ);

        if (endTile == null)
            return startTile.transform.position;

        return (startTile.transform.position + endTile.transform.position) / 2f;
    }

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
        return r != null ? r.bounds.extents.y : 0.5f;
    }

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

    void OccupyTiles(Tile startTile, Guardian data)
    {
        for (int x = 0; x < data.sizeX; x++)
        {
            for (int z = 0; z < data.sizeZ; z++)
            {
                Tile tile = GridManager.instance.GetTile(startTile.x + x, startTile.z + z);

                if (tile != null)
                    tile.isOccupied = true;
            }
        }
    }

    bool TryGetTileUnderMouse(out Tile tile)
    {
        tile = null;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 100f, tileLayer))
        {
            tile = hit.collider.GetComponent<Tile>();
            return tile != null;
        }

        return false;
    }

    void UpdatePreview()
    {
        if (previewInstance == null) return;

        if (selectedGuardian == null || isRemoveMode)
        {
            previewInstance.SetActive(false);
            return;
        }

        previewInstance.SetActive(true);

        if (!TryGetTileUnderMouse(out Tile tile)) return;

        Guardian data = selectedGuardian.GetComponent<Guardian>();

        Vector3 pos = GetPlacementPosition(tile, data);
        previewInstance.transform.position = pos;

        SetPreviewColor(CanPlace(tile, data));
    }

    void SetPreviewColor(bool canPlace)
    {
        Color color = canPlace ? Color.green : Color.red;

        foreach (Renderer r in previewInstance.GetComponentsInChildren<Renderer>())
        {
            r.material.color = color;
        }
    }
}