using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int PhysicsUpdates;

    private float physicsTimer;
    private List<BaseClass> objectsList;

    private void Awake()
    {
        objectsList = new List<BaseClass>();
        objectsList.AddRange(FindObjectsOfType<BaseClass>());

        for (int i = 0; i < objectsList.Count; i++) { objectsList[i].OnAwake(); }
    }

    private void Start()
    {
        for (int i = 0; i < objectsList.Count; i++) { objectsList[i].OnStart(); }
    }

    private void Update()
    {
        if (physicsTimer > (1 / PhysicsUpdates))
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