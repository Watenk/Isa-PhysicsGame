using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Grid grid;
    private Inputs inputs;
    private void Awake()
    {
        grid = FindObjectOfType<Grid>();
        inputs = FindObjectOfType<Inputs>();

        inputs.OnAwake();
    }

    void Start()
    {
        grid.OnStart();
    }

    void Update()
    {
        inputs.OnUpdate();
    }
}