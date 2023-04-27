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

        InitializeGrid();
        gridRenderer.Draw();
        PrintGridSize();
    }

    //---------------------------------------------------------------------

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
            currentTile.id = id;
            currentTile.amount = amount;
            currentTile.temp = temp;
        }
        else
        {
            Debug.Log("SetTile Out of Bounds: " + pos.x + ", " + pos.y);
        }
    }

    public void SetTiles(Vector2Int pos1, Vector2Int pos2, ID id, int amount, int temp)
    {
        if (IsInGridBounds(pos1) && IsInGridBounds(pos2))
        {
            for (int y = pos1.y; y < pos2.y; y++)
            {
                for (int x = pos1.x; x < pos2.x; x++)
                {
                    SetTile(new Vector2Int(x, y), id, amount, temp);
                }
            }
        }
        else
        {
            Debug.Log("SetTiles Out of Bounds: " + pos1.x + ", " + pos1.y + " & " + pos2.x + ", " + pos2.y);
        }
    }

    public bool MoveTile(Tile currentTile, Tile targetTile, int maxAmount, int speed)
    {
        if (targetTile.id == ID.none || currentTile.id == targetTile.id && targetTile.amount < maxAmount)
        {
            //Calc MoveAmount
            int maxMoveAmount = maxAmount - targetTile.amount;
            int moveAmount;

            if (speed > maxMoveAmount)
            {
                moveAmount = maxMoveAmount;
            }
            else
            {
                moveAmount = speed;
            }

            if (moveAmount > currentTile.amount)
            {
                moveAmount = currentTile.amount;
            }

            //Set TargetTile
            if (targetTile.id == ID.none)
            {
                SetTile(targetTile.pos, currentTile.id, targetTile.amount + moveAmount, currentTile.temp);
            }
            else
            {
                SetTile(targetTile.pos, currentTile.id, targetTile.amount + moveAmount, (currentTile.temp + targetTile.temp) / 2);
            }

            //Set CurrentTile
            if (currentTile.amount - moveAmount <= 0)
            {
                ClearTile(currentTile.pos);
            }
            else
            {
                currentTile.amount -= moveAmount;
            }
            return true;
        }
        return false;
    }

    public void ClearTile(Vector2Int pos)
    {
        if (IsInGridBounds(pos))
        {
            SetTile(pos, ID.none, 0, 0);
        }
    }

    public bool IsInGridBounds(Vector2Int pos)
    {
        if (pos.x >= 0 && pos.x <= Width - 1 && pos.y >= 0 && pos.y <= Height - 1)
        {
            return true;
        }
        return false;
    }

    //-----------------------------------------------------------

    private void InitializeGrid()
    {
        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                gridArray[x, y] = new Tile(ID.none, new Vector2Int(x, y), 0, 0);
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