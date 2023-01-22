using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntGrid
{
    private int[,] gridArray;
    private int gridWidth;
    private int gridHeight;

    public IntGrid(int _gridWidth, int _gridHeight)
    {
        gridWidth = _gridWidth;
        gridHeight = _gridHeight;

        gridArray = new int[gridWidth, gridHeight];

        for (int y = 0; y < gridHeight; y++)
        {
            for (int x = 0; x < gridWidth; x++)
            {
                gridArray[x, y] = 0;
            }
        }
    }

    public int GetTile(int _x, int _y)
    {
        return gridArray[_x, _y];
    }
    public void ChangeTile(int _x, int _y, int value)
    {
        gridArray[_x, _y] = gridArray[_x, _y] + value;
    }

    public void SetTile(int _x, int _y, int _value)
    {
        if (_x >= 0 && _x <= gridWidth && _y >= 0 && _y <= gridHeight)
        {
            gridArray[_x, _y] = _value;
        }
        else
        {
            Debug.Log("Selection Out of bounds");
        }
    }

    public void SetTiles(int _x, int _y, int _x2, int _y2, int _value)
    {
        if (_x >= 0 && _x <= gridWidth && _y >= 0 && _y <= gridHeight && _x2 >= 0 && _x2 <= gridWidth && _y2 >= 0 && _y2 <= gridHeight)
        {
            for (int y = _y; y < _y2; y++)
            {
                for (int x = _x; x < _x2; x++)
                {
                    gridArray[x, y] = _value;
                }
            }
        }
        else
        {
            Debug.Log("Selection Out of bounds");
        }
    }
}