using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : BaseClass
{
    public int GridWidth;
    public int GridHeight;
    public int xStartLocation;
    public int yStartLocation;

    public IntGrid solidGrid;
    public IntGrid waterGrid;

    private Mesh gridMesh;
    private Vector3[] vertices;
    private Vector2[] uv;
    private int[] triangles;

    public override void OnStart()
    {
        gridMesh = new Mesh();
        gridMesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
        gridMesh.subMeshCount = 2;
        GetComponent<MeshFilter>().mesh = gridMesh;

        //Generate Arrays for quads
        int quadAmount = GridWidth * GridHeight;
        vertices = new Vector3[4 * quadAmount];
        uv = new Vector2[4 * quadAmount];
        triangles = new int[6 * quadAmount];

        //Solidgrid
        solidGrid = new IntGrid(GridWidth, GridHeight);
        solidGrid.SetTiles(0, GridHeight - 10, GridWidth, GridHeight, 1); //Ground
        solidGrid.SetTiles(0, 0, 1, GridHeight, 1); //Left Wall
        solidGrid.SetTiles(GridWidth - 1, 0, GridWidth, GridHeight, 1); //Right Wall

        waterGrid = new IntGrid(GridWidth, GridHeight);
        waterGrid.SetTiles(1, 1, 5, 5, 300);
    }

    public override void OnPhysicsUpdate()
    {
        DrawMeshGrid();
    }


    private void DrawMeshGrid()
    {
        //Fill the generated quads
        int i = 0;
        for (int y = 0; y < GridHeight; y++)
        {
            for (int x = 0; x < GridWidth; x++)
            {
                int verticesAndUvIndex = i * 4;

                vertices[verticesAndUvIndex + 0] = new Vector3(x, y);
                vertices[verticesAndUvIndex + 1] = new Vector3(x, y + 1);
                vertices[verticesAndUvIndex + 2] = new Vector3(x + 1, y + 1);
                vertices[verticesAndUvIndex + 3] = new Vector3(x + 1, y);

                if (solidGrid.gridArray[x, y] == 0)
                {
                    uv[verticesAndUvIndex + 0] = new Vector2(x, y);
                    uv[verticesAndUvIndex + 1] = new Vector2(x, y);
                    uv[verticesAndUvIndex + 2] = new Vector2(x, y);
                    uv[verticesAndUvIndex + 3] = new Vector2(x, y);
                }

                int trianglesIndex = i * 6;

                triangles[trianglesIndex + 0] = verticesAndUvIndex + 0;
                triangles[trianglesIndex + 1] = verticesAndUvIndex + 1;
                triangles[trianglesIndex + 2] = verticesAndUvIndex + 2;
                triangles[trianglesIndex + 3] = verticesAndUvIndex + 0;
                triangles[trianglesIndex + 4] = verticesAndUvIndex + 2;
                triangles[trianglesIndex + 5] = verticesAndUvIndex + 3;

                i++;
            }
        }
        gridMesh.vertices = vertices;
        gridMesh.uv = uv;
        gridMesh.triangles = triangles;
    }

    //private void FluidPhysics(IntGrid fluidGrid)
    //{
        //for (int y = GridHeight - 1; y > -1; y--)
        //{
        //    for (int x = 0; x < GridWidth; x++)
        //    {
        //        if (fluidGrid.gridArray[x, y] >= 1) // if there is water
        //        {
        //            if (solidGrid.gridArray[x, y + 1] == 0 && fluidGrid.gridArray[x, y + 1] <= 9) // if tile below is free
        //            {
        //                //Move 1 tile down
        //                fluidGrid.gridArray[x, y] -= 1;
        //                UpdateTile(x, y, midLayer);
        //                fluidGrid.gridArray[x, y + 1] += 1;
        //                UpdateTile(x, y + 1, midLayer);
        //            }
        //            else
        //            {
        //                if (solidGrid.gridArray[x + 1, y] == 0 && fluidGrid.gridArray[x + 1, y] <= 9 && fluidGrid.gridArray[x, y] > fluidGrid.gridArray[x + 1, y]) // if tile to the right is free
        //                {
        //                    //Move 1 tile to the right
        //                    fluidGrid.gridArray[x, y] -= 1;
        //                    UpdateTile(x, y, midLayer);
        //                    fluidGrid.gridArray[x + 1, y] += 1;
        //                    UpdateTile(x + 1, y, midLayer);
        //                }

        //                if (solidGrid.gridArray[x - 1, y] == 0 && fluidGrid.gridArray[x - 1, y] <= 9 && fluidGrid.gridArray[x, y] > fluidGrid.gridArray[x - 1, y]) // if tile to the left is free
        //                {
        //                    //Move 1 tile to the left
        //                    fluidGrid.gridArray[x, y] -= 1;
        //                    UpdateTile(x, y, midLayer);
        //                    fluidGrid.gridArray[x - 1, y] += 1;
        //                    UpdateTile(x - 1, y, midLayer);
        //                }
        //            }
        //        }
        //    }
        //}
    //}

    //private Vector2 GetWorldPos(int _x, int _y)
    //{
    //    int xPosTile = _x + xStartLocation;
    //    int yPosTile = -_y + yStartLocation;
    //    Vector2 tilePos = new(xPosTile, yPosTile);
    //    return tilePos;
    //}
}