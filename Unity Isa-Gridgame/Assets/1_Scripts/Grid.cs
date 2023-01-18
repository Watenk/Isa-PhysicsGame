using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public GameObject[] tiles;
    public int gridWidth;
    public int gridHeight;
    public int tileWidth;
    public int tileHeight;
    public int xStartLocation;
    public int yStartLocation;

    private GameObject[,] tileArray;

    public void OnStart()
    {
        tileArray = new GameObject[gridWidth, gridHeight];

        for (int y = 0; y < gridHeight; y++)
        {
            for (int x = 0; x < gridWidth; x++)
            {
                GameObject currentTile = Instantiate(tiles[0], GetWorldPos(x, y), Quaternion.identity);
                currentTile.transform.parent = gameObject.transform;
                tileArray[x, y] = currentTile;
            }
        }
    }

    public void SetTile(int _x, int _y, int _value)
    {
        if (_x >= 0 && _x <= gridWidth && _y >= 0 && _y <= gridHeight)
        {
            GameObject currentTile = Instantiate(tiles[_value], GetWorldPos(_x, _y), Quaternion.identity);
            Destroy(tileArray[_x, _y]);
            tileArray[_x, _y] = currentTile;
        }
        else
        {
            Debug.Log("Selection Out of bounds");
        }
    }

    private Vector2 GetWorldPos(int _x, int _y)
    {
        int xPosTile = _x * tileWidth + xStartLocation;
        int yPosTile = -_y * tileHeight + yStartLocation;
        Vector2 tilePos = new(xPosTile, yPosTile);
        return tilePos;
    }
}