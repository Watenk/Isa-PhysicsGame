using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolidPhysicsTile
{
    protected ID id;
    protected int updateSpeed;
    protected int maxTemp;
    protected ID ifMaxTemp;

    public SolidPhysicsTile(ID id, int updateSpeed, int maxTemp, ID ifMaxTemp)
    {
        this.id = id;
        this.updateSpeed = updateSpeed;
        this.maxTemp = maxTemp;
        this.ifMaxTemp = ifMaxTemp;
    }
}