using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsTile
{
    //Properties
    public ID id { get; }
    public bool isSolid { get; set; }
    public bool isLiquid { get; set; }
    public bool isGas { get; set; }
    public int updateSpeed { get; set; }
    public bool hasGravity { get; set; }
    public int maxAmount { get; set; }
    public int speed { get; set; }
    public int thermalConductivity { get; set; }
    //MinTemp
    public bool hasMinTemp { get; set; }
    public int minTemp { get; set; }
    public ID ifMinTemp { get; set; }
    //MaxTemp
    public bool hasMaxTemp { get; set; }
    public int maxTemp { get; set; }
    public ID ifMaxTemp { get; set; }

    public PhysicsTile(ID id, bool isSolid, bool isLiquid, bool isGas, int updateSpeed, bool hasGravity, int maxAmount, int speed, int thermalConductivity, bool hasMinTemp, int minTemp, ID ifMinTemp, bool hasMaxTemp, int maxTemp, ID ifMaxTemp)
    {
        //Properties
        this.id = id;
        this.isSolid = isSolid;
        this.isLiquid = isLiquid;
        this.isGas = isGas;
        this.updateSpeed = updateSpeed;
        this.hasGravity = hasGravity;
        this.maxAmount = maxAmount;
        this.speed = speed;
        this.thermalConductivity = thermalConductivity;
        //MinTemp
        this.hasMinTemp = hasMinTemp;
        this.minTemp = minTemp;
        this.ifMinTemp = ifMinTemp;
        //MaxTemp
        this.hasMaxTemp = hasMaxTemp;
        this.maxTemp = maxTemp;
        this.ifMaxTemp = ifMaxTemp;
    }
}