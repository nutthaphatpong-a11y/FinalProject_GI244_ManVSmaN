using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int rows = 5;
    public int cols = 9;
    public float spacing = 2f;

    public GameObject tilePrefab;

    void Start()
    {
        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                Vector3 pos = new Vector3(c * spacing, 0, r * spacing);
                Instantiate(tilePrefab, pos, Quaternion.identity, transform);
            }
        }
    }
}
