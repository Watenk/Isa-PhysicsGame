using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemperatureGrid : Grid
{
    public override void OnPhysicsUpdate()
    {
        TemperaturePhysics();
        base.OnPhysicsUpdate();
    }

    private void TemperaturePhysics()
    {
        bool[,] skipTiles = new bool[width, height];

        for (int y = 1; y < height - 1; y++)
        {
            for (int x = 1; x < width - 1; x++)
            {
                if (skipTiles[x, y] != true)
                {
                    int highestNumber = Mathf.Max(GetTile(x - 1, y), GetTile(x + 1, y), GetTile(x, y - 1), GetTile(x, y + 1));

                    if (highestNumber != GetTile(x, y - 1))
                    {
                        gridManager.MoveValue(this, x, y, 0, -1, skipTiles); //Move up
                    }
                    else if (highestNumber != GetTile(x, y + 1)) 
                    {
                        gridManager.MoveValue(this, x, y, 0, 1, skipTiles); //Move down
                    }
                    else if (highestNumber != GetTile(x + 1, y))
                    {
                        gridManager.MoveValue(this, x, y, 1, 0, skipTiles); //Move right
                    }
                    else if (highestNumber != GetTile(x - 1, y))
                    {
                        gridManager.MoveValue(this, x, y, -1, 0, skipTiles); //Move left
                    }
                }
            }
        }
    }
}
