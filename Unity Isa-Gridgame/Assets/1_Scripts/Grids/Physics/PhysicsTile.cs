using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsTile
{
    public ID id { get; }
    public int updateSpeed { get; set; }
    public int currentUpdate { get; set; }
    public bool hasGravity { get; set; }
    public int speed { get; set; }
    public int maxAmount { get; set; }
    public int maxTemp { get; set; }
    public ID ifMaxTemp { get; set; }
    public int thermalConductivity { get; set; }

    public PhysicsTile(ID id, int updateSpeed, bool hasGravity, int speed, int maxAmount, int maxTemp, ID ifMaxTemp, int thermalConductivity)
    {
        this.id = id;
        this.updateSpeed = updateSpeed;
        this.hasGravity = hasGravity;
        this.speed = speed;
        this.maxAmount = maxAmount;
        this.maxTemp = maxTemp;
        this.ifMaxTemp = ifMaxTemp;
        this.thermalConductivity = thermalConductivity;
    }
}