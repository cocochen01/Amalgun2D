using UnityEngine;
using UnityEngine.Tilemaps;

public enum TileType
{
    Empty,
    Wall,
    Floor,
    Water,
    Lava
}
[CreateAssetMenu(fileName = "TerrainRegistry", menuName = "World/Terrain Registry")]
public class TerrainRegistry : ScriptableObject
{
    public TerrainTile[] terrains;

    public TileType DefaultWallType = TileType.Wall;
    public TileType DefaultFloorType = TileType.Floor;

    private static TerrainRegistry _instance;
    public static TerrainRegistry Instance
    {
        get
        {
            if (_instance == null)
                _instance = Resources.Load<TerrainRegistry>("TerrainRegistry");
            return _instance;
        }
    }

    public TerrainTile Get(TileType type)
    {
        foreach (var t in terrains)
            if (t.type == type)
                return t;
        return null;
    }
}

[System.Serializable]
public class TerrainTile
{
    public TileType type;
    public TileBase tile;
    public Color debugColor;
    public bool isWalkable;
}
