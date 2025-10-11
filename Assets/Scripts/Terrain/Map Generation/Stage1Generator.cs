using UnityEngine;
using UnityEngine.Tilemaps;

public class Stage1Generator : MonoBehaviour
{
    [Header("Tilemap References")]
    public Tilemap tilemap;
    public TileBase floorTile;
    public TileBase wallTile;

    [Header("Map Settings")]
    public int width = 80;
    public int height = 80;
    public int stepsPerWalker = 300;
    public float initialRadius = 6f;
    public float finalRadius = 2f;

    [Range(0f, 1f)]
    public float turnChance = 0.3f; // chance each step to slightly change angle
    public float turnAngleVariance = 30f; // degrees of possible turn
    public float directionSeparation = 180f; // roughly opposite

    private bool[,] mapData;

    void Start()
    {
        GenerateMap();
    }

    void GenerateMap()
    {
        mapData = new bool[width, height];
        Vector2 center = new Vector2(width / 2f, height / 2f);

        // Two walkers going in opposite directions
        Walker walkerA = new Walker(center, Random.Range(0f, 360f));
        Walker walkerB = new Walker(center, walkerA.angle + directionSeparation + Random.Range(-30f, 30f));

        SimulateWalker(walkerA);
        SimulateWalker(walkerB);

        RenderMap();
    }

    void SimulateWalker(Walker walker)
    {
        for (int step = 0; step < stepsPerWalker; step++)
        {
            // Calculate how far along the walker is
            float t = (float)step / stepsPerWalker;

            // Interpolate radius from initial → final
            float radius = Mathf.Lerp(initialRadius, finalRadius, t);

            // Carve area
            CarveCircle(walker.position, radius);

            // Move forward
            walker.position += walker.Forward();

            // Randomly turn
            if (Random.value < turnChance)
            {
                float turn = Random.Range(-turnAngleVariance, turnAngleVariance);
                walker.angle += turn;
            }

            // Keep inside map bounds
            walker.position.x = Mathf.Clamp(walker.position.x, 1, width - 2);
            walker.position.y = Mathf.Clamp(walker.position.y, 1, height - 2);
        }
    }

    void CarveCircle(Vector2 center, float radius)
    {
        int minX = Mathf.Max(0, Mathf.FloorToInt(center.x - radius));
        int maxX = Mathf.Min(width - 1, Mathf.CeilToInt(center.x + radius));
        int minY = Mathf.Max(0, Mathf.FloorToInt(center.y - radius));
        int maxY = Mathf.Min(height - 1, Mathf.CeilToInt(center.y + radius));

        for (int x = minX; x <= maxX; x++)
        {
            for (int y = minY; y <= maxY; y++)
            {
                float dx = x - center.x;
                float dy = y - center.y;
                if (dx * dx + dy * dy <= radius * radius)
                    mapData[x, y] = true;
            }
        }
    }

    void RenderMap()
    {
        tilemap.ClearAllTiles();

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3Int tilePos = new Vector3Int(x - width / 2, y - height / 2, 0);
                tilemap.SetTile(tilePos, mapData[x, y] ? floorTile : wallTile);
            }
        }
    }

    // Internal class for each tunneling walker
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

#if UNITY_EDITOR
    void OnDrawGizmosSelected()
    {
        if (mapData == null) return;

        Gizmos.color = Color.gray;
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (mapData[x, y])
                    Gizmos.color = Color.green;
                else
                    Gizmos.color = Color.black;

                Gizmos.DrawCube(new Vector3(x - width / 2, y - height / 2, 0), Vector3.one * 0.9f);
            }
        }
    }
#endif
}
