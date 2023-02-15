using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ID
{
    public class none
    {
        public const int id = 0;
    }

    public class dirt
    {
        public const int id = 1;
        public const int max = 10;
    }

    public class stone
    {
        public const int id = 2;
        public const int max = 10;
    }

    public class lava
    {
        public const int id = 3;
        public const int max = 8;
    }

    public class ice
    {
        public const int id = 4;
        public const int max = 15;
    }

    public class water
    {
        public const int id = 5;
        public const int max = 10;
    }

    public class steam
    {
        public const int id = 6;
        public const int max = 5;
    }
}

public class Tile 
{
    protected int id;
    protected int amount;
    protected int temperature;

    public Tile(int id, int amount, int temperature)
    {
        this.id = id;
        this.amount = amount;
        this.temperature = temperature;
    }

    public int GetID() { return id; }
    public void SetID(int id) { this.id = id; }
    public int GetAmount() { return amount; }
    public void ChangeAmount(int value) { amount += value; }
    public void SetAmount(int amount) { this.amount = amount; }
    public int GetTemperature() { return temperature; }
    public void SetTemperature(int temperature) { this.temperature = temperature; }
}