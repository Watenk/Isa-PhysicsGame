using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Inputs : BaseClass
{
    public float ScrollSpeed;
    public int minCamSize;
    public int maxCamSize;

    public ID currentElement;

    private Vector2 referenceMousePos;

    //references
    private InputManager inputManager;
    private GameManager gameManager;
    private MainGrid mainGrid;
    private GameObject tempRenderer;
    private GameObject amountRenderer;

    public override void OnAwake()
    {
        inputManager = FindObjectOfType<InputManager>();
        gameManager = FindObjectOfType<GameManager>();
        mainGrid = FindObjectOfType<MainGrid>();
        tempRenderer = FindObjectOfType<TempRenderer>().gameObject;
        tempRenderer.SetActive(false);
        amountRenderer = FindObjectOfType<AmountRenderer>().gameObject;
        amountRenderer.SetActive(false);
    }

    public override void OnUpdate()
    {
        CameraInput();
        PlaceTiles();
        GameRules();
        Overlays();
        ElementSelection();
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

    private void ElementSelection()
    {
        if (inputManager.one == true)
        {
            currentElement = ID.dirt;
        }
        if (inputManager.two == true)
        {
            currentElement = ID.grass;
        }
        if (inputManager.three == true)
        {
            currentElement = ID.water;
        }
        if (inputManager.four == true)
        {
            currentElement = ID.stone;
        }
        if (inputManager.five == true)
        {
            currentElement = ID.ice;
        }
        if (inputManager.six == true)
        {
            currentElement = ID.carbonDioxite;
        }
        if (inputManager.seven == true)
        {
            currentElement = ID.oxygen;
        }
        if (inputManager.eight == true)
        {
            currentElement = ID.steam;
        }
    }

    private void PlaceTiles()
    {
        if (inputManager.LeftMouse == true)
        {
            mainGrid.SetTile(inputManager.mousePosGrid, currentElement, 9, 20000);
        }
    }

    private void Overlays()
    {
        //TempOverlay
        if (inputManager.F1 == true)
        {
            if (tempRenderer.activeSelf == false)
            {
                tempRenderer.SetActive(true);
            }
            else
            {
                tempRenderer.SetActive(false);
            }
        }

        //AmountOverlay
        if (inputManager.F2 == true)
        {
            if (amountRenderer.activeSelf == false)
            {
                amountRenderer.SetActive(true);
            }
            else
            {
                amountRenderer.SetActive(false);
            }
        }
    }

    private void GameRules()
    {
        if (inputManager.space == true)
        {
            if (gameManager.UPS == 60)
            {
                gameManager.UPS = 0;
            }
            else
            {
                gameManager.UPS = 60;
            }
        }
    }
}