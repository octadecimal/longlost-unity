using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "TileRule", menuName = "longlost/TileRule", order = 0)]
public class TileRule : ScriptableObject
{
    public TileType Type;
    public TileBase[] Tiles;
    public float[] Weights;

    public TileType North;
    public TileType NorthEast;
    public TileType East;
    public TileType SouthEast;
    public TileType South;
    public TileType SouthWest;
    public TileType West;
    public TileType NorthWest;

}