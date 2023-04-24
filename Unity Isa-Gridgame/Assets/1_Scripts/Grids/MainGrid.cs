using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGrid : TileGrid
{
    public override void OnStart()
    {
        base.OnStart();

        SetTiles(new Vector2Int(0, 0), new Vector2Int(1, Height), ID.dirt, 1, 20); // Left wall
        SetTiles(new Vector2Int(Width - 1, 0), new Vector2Int(Width, Height), ID.dirt, 1, 20); // Right Wall
        SetTiles(new Vector2Int(0, Height - 1), new Vector2Int(Width, Height), ID.dirt, 1, 20); // Floor
        SetTiles(new Vector2Int(0, 0), new Vector2Int(Width, 1), ID.dirt, 1, 20); // Ceiling
    }
}
