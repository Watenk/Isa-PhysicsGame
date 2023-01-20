using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Inputs : BaseClass
{
    public float ScrollSpeed;
    public GameObject GroundGridGO;

    private Grid groundGrid;
    private Vector2 referenceMousePos;

    public override void OnAwake()
    {
        groundGrid = GroundGridGO.GetComponent<Grid>();
    }

    public override void OnUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            Vector2 mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            int xMousePos = (int)math.round(mousePos.x);
            int yMousePos = (int)math.round(mousePos.y);
            //groundGrid.SetTile(xMousePos, -yMousePos, 1);
        }

        if (Input.GetMouseButton(1))
        {

        }

        if (Input.GetMouseButtonDown(2))
        {
            referenceMousePos = Input.mousePosition;
            referenceMousePos = Camera.main.ScreenToWorldPoint(referenceMousePos);
        }

        if (Input.GetMouseButton(2))
        {
            //Get mousepos and calc newPos
            Vector2 currentMousePos = Input.mousePosition;
            currentMousePos = Camera.main.ScreenToWorldPoint(currentMousePos);
            float xDifference = currentMousePos.x - referenceMousePos.x;
            float yDifference = currentMousePos.y - referenceMousePos.y;
            float newXPos = Camera.main.transform.position.x - xDifference;
            float newYPos = Camera.main.transform.position.y - yDifference;

            //Set newPos
            Vector3 newPos = new Vector3(newXPos, newYPos, -10);
            Camera.main.transform.position = newPos;
        }

        if (Input.mouseScrollDelta.y > 0f) //Scroll up
        {
            Camera.main.orthographicSize -= ScrollSpeed;
        }

        if (Input.mouseScrollDelta.y < 0f) //Scroll down
        {
            Camera.main.orthographicSize += ScrollSpeed;
        }
    }
}