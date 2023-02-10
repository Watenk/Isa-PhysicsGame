using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;

public class GridManager : BaseClassLate
{
    //Grid Values
    public int GridWidth;
    public int GridHeight;
    public GameObject GridsGO;
    public List<Grid> grids;
    
    public override void OnStart()
    {
        grids = new List<Grid>();
        grids.AddRange(FindObjectsOfType<Grid>());

        GenerateWorld();
    }
    private void GenerateWorld()
    {

    }

    private void UpdatePhysics()
    {

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

    public Grid GetGrid(GridType gridType)
    {
        for (int i = 0; i < grids.Count; i++)
        {
            if (grids[i].GridType == gridType)
            {
                return grids[i];
            }
        }
        return null;
    }

    private bool IsTileAvailible(Grid currentGrid, int _x, int _y)
    {
        for (int i = 0; i < grids.Count; i++)
        {
            //Check if amount would fit in current grid
            if (currentGrid.GetTile(_x, _y) >= currentGrid.maxValue)
            {
                return false;
            }

            //Check if space is free in other grids
            if (i != (int)currentGrid.GridType) // skip currentGrid
            {
                if (grids[i].GetTile(_x, _y) != 0)
                {
                    return false;
                }
            }
        }
        return true;
    }
}