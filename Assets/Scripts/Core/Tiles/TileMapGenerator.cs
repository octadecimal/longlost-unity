using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMapGenerator : MonoBehaviour
{
    [Range(0.0f, 1.0f)]
    public float HillThreshold;

    [Range(0.0f, 1.0f)]
    public float FieldThreshold;

    [Range(0.0f, 1.0f)]
    public float WaterThreshold;

    [Range(0.0f, 1.0f)]
    public float MountainThreshold;

    private TileGrid _tileGrid;

    public void Start()
    {
        this._tileGrid = GetComponent<TileGrid>();

    }

    public void Generate()
    {
        this._tileGrid = GetComponent<TileGrid>();

        float scale = 8f;
        float size = Mathf.Max(_tileGrid.Width, _tileGrid.Height);

        for (int y = 0; y < _tileGrid.Height; y += 2)
        {

            for (int x = 0; x < _tileGrid.Width; x += 2)
            {
                float px = x / size * scale;
                float py = y / size * scale;
                float sample = Mathf.PerlinNoise(px, py);

                if (sample < HillThreshold)
                {
                    FillGroupedCell(TileType.Hill, x, y);
                }
                else if (sample < FieldThreshold)
                {
                    FillGroupedCell(TileType.Field, x, y);
                }
                else if (sample < WaterThreshold)
                {
                    FillGroupedCell(TileType.Water, x, y);
                }
            }
        }
    }

    private void FillGroupedCell(TileType type, int x, int y)
    {
        _tileGrid.Set(type, x, y);
        _tileGrid.Set(type, x + 1, y);
        _tileGrid.Set(type, x, y + 1);
        _tileGrid.Set(type, x + 1, y + 1);
    }
}
