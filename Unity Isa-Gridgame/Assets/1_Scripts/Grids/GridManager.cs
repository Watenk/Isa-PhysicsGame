using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class GridManager : BaseClassLate
{
    //Grid Values
    public int GridWidth;
    public int GridHeight;
    public GameObject GridsGO;
    public Grid[] grids;
    private int gridsCount;
    
    public override void OnStart()
    {
        gridsCount = GridsGO.transform.childCount;
        grids = new Grid[gridsCount];

        //Add grids to gridsArray in order of Grids enum
        for (int i = 0; i < gridsCount; i++)
        {
            for (int j = 0; j < gridsCount; j++)
            {
                if (i == (int)GridsGO.transform.GetChild(j).GetComponent<Grid>().currentGrid)
                {
                    grids[i] = GridsGO.transform.GetChild(j).GetComponent<Grid>();
                    Debug.Log(grids[i]);
                }
            }
        }

        GenerateWorld();
    }

    public void MoveValue(Grid grid, int x, int y, int xMove, int yMove, bool[,] skipTiles)
    {
        if (IsTileAvailible(grid, x + xMove, y + yMove) && grid.GetTile(x, y) >= 1)
        {
            grid.ChangeTile(x, y, -1);
            grid.ChangeTile(x + xMove, y + yMove, 1);

            skipTiles[x, y] = true;
            skipTiles[x + xMove, y + yMove] = true;
        }
    }

    public void GenerateWorld()
    {

    }

    private bool IsTileAvailible(Grid currentGrid, int _x, int _y)
    {
        for (int i = 0; i < grids.Length; i++)
        {
            //Check if amount would fit in current grid
            if (currentGrid.GetTile(_x, _y) >= currentGrid.maxValue)
            {
                return false;
            }

            //Check if space is free in other grids
            if (i != (int)currentGrid.currentGrid) // skip currentGrid
            {
                if (grids[i].GetTile(_x, _y) != 0)
                {
                    return false;
                }
            }
        }
        return true;
    }

    private int CalcPercentage(float percentage)
    {
        percentage /= 100;
        return (int)(Mathf.Round(GridHeight * percentage));
    }
}