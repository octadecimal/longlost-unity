using UnityEngine;
using UnityEngine.Tilemaps;

public class TileGrid : MonoBehaviour
{
    public int Width;
    public int Height;
    public TileRuleSet RuleSet;
    public bool Debug;

    private TileType[,] _cells;

    public void Start()
    {
        _cells = new TileType[Width, Height];

        Fill(TileType.Field, 0, 0, Width, Height);
        // Fill(TileType.Road, 5, 0, 2, Height);
        // Fill(TileType.Road, 0, 4, 12, 2);

        Fill(TileType.Road, 2, 2, 8, 12);
        Fill(TileType.Field, 4, 4, 4, 8);
        Fill(TileType.Road, 5, 0, 2, Height);
        Fill(TileType.Road, 0, 6, 12, 2);
        Paint();
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Paint();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug = !Debug;
            Paint();
        }
    }

    public void Set()
    {

    }

    public void Fill(TileType type, int x, int y, int width, int height)
    {
        for (int j = 0; j < height; j++)
        {
            for (int i = 0; i < width; i++)
            {
                _cells[x + i, y + j] = type;
            }
        }
    }

    public void Paint()
    {
        Tilemap map = GetComponent<Tilemap>();

        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                map.SetTile(new Vector3Int(x, y, 0), GetTile(x, y));
            }
        }
    }

    public TileBase GetTile(int x, int y)
    {
        TileRule bestRule = null;
        int bestScore = 0;

        // Get type at target
        TileType type = _cells[x, y];

        // Get tile with matching rule
        foreach (TileRule rule in RuleSet.Rules)
        {
            if (rule.Type == type)
            {
                int score = ScoreRule(rule, x, y);
                if (score > bestScore)
                {
                    bestRule = rule;
                    bestScore = score;
                }
            }
        }

        // Return best scoring rule
        if (bestRule)
        {
            float index = Debug ? 0 : Mathf.Pow(Random.Range(0f, 1f), 5f) * bestRule.Tiles.Length;
            return bestRule.Tiles[(int)index];
        }
        else
        {
            return null;
        }
    }

    private int ScoreRule(TileRule rule, int x, int y)
    {
        int score = 0;
        score += ScoreRuleDirection(rule.North, x, y, 0, 1);
        score += ScoreRuleDirection(rule.NorthEast, x, y, 1, 1);
        score += ScoreRuleDirection(rule.East, x, y, 1, 0);
        score += ScoreRuleDirection(rule.SouthEast, x, y, 1, -1);
        score += ScoreRuleDirection(rule.South, x, y, 0, -1);
        score += ScoreRuleDirection(rule.SouthWest, x, y, -1, -1);
        score += ScoreRuleDirection(rule.West, x, y, -1, 0);
        score += ScoreRuleDirection(rule.NorthWest, x, y, -1, 1);

        return score;
    }

    private int ScoreRuleDirection(TileType matchType, int x, int y, int offsetX, int offsetY)
    {
        TileType neighborType = GetNeighborType(x, y, offsetX, offsetY);
        bool pass = neighborType == matchType || neighborType == TileType.Any || matchType == TileType.Any;

        if (Debug)
        {
            if (matchType == TileType.Any)
            {
                return 100;
            }
        }

        if (neighborType == matchType)
        {
            return 10;
        }

        if (neighborType == TileType.Any || matchType == TileType.Any)
        {
            return 1;
        }

        return 0;
    }

    private TileType GetNeighborType(int x, int y, int offsetX, int offsetY)
    {
        // Return any if out of bounds
        if (x + offsetX < 0 || x + offsetX >= Width || y + offsetY < 0 || y + offsetY >= Height)
        {
            return TileType.Any;
        }

        // Return in-bounds neighbor
        return _cells[x + offsetX, y + offsetY];
    }
}