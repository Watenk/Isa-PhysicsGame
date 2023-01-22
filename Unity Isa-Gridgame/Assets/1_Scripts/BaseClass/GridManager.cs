using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : BaseClass
{
    //Grid Values
    public int GridWidth;
    public int GridHeight;
    public int xStartLocation;
    public int yStartLocation;

    //MaterialsGrid
    public IntGrid materialGrid;
    private Texture materialsTexture;
    private Vector2[] uv0Materials;
    private Vector2[] uv1Materials;

    //WaterGrid
    public IntGrid waterGrid;
    public int minWater;
    public int maxWater;
    public int waterSpeed;

    //MaterialsMesh
    private Mesh materialsMesh;
    private Vector3[] vertices;
    private Vector2[] uv;
    private int[] triangles;

    public override void OnAwake()
    {
        materialsTexture = GetComponent<MeshRenderer>().material.mainTexture;
    }

    public override void OnStart()
    {
        materialsMesh = new Mesh();
        materialsMesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
        GetComponent<MeshFilter>().mesh = materialsMesh;

        //Generate Arrays for quads
        int quadAmount = GridWidth * GridHeight;
        vertices = new Vector3[4 * quadAmount];
        uv = new Vector2[4 * quadAmount];
        triangles = new int[6 * quadAmount];

        //MaterialGrid
        materialGrid = new IntGrid(GridWidth, GridHeight);
        uv0Materials = new Vector2[10];
        uv1Materials = new Vector2[10];
        AssignUv(materialsTexture, uv0Materials, uv1Materials, 0, 0, 32, 32); //Air
        AssignUv(materialsTexture, uv0Materials, uv1Materials, 1, 32, 64, 32); //Dirt
        AssignUv(materialsTexture, uv0Materials, uv1Materials, 2, 64, 96, 32); //Stone
        AssignUv(materialsTexture, uv0Materials, uv1Materials, 3, 96, 128, 32); //Plant
        AssignUv(materialsTexture, uv0Materials, uv1Materials, 4, 128, 160, 32); //Water

        //Generate world
        materialGrid.SetTiles(0, GridHeight - 10, GridWidth, GridHeight, 1); //Ground
        materialGrid.SetTiles(0, 0, 1, GridHeight, 1); //Left Wall
        materialGrid.SetTiles(GridWidth - 1, 0, GridWidth, GridHeight, 1); //Right Wall

        //WaterGrid
        waterGrid = new IntGrid(GridWidth, GridHeight);
        //Generate Water
        //waterGrid.SetTile(50, 5, 300);
    }

    public override void OnPhysicsUpdate()
    {
        FluidPhysics(waterGrid, minWater, maxWater, waterSpeed, 4);

        DrawMeshGrid(materialGrid, uv0Materials, uv1Materials);
    }


    private void DrawMeshGrid(IntGrid currentGrid, Vector2[] uv0, Vector2[] uv1)
    {
        //Fill the generated grid arrays
        int i = 0;
        for (int y = 0; y < GridHeight; y++)
        {
            for (int x = 0; x < GridWidth; x++)
            {
                if (materialGrid.GetTile(x, y) != 0)
                {
                    //Vertices
                    int verticesAndUvIndex = i * 4;
                    vertices[verticesAndUvIndex + 0] = new Vector3(x, -y);
                    vertices[verticesAndUvIndex + 1] = new Vector3(x, -y + 1);
                    vertices[verticesAndUvIndex + 2] = new Vector3(x + 1, -y + 1);
                    vertices[verticesAndUvIndex + 3] = new Vector3(x + 1, -y);
           
                    //Textures
                    uv[verticesAndUvIndex + 1] = new Vector2(uv0[currentGrid.GetTile(x, y)].x, uv0[currentGrid.GetTile(x, y)].y);
                    uv[verticesAndUvIndex + 2] = new Vector2(uv1[currentGrid.GetTile(x, y)].x, uv0[currentGrid.GetTile(x, y)].y);
                    uv[verticesAndUvIndex + 0] = new Vector2(uv0[currentGrid.GetTile(x ,y)].x, uv1[currentGrid.GetTile(x, y)].y);
                    uv[verticesAndUvIndex + 3] = new Vector2(uv1[currentGrid.GetTile(x, y)].x, uv1[currentGrid.GetTile(x, y)].y);
           
                    //Triangles
                    int trianglesIndex = i * 6;
                    triangles[trianglesIndex + 0] = verticesAndUvIndex + 0;
                    triangles[trianglesIndex + 1] = verticesAndUvIndex + 1;
                    triangles[trianglesIndex + 2] = verticesAndUvIndex + 2;
                    triangles[trianglesIndex + 3] = verticesAndUvIndex + 0;
                    triangles[trianglesIndex + 4] = verticesAndUvIndex + 2;
                    triangles[trianglesIndex + 5] = verticesAndUvIndex + 3;
                }
                else
                {
                    //Vertices
                    int verticesAndUvIndex = i * 4;
                    vertices[verticesAndUvIndex + 0] = new Vector3(0, 0);
                    vertices[verticesAndUvIndex + 1] = new Vector3(0, 0);
                    vertices[verticesAndUvIndex + 2] = new Vector3(0, 0);
                    vertices[verticesAndUvIndex + 3] = new Vector3(0, 0);

                    //Textures
                    uv[verticesAndUvIndex + 1] = new Vector2(0, 0);
                    uv[verticesAndUvIndex + 2] = new Vector2(0, 0);
                    uv[verticesAndUvIndex + 0] = new Vector2(0, 0);
                    uv[verticesAndUvIndex + 3] = new Vector2(0, 0);
                }

                i++;
            }
        }
        //Update Mesh
        materialsMesh.vertices = vertices;
        materialsMesh.uv = uv;
        materialsMesh.triangles = triangles;
    }

    private void AssignUv(Texture texture, Vector2[] uv0Array, Vector2[] uv1Array, int index, float pixelStart, float pixelEnd, float pixelHeight)
    {
        uv0Array[index] = new Vector2(pixelStart / texture.width, 0);
        uv1Array[index] = new Vector2(pixelEnd / texture.width, pixelHeight / texture.height);
    }

    private void FluidPhysics(IntGrid fluidGrid, int minValue, int maxValue, int flowSpeed, int material)
    {
        for (int y = GridHeight - 1; y > 0; y--)
        {
            for (int x = 1; x < GridWidth - 1; x++)
            {
                if (fluidGrid.GetTile(x, y) >= 1) // if there is water
                {
                    //Move down
                    if (materialGrid.GetTile(x, y + 1) == 0 || materialGrid.GetTile(x, y + 1) == material)
                    {
                        if (fluidGrid.GetTile(x, y + 1) <= maxValue)
                        {
                            MoveValue(materialGrid, waterGrid, x, y, 0, 1, flowSpeed + 1, material);
                        }
                    }

                    //Move to the lowest neighbour
                    int highestNumber = Mathf.Max(materialGrid.GetTile(x - 1, y), materialGrid.GetTile(x + 1, y));
                    if (highestNumber == materialGrid.GetTile(x - 1, y))
                    {
                        //Move right
                        if (materialGrid.GetTile(x + 1, y) == 0 || materialGrid.GetTile(x + 1, y) == material)
                        {
                            if (fluidGrid.GetTile(x + 1, y) <= maxValue)
                            {
                                MoveValue(materialGrid, waterGrid, x, y, 1, 0, flowSpeed, material);
                            }
                        }
                    }
                    else
                    {
                        //Move left
                        if (materialGrid.GetTile(x - 1, y) == 0 || materialGrid.GetTile(x - 1, y) == material)
                        {
                            if (fluidGrid.GetTile(x - 1, y) <= maxValue)
                            {
                                MoveValue(materialGrid, waterGrid, x, y, -1, 0, flowSpeed, material);
                            }
                        }
                    }
                }
                //Need to fix updates to materialGrid
                else if (fluidGrid.GetTile(x, y) == 0 && materialGrid.GetTile(x, y) != 1)
                {
                    materialGrid.SetTile(x, y, 0);
                }
            }
        }
    }

    private void MoveValue(IntGrid materialGrid, IntGrid intGrid, int x, int y, int xMove, int yMove, int flowAmount, int index)
    {
        for (int i = 0; i < flowAmount; i++)
        {
            if (intGrid.GetTile(x, y) != 0)
            {
                intGrid.ChangeTile(x, y, -1);
                intGrid.ChangeTile(x + xMove, y + yMove, 1);
            }
        }

        if (intGrid.GetTile(x, y) == 0) { materialGrid.SetTile(x, y, 0); }
        materialGrid.SetTile(x + xMove, y + yMove, index);
    }
}