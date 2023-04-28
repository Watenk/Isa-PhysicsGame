using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGrid : TileGrid
{
    public override void OnStart()
    {
        base.OnStart();

        SetTiles(new Vector2Int(0, 0), new Vector2Int(1, Height - 1), ID.black, 0, 0); // Left wall
        SetTiles(new Vector2Int(Width - 2, 0), new Vector2Int(Width - 1, Height - 1), ID.black, 0, 0); // Right Wall
        SetTiles(new Vector2Int(0, Height - 2), new Vector2Int(Width - 1, Height - 1), ID.black, 0, 0); // Floor
        SetTiles(new Vector2Int(0, 0), new Vector2Int(Width - 1, 1), ID.black, 0, 0); // Ceiling

        SetTiles(new Vector2Int(10, 10), new Vector2Int(Width - 10, Height - 10), ID.oxygen, 5, 20000); //Oxygen
    }
}
