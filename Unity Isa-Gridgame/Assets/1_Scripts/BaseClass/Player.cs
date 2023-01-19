using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : BaseClass
{
    private Vector2 referenceMousePos;
    private bool refrencePosAvailible;

    public override void OnAwake()
    {
    }

    public override void OnStart()
    {
    }

    public override void OnUpdate()
    {
    }
    public void CalcReferenceMousePos()
    {
        referenceMousePos = Input.mousePosition;
        referenceMousePos = Camera.main.ScreenToWorldPoint(referenceMousePos);
    }

    public void MoveCamera()
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
}