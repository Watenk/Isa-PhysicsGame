using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilePhysics : BaseClass
{
    private Dictionary<ID, SolidPhysics> solidPhysicTiles = new Dictionary<ID, SolidPhysics>();
    public HashSet<Vector2Int> skipTiles = new HashSet<Vector2Int>();

    private MainGrid mainGrid;

    public override void OnAwake()
    {
        mainGrid = FindObjectOfType<MainGrid>();
    }

    public override void OnStart()
    {
        //Solid tiles dictionary
        solidPhysicTiles.Add(ID.grass, new SolidPhysics(ID.grass, 20, true, 9, 9, 50, ID.dirt));
        solidPhysicTiles.Add(ID.dirt, new SolidPhysics(ID.dirt, 20, true, 9, 9, 2000, ID.none));
        solidPhysicTiles.Add(ID.stone, new SolidPhysics(ID.stone, 20, false, 9, 9, 5000, ID.none));
    }

    public override void OnUPS()
    {
        Physics();
    }

    private void Physics()
    {
        //Check entire grid (probably inefficient)
        for (int y = 0; y < mainGrid.Height; y++)
        {
            for (int x = 0; x < mainGrid.Width; x++)
            {
                //Solid Physics
                //Check if should apply physics to this tile
                Tile currentTile = mainGrid.GetTile(new Vector2Int(x, y));
                if (!skipTiles.Contains(currentTile.pos) && solidPhysicTiles.ContainsKey(currentTile.id))
                {
                    //Check if should update tile
                    solidPhysicTiles.TryGetValue(currentTile.id, out SolidPhysics currentPhysics);
                    if (currentPhysics.currentUpdate < currentPhysics.updateSpeed)
                    {
                        //Increase currentUpdate and no physics
                        currentPhysics.currentUpdate += 1;
                    }
                    else
                    {
                        //Do Physics
                        //Get Neighbours
                        Tile downTile = mainGrid.GetTile(new Vector2Int(x, y + 1));

                        //Gravity
                        if (currentPhysics.hasGravity && downTile != null)
                        {
                            if (mainGrid.MoveTile(currentTile, downTile, currentPhysics.maxAmount, currentPhysics.maxAmount))
                            {
                                skipTiles.Add(downTile.pos);
                            }
                        }
                    }
                }

                //Liquid physics


            }
        }
        skipTiles.Clear();
    }
}