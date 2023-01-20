using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : BaseClass
{
    public GameObject[] SolidTiles;
    public GameObject WaterTile;
    public int GridWidth;
    public int GridHeight;
    public int xStartLocation;
    public int yStartLocation;

    private GameObject[,] midLayer;
    private IntGrid solidGrid;
    private IntGrid waterGrid;

    public override void OnStart()
    {
        midLayer = new GameObject[GridWidth, GridHeight];

        solidGrid = new IntGrid(GridWidth, GridHeight);
        solidGrid.SetTiles(0, GridHeight - 20, GridWidth, GridHeight, 1); //Ground
        solidGrid.SetTiles(0, 0, 1, GridHeight, 1); //Left Wall
        solidGrid.SetTiles(GridWidth - 1, 0, GridWidth, GridHeight, 1); //Right Wall

        waterGrid = new IntGrid(GridWidth, GridHeight);
        waterGrid.SetTiles(1, 1, 5, 5, 40);

        DrawAllTiles();
    }

    public override void OnPhysicsUpdate()
    {
        //Fluid
        for (int y = GridHeight - 1; y > -1; y--)
        {
            for (int x = 0; x < GridWidth; x++)
            {
                if (waterGrid.gridArray[x, y] >= 1) // if there is water
                {
                    if (solidGrid.gridArray[x, y + 1] == 0 && waterGrid.gridArray[x, y + 1] <= 9) // if tile below is free
                    {
                        //Move 1 tile down
                        waterGrid.gridArray[x, y] -= 1;
                        UpdateTile(x, y, midLayer);
                        waterGrid.gridArray[x, y + 1] += 1;
                        UpdateTile(x, y + 1, midLayer);
                    }
                    else
                    {
                        if (solidGrid.gridArray[x + 1, y] == 0 && waterGrid.gridArray[x + 1, y] <= 9) //&& waterGrid.gridArray[x, y] > waterGrid.gridArray[x + 1, y]) // if tile to the right is free
                        {
                            //Move 1 tile to the right
                            waterGrid.gridArray[x, y] -= 1;
                            UpdateTile(x, y, midLayer);
                            waterGrid.gridArray[x + 1, y] += 1;
                            UpdateTile(x + 1, y, midLayer);
                        }

                        if (solidGrid.gridArray[x - 1, y] == 0 && waterGrid.gridArray[x + 1, y] <= 9) //&& waterGrid.gridArray[x, y] > waterGrid.gridArray[x + 1, y]) // if tile to the left is free
                        {
                            //Move 1 tile to the left
                            waterGrid.gridArray[x, y] -= 1;
                            UpdateTile(x, y, midLayer);
                            waterGrid.gridArray[x - 1, y] += 1;
                            UpdateTile(x - 1, y, midLayer);
                        }
                    }
                }
            }
        }
    }

    private void UpdateTile(int _x, int _y, GameObject[,] _layer)
    {
        Destroy(_layer[_x, _y]);
        
        if (waterGrid.gridArray[_x, _y] >= 1) //Draw water
        {
            GameObject currentTile = Instantiate(WaterTile, GetWorldPos(_x, _y), Quaternion.identity);
            currentTile.transform.parent = gameObject.transform;
            _layer[_x, _y] = currentTile;
        }
        else //Draw solid
        {
            GameObject currentTile = Instantiate(SolidTiles[solidGrid.gridArray[_x, _y]], GetWorldPos(_x, _y), Quaternion.identity);
            currentTile.transform.parent = gameObject.transform;
            _layer[_x, _y] = currentTile;
        }
    }

    private void DrawAllTiles()
    {
        //Solid
        for (int y = 0; y < GridHeight; y++)
        {
            for (int x = 0; x < GridWidth; x++)
            {
                UpdateTile(x, y, midLayer);
            }
        }
    }

    private Vector2 GetWorldPos(int _x, int _y)
    {
        int xPosTile = _x + xStartLocation;
        int yPosTile = -_y + yStartLocation;
        Vector2 tilePos = new(xPosTile, yPosTile);
        return tilePos;
    }
}