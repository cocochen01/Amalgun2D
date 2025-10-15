using UnityEngine;
using UnityEngine.Tilemaps;
using static TerrainRegistry;
public class Stage1Generator : MonoBehaviour
{
    [Header("Tilemap References")]
    public Tilemap tilemap;

    [Header("Map Settings")]
    public int width = 80;
    public int height = 80;
    public int stepsPerWalker = 300;
    public float initialRadius = 6f;
    public float finalRadius = 2f;

    [Range(0f, 1f)]
    public float turnChance = 0.3f;
    public float turnAngleVariance = 30f;
    public float directionSeparation = 180f;

    private TileType[,] mapData;

    [ContextMenu("Generate Map")]
    public void GenerateMap()
    {
        mapData = new TileType[width, height];
        FillWithDefault(TerrainRegistry.Instance.DefaultWallType);

        Vector2 center = new Vector2(width / 2f, height / 2f);

        RenderMap();
    }

    void FillWithDefault(TileType type)
    {
        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
                mapData[x, y] = type;
    }


    void RenderMap()
    {
        tilemap.ClearAllTiles();

        var registry = TerrainRegistry.Instance;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var tileType = mapData[x, y];
                TerrainTile terrain = registry.Get(tileType);

                if (terrain == null) continue;

                Vector3Int tilePos = new Vector3Int(x - width / 2, y - height / 2, 0);
                tilemap.SetTile(tilePos, terrain.tile);
            }
        }
    }

    private class Walker
    {
        public Vector2 position;
        public float angle;

        public Walker(Vector2 pos, float ang)
        {
            position = pos;
            angle = ang;
        }

        public Vector2 Forward()
        {
            float rad = angle * Mathf.Deg2Rad;
            return new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
        }
    }
}
