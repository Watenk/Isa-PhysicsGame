using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Inputs : BaseClass
{
    public float ScrollSpeed;
    public int minCamSize;
    public int maxCamSize;

    private Vector2 referenceMousePos;

    //references
    private InputManager inputManager;
    private MainGrid mainGrid;

    public override void OnAwake()
    {
        inputManager = FindObjectOfType<InputManager>();
        mainGrid = FindObjectOfType<MainGrid>();
    }

    public override void OnUpdate()
    {
        CameraInput();
        BuildInput();
    }

    private void CameraInput()
    {
        if (inputManager.MiddleMouseDown == true)
        {
            referenceMousePos = Input.mousePosition;
            referenceMousePos = Camera.main.ScreenToWorldPoint(referenceMousePos);
        }

        if (inputManager.MiddleMouse == true)
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

        //Scroll up
        if (inputManager.ScrollMouseDelta > 0f && Camera.main.orthographicSize > minCamSize && Input.GetMouseButton(2) == false)
        {
            Camera.main.orthographicSize -= Camera.main.orthographicSize * ScrollSpeed * 0.01f;
        }

        //Scroll down
        if (inputManager.ScrollMouseDelta < 0f && Camera.main.orthographicSize < maxCamSize && Input.GetMouseButton(2) == false)
        {
            Camera.main.orthographicSize += Camera.main.orthographicSize * ScrollSpeed * 0.01f;
        }
    }

    private void BuildInput()
    {
        if (inputManager.LeftMouse == true)
        {
            Vector2 mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            Vector2Int pos = new Vector2Int(Mathf.RoundToInt(mousePos.x), Mathf.RoundToInt(-mousePos.y));
            mainGrid.SetTile(pos, ID.water, 1, 20);
        }
    }
}