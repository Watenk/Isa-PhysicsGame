using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class TilePhysics : BaseClass
{
    public Dictionary<ID, SolidPhysicsTile> SolidPhysicTiles = new Dictionary<ID, SolidPhysicsTile>();

    private MainGrid mainGrid;

    public override void OnAwake()
    {
        mainGrid = FindObjectOfType<MainGrid>();
    }

    public override void OnStart()
    {
        //Solid
        SolidPhysicTiles.Add(ID.grass, new SolidPhysicsTile(ID.grass, 20, 50, ID.dirt));
        SolidPhysicTiles.Add(ID.dirt, new SolidPhysicsTile(ID.dirt, 20, 2000, ID.none));
        SolidPhysicTiles.Add(ID.stone, new SolidPhysicsTile(ID.stone, 20, 5000, ID.none));
    }

    public override void OnUPS()
    {
        SolidPhysics();
    }

    private void SolidPhysics()
    {
        for (int y = 0; y < mainGrid.Height; y++)
        {
            for (int x = 0; x < mainGrid.Width; x++)
            {
                Tile currentTile = mainGrid.GetTile(new Vector2Int(x, y));
                //if (solidPhysicsIDs.Contains(currentTile.GetID()))
                //{

                //}
            }
        }
    }
}