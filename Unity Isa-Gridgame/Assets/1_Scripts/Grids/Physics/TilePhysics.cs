using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilePhysics : BaseClass
{
    public int TempDifferenceMagnitude; //The higher the less temp difference makes a difference

    private Dictionary<ID, PhysicsTile> PhysicTiles = new Dictionary<ID, PhysicsTile>();
    private HashSet<Vector2Int> skipTiles = new HashSet<Vector2Int>();

    private Tile currentTile;
    private PhysicsTile currentPhysics;

    private MainGrid mainGrid;

    public override void OnAwake()
    {
        mainGrid = FindObjectOfType<MainGrid>();
    }

    public override void OnStart()
    {
        //Solid tiles dictionary
        PhysicTiles.Add(ID.grass, new PhysicsTile(ID.grass, 20, true, 5, 9, 50000, ID.dirt, 5));
        PhysicTiles.Add(ID.dirt, new PhysicsTile(ID.dirt, 20, true, 5, 9, 2000000, ID.none, 5));
        PhysicTiles.Add(ID.stone, new PhysicsTile(ID.stone, 20, false, 9, 9, 5000000, ID.none, 1));
    }

    public override void OnUPS()
    {
        Physics();
    }

    private void Physics()
    {
        //Check entire grid (probably inefficient)
        for (int y = mainGrid.Height - 1; y >= 1 ; y--)
        {
            for (int x = 1; x < mainGrid.Width - 1; x++)
            {
                //Check if should apply physics to this tile
                currentTile = mainGrid.GetTile(new Vector2Int(x, y));
                if (!skipTiles.Contains(currentTile.pos) && PhysicTiles.ContainsKey(currentTile.id))
                {
                    //Check if should update tile
                    PhysicTiles.TryGetValue(currentTile.id, out currentPhysics);
                    if (currentPhysics.currentUpdate < currentPhysics.updateSpeed)
                    {
                        //Increase currentUpdate and no physics
                        currentPhysics.currentUpdate += 1;
                    }
                    else
                    {
                        //Physics
                        Tile upTile = mainGrid.GetTile(new Vector2Int(x, y - 1));
                        Tile downTile = mainGrid.GetTile(new Vector2Int(x, y + 1));
                        Tile rightTile = mainGrid.GetTile(new Vector2Int(x + 1, y));
                        Tile leftTile = mainGrid.GetTile(new Vector2Int(x - 1, y));

                        TempPhysics(upTile, downTile, rightTile, leftTile);

                        if (currentPhysics.hasGravity)
                        {
                            GravityPhysics(downTile);
                        }
                    }
                }
            }
        }
        skipTiles.Clear();
    }

    private void TempPhysics(Tile upTile, Tile downTile, Tile rightTile, Tile leftTile)
    {
        //UpTile
        if (upTile.id != ID.none)
        {
            PhysicTiles.TryGetValue(upTile.id, out PhysicsTile upTilePhysics);
            if (upTilePhysics != null)
            {
                CalcTemp(upTile, upTilePhysics);
            }
        }

        //DownTile
        if (downTile.id != ID.none)
        {
            PhysicTiles.TryGetValue(downTile.id, out PhysicsTile downTilePhysics);
            if (downTilePhysics != null)
            {
                CalcTemp(downTile, downTilePhysics);
            }
        }

        //RightTile
        if (rightTile.id != ID.none)
        {
            PhysicTiles.TryGetValue(rightTile.id, out PhysicsTile rightTilePhysics);
            if (rightTilePhysics != null)
            {
                CalcTemp(rightTile, rightTilePhysics);
            }
        }

        //LeftTile
        if (leftTile.id != ID.none)
        {
            PhysicTiles.TryGetValue(leftTile.id, out PhysicsTile leftTilePhysics);
            if (leftTilePhysics != null)
            {
                CalcTemp(leftTile, leftTilePhysics);
            }
        }

        //MaxTemp
        if (currentTile.temp >= currentPhysics.maxTemp)
        {
            mainGrid.SetTile(currentTile.pos, currentPhysics.ifMaxTemp, currentTile.amount, currentTile.temp);
        }
    }

    private void CalcTemp(Tile targetTile, PhysicsTile targetPhysics)
    {
        int tempDifference = Mathf.Abs(currentTile.temp - targetTile.temp) / TempDifferenceMagnitude;
        if (currentTile.temp > targetTile.temp)
        {
            if (currentPhysics.thermalConductivity > targetPhysics.thermalConductivity)
            {
                currentTile.temp -= targetPhysics.thermalConductivity + tempDifference;
                targetTile.temp += targetPhysics.thermalConductivity + tempDifference;
            }
            else
            {
                currentTile.temp -= currentPhysics.thermalConductivity + tempDifference;
                targetTile.temp += currentPhysics.thermalConductivity + tempDifference;
            }
        }
        else
        {
            if (currentPhysics.thermalConductivity > targetPhysics.thermalConductivity)
            {
                currentTile.temp += targetPhysics.thermalConductivity + tempDifference;
                targetTile.temp -= targetPhysics.thermalConductivity + tempDifference;
            }
            else
            {
                currentTile.temp += currentPhysics.thermalConductivity + tempDifference;
                targetTile.temp -= currentPhysics.thermalConductivity + tempDifference;
            }
        }
    }

    private void GravityPhysics(Tile downTile)
    {
        if (mainGrid.MoveTile(currentTile, downTile, currentPhysics.maxAmount, currentPhysics.speed))
        {
            skipTiles.Add(downTile.pos);
        }
    }
}