using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Inputs : MonoBehaviour
{
    public GameObject groundGridGO;
    private Grid groundGrid;

    public void OnAwake()
    {
        groundGrid = groundGridGO.GetComponent<Grid>();
    }

    public void OnUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            Vector2 mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            int xMousePos = (int)math.round(mousePos.x);
            int yMousePos = (int)math.round(mousePos.y);
            groundGrid.SetTile(xMousePos, -yMousePos, 1);
        }
    }
}
