using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "TileRuleSet", menuName = "longlost/TileRuleSet", order = 0)]
public class TileRuleSet : ScriptableObject
{
    public TileRule[] Rules;

    public TileBase GetTile(int[,] grid, int x, int y)
    {
        return null;
    }

    private int GetNeighborType()
    {
        return 0;
    }
}