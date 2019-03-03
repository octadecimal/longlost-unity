using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "TileRuleSet", menuName = "longlost/TileRuleSet", order = 0)]
public class TileRuleSet : ScriptableObject
{
    public TileRule[] Rules;

}