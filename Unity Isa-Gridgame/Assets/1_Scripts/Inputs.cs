using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Inputs : BaseClass
{
    public Grid Grid;
    public float ScrollSpeed;

    private Vector2 referenceMousePos;

    public override void OnUpdate()
    {
        //Mouse ---------------------------------------------------------
        if (Input.GetMouseButton(0))
        {
            Vector2 mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            int xMousePosInt = (int)math.round(mousePos.x);
            int yMousePosInt = (int)math.round(mousePos.y);
            Grid.SetTile(xMousePosInt, -yMousePosInt, ID.dirt, 1, 20);
        }

        if (Input.GetMouseButton(1))
        {
            Vector2 mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            int xMousePosInt = (int)math.round(mousePos.x);
            int yMousePosInt = (int)math.round(mousePos.y);
            Grid.SetTile(xMousePosInt, -yMousePosInt, ID.stone, 1, 20);
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

        if (Input.mouseScrollDelta.y > 0f && Camera.main.orthographicSize > 1) //Scroll up
        {
            Camera.main.orthographicSize -= ScrollSpeed + (Grid.Width + Grid.Height) * 0.01f;
        }

        if (Input.mouseScrollDelta.y < 0f) //Scroll down
        {
            Camera.main.orthographicSize += ScrollSpeed + (Grid.Width + Grid.Height) * 0.01f;
        }

        //-------------------------------------------------------------------------------------------------------------------

        //if (Input.GetKeyDown("f1"))
        //{
        //    if (GridManager.GetGrid(GridType.Temperature).GetComponent<MeshRenderer>().enabled == false)
        //    {
        //        GridManager.GetGrid(GridType.Temperature).GetComponent<MeshRenderer>().enabled = true;
        //    }
        //    else
        //    {
        //        GridManager.GetGrid(GridType.Temperature).GetComponent<MeshRenderer>().enabled = false;
        //    }
        //}
    }
}