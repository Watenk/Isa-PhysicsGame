using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridRenderer : BaseClassLate
{
    public ID ID;
    public int maxValue;

    //References
    private Grid grid;
    private Mesh mesh;

    //Quads
    private Vector3[] vertices;
    private Vector2[] uv;
    private int[] triangles;

    //UV
    private Vector2[] uv00;
    private Vector2[] uv11;

    public override void OnStart()
    {
        GenerateMesh();
        GenerateQuads();
        GenerateUVs(maxValue + 1, 32);
    }

    public override void OnPhysicsUpdate()
    {
        Draw();
    }

    private void Draw()
    {
        int i = 0;
        for (int y = 0; y < grid.Height; y++)
        {
            for (int x = 0; x < grid.Width; x++)
            {
                if (grid.GetTile(x, y).GetID() == ID)
                {
                    //Generate quads
                    //Vertices
                    int verticesAndUvIndex = i * 4;
                    vertices[verticesAndUvIndex + 0] = new Vector3(x, -y);
                    vertices[verticesAndUvIndex + 1] = new Vector3(x, -y + 1);
                    vertices[verticesAndUvIndex + 2] = new Vector3(x + 1, -y + 1);
                    vertices[verticesAndUvIndex + 3] = new Vector3(x + 1, -y);

                    //Map values to uv
                    int currentTileValue = grid.GetTile(x, y).GetAmount();
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
                    ////Generate no quads
                    ////Vertices
                    //int verticesAndUvIndex = i * 4;
                    //vertices[verticesAndUvIndex + 0] = new Vector3(0, 0);
                    //vertices[verticesAndUvIndex + 1] = new Vector3(0, 0);
                    //vertices[verticesAndUvIndex + 2] = new Vector3(0, 0);
                    //vertices[verticesAndUvIndex + 3] = new Vector3(0, 0);

                    ////Map values to uv
                    //uv[verticesAndUvIndex + 1] = new Vector2(0, 0);
                    //uv[verticesAndUvIndex + 2] = new Vector2(0, 0);
                    //uv[verticesAndUvIndex + 0] = new Vector2(0, 0);
                    //uv[verticesAndUvIndex + 3] = new Vector2(0, 0);

                    //int trianglesIndex = i * 6;
                    //triangles[trianglesIndex + 0] = 0;
                    //triangles[trianglesIndex + 1] = 0;
                    //triangles[trianglesIndex + 2] = 0;
                    //triangles[trianglesIndex + 3] = 0;
                    //triangles[trianglesIndex + 4] = 0;
                    //triangles[trianglesIndex + 5] = 0;
                }
                i++;
            }
        }
        UpdateMesh();
        SetMeshBoundToCam();
    }

    private void GenerateMesh()
    {
        grid = FindObjectOfType<Grid>();
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
    }

    private void UpdateMesh()
    {
        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
    }

    private void SetMeshBoundToCam()
    {
        Transform camTransform = Camera.main.transform;
        float distToCenter = (Camera.main.farClipPlane - Camera.main.nearClipPlane) / 2.0f;
        Vector3 center = camTransform.position + camTransform.forward * distToCenter;
        float extremeBound = 500.0f;
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        meshFilter.sharedMesh.bounds = new Bounds(center, new Vector3(1, 1) * extremeBound);
    }

    private void GenerateQuads()
    {
        int quadAmount = grid.Width * grid.Height;
        vertices = new Vector3[4 * quadAmount];
        uv = new Vector2[4 * quadAmount];
        triangles = new int[6 * quadAmount];
    }

    private void GenerateUVs(int amount, float tileSize)
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
}