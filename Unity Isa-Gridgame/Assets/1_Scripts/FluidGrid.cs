using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FluidGrid : Grid
{
    public int waterUpdates; //Per Second
    private int updateTimer;

    public override void OnPhysicsUpdate()
    {
        if (updateTimer >= 60 / waterUpdates)
        {
            FluidPhysics();
        }

        updateTimer += 1;
    }

    private void FluidPhysics()
    {
        for (int y = height - 1; y >= 1; y--)
        {
            for (int x = 1; x < width - 1; x++)
            {
                if (GetTile(x, y) >= 1) // if there is water
                {
                    gridManager.MoveValue(this, x, y, 0, 1); //Move Down
                    
                    //Move to the lowest value neighbour
                    int highestNumber = Mathf.Max(GetTile(x - 1, y), GetTile(x + 1, y));
                    if (highestNumber == GetTile(x - 1, y))
                    {
                        gridManager.MoveValue(this, x, y, 1, 0); //Move Right
                    }
                    else
                    {
                        gridManager.MoveValue(this, x, y, -1, 0); //Move Left
                    }
                }
            }
        }
    }
}
