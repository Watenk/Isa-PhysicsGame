using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Inputs : BaseClass
{
    private Player player;

    public override void OnAwake()
    {
        player = FindObjectOfType<Player>();
    }

    public override void OnStart()
    {
    }

    public override void OnUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            //Vector2 mousePos = Input.mousePosition;
            //mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            //int xMousePos = (int)math.round(mousePos.x);
            //int yMousePos = (int)math.round(mousePos.y);
            //groundGrid.SetTile(xMousePos, -yMousePos, 1);
        }

        if (Input.GetMouseButton(1))
        {

        }

        if (Input.GetMouseButtonDown(2))
        {
            player.CalcReferenceMousePos();
        }

        if (Input.GetMouseButton(2))
        {
            player.MoveCamera();
        }
    }
}
