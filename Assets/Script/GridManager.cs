using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager instance;

    public int width = 9;
    public int height = 5;

    public float tileSize = 1.2f;

    public GameObject tilePrefab;

    public Tile[,] grid;


    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        grid = new Tile[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                Vector3 pos = new Vector3(x * tileSize, 0, z * tileSize);

                GameObject tileObj = Instantiate(tilePrefab, pos, Quaternion.identity, transform);

                Tile tile = tileObj.GetComponent<Tile>();

                tile.x = x;
                tile.z = z;

                grid[x, z] = tile;
            }
        }
    }

    public Tile GetTile(int x, int z)
    {
        if (x < 0 || x >= width || z < 0 || z >= height)
            return null;

        return grid[x, z];
    }
}