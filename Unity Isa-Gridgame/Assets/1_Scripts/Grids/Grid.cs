using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class Grid : BaseClass
{
    public TilesDictionary TilesDictionary;
    public int Width;
    public int Height;
    public ID WallTile;
    public ID FloorTile;

    private TilesDictionary tiledict;

    private Tile[,] gridArray;

    public override void OnStart()
    {
        gridArray = new Tile[Width, Height];
        GenerateGrid();
        Debug.Log("Grid size: " + gridArray.Length / 1000 + "K");
    }

    public Tile GetTile(int x, int y) { return gridArray[x, y]; }

    public void SetTile(int x, int y, ID id, int amount, int temperature) 
    { 
        if (x >= 0 && x <= Width && y >= 0 && y <= Height)
        {
            Tile currentTile = GetTile(x, y);
            currentTile.SetID(id);
            currentTile.SetAmount(amount);
            currentTile.SetTemperature(temperature);
        }
        else
        {
            Debug.Log("SetTile Out of Bounds: " + x + ", " + y);
        }
    }

    public void SetTiles(int x1, int y1, int x2, int y2, ID id, int amount, int temperature)
    {
        for (int y = y1; y < y2; y++)
        {
            for (int x = x1; x < x2; x++)
            {
                SetTile(x, y, id, amount, temperature);
            }
        }
    }

    public void MoveTile(Tile currentTile, int x, int y)
    {
        if (IsThereSpaceOnTile(currentTile, x, y))
        {

        }
    }

    public bool IsThereSpaceOnTile(Tile currentTile, int x, int y)
    {
        //Is the targetTile empty or the target and current tile are the same and is there room
        ID targetID = GetTile(x, y).GetID();
        if (targetID == ID.none)
        {
            return true;
        }

        ID currentID = currentTile.GetID();
        int targetAmount = GetTile(x, y).GetAmount();
        if (targetID == currentID && targetAmount + 1 <= TilesDictionary.GetValue(currentID, IDProperties.maxValue))
        {
            return true;
        }
        return false;
    }

    private void GenerateGrid()
    {
        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                gridArray[x, y] = new Tile(ID.none, 0, 0);
            }
        }

        SetTiles(0, Height - 20, Width, Height, FloorTile, 1, 20); //Floor
        SetTiles(0, 0, 1, Height, WallTile, 1, 20); //Left wall
        SetTiles(Width - 1, 0, Width, Height, WallTile, 1, 20); //Right wall
        SetTiles(0, 0, Width, 1, WallTile, 1, 20); //Ceiling
    }
}