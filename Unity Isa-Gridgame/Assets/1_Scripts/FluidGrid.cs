using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FluidGrid : Grid
{
    public override void OnPhysicsUpdate()
    {
        FluidPhysics();
        base.OnPhysicsUpdate();
    }

    private void FluidPhysics()
    {
        bool[,] skipTiles = new bool[width, height];

        for (int y = 1; y < height - 1; y++)
        {
            for (int x = 1; x < width - 1; x++)
            {
                if (GetTile(x, y) >= 1 && skipTiles[x, y] != true) // if there is water
                {
                    gridManager.MoveValue(this, x, y, 0, 1, skipTiles); //Move Down
                    
                    //Move to the lowest value neighbour
                    int highestNumber = Mathf.Max(GetTile(x - 1, y), GetTile(x + 1, y));
                    if (highestNumber == GetTile(x - 1, y))
                    {
                        gridManager.MoveValue(this, x, y, 1, 0, skipTiles); //Move Right
                    }
                    else
                    {
                        gridManager.MoveValue(this, x, y, -1, 0, skipTiles); //Move Left
                    }
                }
            }
        }
    }
}
