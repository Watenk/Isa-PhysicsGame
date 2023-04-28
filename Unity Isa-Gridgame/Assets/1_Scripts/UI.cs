using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UI : BaseClass
{
    public Text FrameRate;
    public Text AverageFrameRate;
    public int FrameAmountForAverageFramerate;
    public Text LowestFrame;
    public Text MouseID;
    public Text MouseAmount;
    public Text MouseTemp;

    private int averageFPS;
    private int lowestFrame;
    private float[] frames;
    private int frameCounter;

    public Text currentElement;

    private MainGrid mainGrid;
    private InputManager inputManager;
    private Inputs inputs;

    public override void OnAwake()
    {
        mainGrid = FindObjectOfType<MainGrid>();
        inputManager = FindObjectOfType<InputManager>();
        inputs = FindObjectOfType<Inputs>();
    }

    public override void OnStart()
    {
        frames = new float[FrameAmountForAverageFramerate];
    }

    public override void OnUpdate()
    {
        //Calc
        if (frameCounter != FrameAmountForAverageFramerate)
        {
            frames[frameCounter] = 1.0f / Time.deltaTime;
            frameCounter += 1;
        }
        else
        {
            averageFPS = (int)frames.Sum() / frames.Length;
            lowestFrame = (int)frames.Min();
            AverageFrameRate.text = "AverageFPS: " + averageFPS.ToString();
            LowestFrame.text = "LowestFrame: " + lowestFrame.ToString();
            frameCounter = 0;
        }
        

        //UI
        FrameRate.text = "FPS: " + (1.0f / Time.deltaTime).ToString();
        currentElement.text = "CurrentElement: " + inputs.currentElement.ToString();

        if (mainGrid.IsInGridBounds(inputManager.mousePosGrid))
        {
            Tile currentTile = mainGrid.GetTile(inputManager.mousePosGrid);
            MouseID.text = currentTile.id.ToString() + " : ID";
            MouseAmount.text = currentTile.amount.ToString() + " : Amount";
            string temp = currentTile.temp.ToString().PadLeft(6);
            MouseTemp.text = temp.Insert(temp.Length - 3, ".") + " : Temp";
        }
    }
}