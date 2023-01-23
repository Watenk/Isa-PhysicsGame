using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    //Grid Values
    public int GridWidth;
    public int GridHeight;

    //Grids
    private Grid[] grids;
    public GameObject DirtGridGO;
    [HideInInspector]
    public Grid dirtGrid;
    public GameObject WaterGridGO;
    [HideInInspector]
    public Grid waterGrid;

    public void OnAwake()
    {
        dirtGrid = DirtGridGO.GetComponent<Grid>();
        waterGrid = WaterGridGO.GetComponent<FluidGrid>();
    }

    public void OnStart()
    {
        //Grids Index
        grids = new Grid[2];
        SetGridIndex(dirtGrid, 0);
        SetGridIndex(waterGrid, 1);

        //Generate world
        dirtGrid.SetTiles(0, GridHeight - 10, GridWidth, GridHeight, 1); //Ground
        dirtGrid.SetTiles(0, 0, 1, GridHeight, 1); //Left Wall
        dirtGrid.SetTiles(GridWidth - 1, 0, GridWidth, GridHeight, 1); //Right Wall
    }

    public void OnPhysicsUpdate()
    {
        dirtGrid.Draw();
        waterGrid.Draw();
    }

    public void MoveValue(Grid grid, int x, int y, int xMove, int yMove)
    {
        if (IsTileAvailible(grid, x + xMove, y + yMove) && grid.GetTile(x, y) >= 1)
        {
            grid.ChangeTile(x, y, -1);
            grid.ChangeTile(x + xMove, y + yMove, 1);
        }
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
            if (i != currentGrid.Index) // skip currentGrid
            {
                if (grids[i].GetTile(_x, _y) != 0)
                {
                    return false;
                }
            }
        }
        return true;
    }

    private void SetGridIndex(Grid currentGrid, int index)
    {
        currentGrid.Index = index;
        grids[index] = currentGrid;
    }
}