using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ID
{
    none,
    dirt,
    grass,
    water,
    stone,
    black,
}

public class Tile
{
    public ID id { get; set; }
    public Vector2Int pos { get; set; }
    public int amount { get; set; }
    public int temp { get; set; }

    public Tile(ID id, Vector2Int pos, int amount, int temp)
    {
        this.id = id;
        this.pos = pos;
        this.amount = amount;
        this.temp = temp;
    }
}