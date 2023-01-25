using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : BaseClass
{
    public Grids currentGrid;

    //Update
    public int UpdatesPerSecond; //Per Second
    private int updateTimer;

    //Mesh
    private Mesh mesh;
    private Texture texture;

    //Grid
    protected int width;
    protected int height;
    public int startValue;
    public int minValue;
    public int maxValue;
    private int[,] gridArray;

    //Quads
    private Vector3[] vertices;
    private Vector2[] uv;
    private int[] triangles;

    //UV
    private Vector2[] uv00;
    private Vector2[] uv11;

    //Reference
    protected GridManager gridManager;

    public override void OnAwake()
    {
        texture = GetComponent<MeshRenderer>().material.mainTexture;
        gridManager = FindObjectOfType<GridManager>();
    }

    public override void OnStart()
    {
        //Generate Grid
        width = gridManager.GridWidth;
        height = gridManager.GridHeight;
        gridArray = new int[width, height];

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                gridArray[x, y] = startValue;
            }
        }

        //Generate Mesh
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;

        //Generate Quads Arrays
        int quadAmount = width * height;
        vertices = new Vector3[4 * quadAmount];
        uv = new Vector2[4 * quadAmount];
        triangles = new int[6 * quadAmount];

        //UV
        GenerateUVs(maxValue + 1, 32);
    }

    public override void OnPhysicsUpdate()
    {
        if (updateTimer >= 60 / UpdatesPerSecond)
        {
            Draw();
            updateTimer = 0;
        }

        updateTimer += 1;
    }

    public void Draw()
    {
        int i = 0;
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (GetTile(x, y) != 0)
                {
                    //Vertices
                    int verticesAndUvIndex = i * 4;
                    vertices[verticesAndUvIndex + 0] = new Vector3(x, -y);
                    vertices[verticesAndUvIndex + 1] = new Vector3(x, -y + 1);
                    vertices[verticesAndUvIndex + 2] = new Vector3(x + 1, -y + 1);
                    vertices[verticesAndUvIndex + 3] = new Vector3(x + 1, -y);

                    //Map values to uv
                    int currentTileValue = GetTile(x, y);

                    uv[verticesAndUvIndex + 1] = new Vector2(uv00[currentTileValue].x, uv00[currentTileValue].y);
                    uv[verticesAndUvIndex + 2] = new Vector2(uv11[currentTileValue].x, uv00[currentTileValue].y);
                    uv[verticesAndUvIndex + 0] = new Vector2(uv00[currentTileValue].x, uv11[currentTileValue].y);
                    uv[verticesAndUvIndex + 3] = new Vector2(uv11[currentTileValue].x, uv11[currentTileValue].y);

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
        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;

        //Set mesh bounds
        Transform camTransform = UnityEngine.Camera.main.transform;
        float distToCenter = (UnityEngine.Camera.main.farClipPlane - UnityEngine.Camera.main.nearClipPlane) / 2.0f;
        Vector3 center = camTransform.position + camTransform.forward * distToCenter;
        float extremeBound = 500.0f;
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        meshFilter.sharedMesh.bounds = new Bounds(center, new Vector3(1, 1) * extremeBound);
    }

    public void GenerateUVs(int amount, float tileSize)
    {
        uv00 = new Vector2[amount];
        uv11 = new Vector2[amount];

        float tileWidthNormalized = tileSize / (tileSize * amount);

        for (int i = 0; i < amount; i++)
        {
            uv00[i] = new Vector2(tileWidthNormalized * i + 0.001f, 0);
            uv11[i] = new Vector2((tileWidthNormalized * i - 0.001f) + tileWidthNormalized, 1);
        }
    }


    public int GetTile(int _x, int _y)
    {
        return gridArray[_x, _y];
    }

    public void ChangeTile(int _x, int _y, int value) 
    {
        gridArray[_x, _y] = gridArray[_x, _y] + value;
    }

    public void SetTile(int _x, int _y, int _value)
    {
        if (_x >= 0 && _x <= width && _y >= 0 && _y <= height)
        {
            gridArray[_x, _y] = _value;
        }
        else
        {
            Debug.Log("Selection Out of bounds: " + width + ", " + height + " : " + _x + ", " + _y);
        }
    }

    public void SetTiles(int _x, int _y, int _x2, int _y2, int _value)
    {
        if (_x >= 0 && _x <= width && _y >= 0 && _y <= height && _x2 >= 0 && _x2 <= width && _y2 >= 0 && _y2 <= height)
        {
            for (int y = _y; y < _y2; y++)
            {
                for (int x = _x; x < _x2; x++)
                {
                    gridArray[x, y] = _value;
                }
            }
        }
        else
        {
            Debug.Log("Selection Out of bounds: " + width + ", " + height + " : " + _x + ", " + _y + " : " + _x2 + ", " + _y2);
        }
    }
}
