using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile
{
    protected ID id;
    protected bool isSolid;
    protected bool isLiquid;
    protected bool isGas;
    protected int amount;
    protected int speed;
    protected int temperature;
    protected int thermalConductivity;

    public Tile(ID id, int amount, int temperature)
    {
        this.id = id;
        this.amount = amount;
        this.temperature = temperature;
    }

    public ID GetID() { return id; }
    public void SetID(ID id) 
    { 
        this.id = id;
        //Change speed, amount, thermalconductivity, isGas, etc...
    }
    public int GetAmount() { return amount; }
    public void SetAmount(int amount) { this.amount = amount; }
    public void ChangeAmount(int value) { amount += value; }
    public int GetTemperature() { return temperature; }
    public void SetTemperature(int temperature) { this.temperature = temperature; }
    public int GetThermalConductivity() { return thermalConductivity; }
}