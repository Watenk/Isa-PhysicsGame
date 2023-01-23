using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float PhysicsFrameRate; //FPS
    private float physicsTimer;

    private List<BaseClass> objectsList;
    private GridManager gridManager;

    private void Awake()
    {
        objectsList = new List<BaseClass>();
        objectsList.AddRange(FindObjectsOfType<BaseClass>());
        gridManager = FindObjectOfType<GridManager>();

        for (int i = 0; i < objectsList.Count; i++) { objectsList[i].OnAwake(); }
        gridManager.OnAwake();
    }

    private void Start()
    {
        for (int i = 0; i < objectsList.Count; i++) { objectsList[i].OnStart(); }
        gridManager.OnStart();
    }

    private void Update()
    {
        if (physicsTimer > 1 / PhysicsFrameRate)
        {
            PhysicsUpdate();
            physicsTimer = 0;
        }
        physicsTimer += Time.deltaTime;

        for (int i = 0; i < objectsList.Count; i++) { objectsList[i].OnUpdate(); }
    }

    private void PhysicsUpdate()
    {
        for (int i = 0; i < objectsList.Count; i++) { objectsList[i].OnPhysicsUpdate(); }
    }

    public void AddObject(BaseClass _object)
    {
        _object.OnAwake();
        _object.OnStart();
        objectsList.Add(_object);
    }

    public void RemoveObject(BaseClass _object)
    {
        objectsList.Remove(_object);
        Destroy(_object.gameObject);
    }
}