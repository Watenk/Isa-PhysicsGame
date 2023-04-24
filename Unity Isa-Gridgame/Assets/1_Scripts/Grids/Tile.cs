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
}

public class Tile
{
    protected ID id;
    protected Vector2Int pos;
    protected int amount;
    protected int temp;

    public Tile(ID id, Vector2Int pos)
    {
        this.id = id;
        this.pos = pos;
    }

    //ID
    public ID GetID() 
    { 
        return id; 
    }
    public void SetID(ID id) 
    { 
        this.id = id; 
    }

    //Pos
    public Vector2Int GetPos() 
    {  
        return pos; 
    }

    //Amount
    public int GetAmount() 
    { 
        return amount; 
    }
    public void SetAmount(int amount) 
    { 
        this.amount = amount; 
    }

    //Temp
    public int GetTemp() 
    { 
        return temp; 
    }
    public void SetTemp(int amount) 
    { 
        temp = amount; 
    }
}