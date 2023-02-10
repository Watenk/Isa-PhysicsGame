using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile 
{
    protected TileType tileType;
    protected int amount;
    protected int temperature;
    protected int min;
    protected int max;

    public Tile(TileType tiletype, int amount, int temperature)
    {
        this.tileType = tiletype;
        this.amount = amount;
        this.temperature = temperature;
    }
}

public enum TileType
{
    Dirt,
    Grass,
    Water,
    lava
}