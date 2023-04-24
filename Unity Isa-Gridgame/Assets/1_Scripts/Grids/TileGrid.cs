using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGrid : BaseClass
{
    public TileGridRenderer gridRenderer;

    //Wouldn't recommend anything above 250k tiles
    public int Width; 
    public int Height;

    protected Tile[,] gridArray;

    public override void OnStart()
    {
        gridArray = new Tile[Width, Height];

        FillGridWithTiles();
        gridRenderer.Draw();
        PrintGridSize();
    }

    public Tile GetTile(Vector2Int pos) 
    {
        if (IsInGridBounds(pos))
        {
            return gridArray[pos.x, pos.y]; 
        }
        return null;
    }

    public void SetTile(Vector2Int pos, ID id, int amount, int temp) 
    { 
        if (IsInGridBounds(pos))
        {
            Tile currentTile = GetTile(pos);
            currentTile.SetID(id);
            currentTile.SetAmount(amount);
            currentTile.SetTemp(temp);
        }
        else
        {
            Debug.Log("SetTile Out of Bounds: " + pos.x + ", " + pos.y);
        }
    }

    public void SetTiles(Vector2Int pos1, Vector2Int pos2, ID id, int amount, int temp)
    {
        for (int y = pos1.y; y < pos2.y; y++)
        {
            for (int x = pos1.x; x < pos2.x; x++)
            {
                SetTile(new Vector2Int(x, y), id, amount, temp);
            }
        }
    }

    public bool IsTileAvailible(Vector2Int pos)
    {
        //ID currentTileID = GetTile(pos).GetID();
        
        return true;
    }

    public bool AreTilesAvailible(Vector2Int pos1, Vector2Int pos2)
    {
        if (IsInGridBounds(pos1) && IsInGridBounds(pos2))
        {
            for (int y = pos1.y; y < pos2.y; y++)
            {
                for (int x = pos1.x; x < pos2.x; x++)
                {
                    if (IsTileAvailible(new Vector2Int(x, y)) == false)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        return false;
    }

    public Tile FindRandomFreeSpace(Vector2Int pos1, Vector2Int pos2)
    {
        if (IsInGridBounds(pos1) && IsInGridBounds(pos2))
        {
            int retrys = 0;
            retry:
            int x = Random.Range(pos1.x, pos2.x);
            int y = Random.Range(pos1.y, pos2.y);

            if (IsTileAvailible(new Vector2Int(x, y)))
            {
                return GetTile(new Vector2Int(x, y));
            }

            retrys++;
            if (retrys > 100)
            {
                return null;
            }
            goto retry;
        }
        return null;
    }

    public bool IsInGridBounds(Vector2Int pos)
    {
        if (pos.x >= 0 && pos.x < Width && pos.y >= 0 && pos.y < Height)
        {
            return true;
        }
        return false;
    }

    private void FillGridWithTiles()
    {
        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                gridArray[x, y] = new Tile(ID.none, new Vector2Int(x, y));
            }
        }
    }

    private void PrintGridSize()
    {
        if (gridArray.Length < 1000000)
        {
            Debug.Log(gameObject.name + " size: " + gridArray.Length / 1000 + "K");
        }
        else
        {
            Debug.Log(gameObject.name + " size: " + gridArray.Length / 1000000 + "M");
        }
    }
}