using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : BaseClass
{
    public int Width;
    public int Height;
    public TileID Floor;

    private Tile[,] gridArray;

    public override void OnStart()
    {
        GenerateGrid();
        Debug.Log("Grid size: " + gridArray.Length / 1000 + "K");
    }

    public Tile GetTile(int x, int y)
    {
        return gridArray[x, y];
    }

    private void GenerateGrid()
    {
        gridArray = new Tile[Width, Height];

        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                gridArray[x, y] = new Tile(TileID.none, 1, 20);
            }
        }

        //Generatefloor here
    }
}