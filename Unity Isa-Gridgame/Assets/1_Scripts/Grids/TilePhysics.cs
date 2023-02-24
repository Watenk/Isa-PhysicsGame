using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilePhysics : BaseClass
{
    public Grid Grid;
    public TilesDictionary TilesDictionary;

    public override void OnPhysicsUpdate()
    {
        for (int y = 0; y < Grid.Height; y++)
        {
            for (int x = 0; x < Grid.Width; x++)
            {
                //Apply liquid physics
                if (TilesDictionary.GetValue(Grid.GetTile(x, y).GetID(), IDProperties.isLiquid) == 1)
                {

                }
            }
        }
    }
}