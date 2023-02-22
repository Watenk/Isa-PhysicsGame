using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ID
{
    none,
    dirt,
    stone,
    lava,
    ice,
    water,
    steam,
}

public enum IDProperties
{
    minValue,
    maxValue,
    minTemp,
    maxTemp,
}

public class Tiles : BaseClass
{
    private Dictionary<ID, Dictionary<IDProperties, int>> tilesDictionary;

    public override void OnStart()
    {
        tilesDictionary = new Dictionary<ID, Dictionary<IDProperties, int>>
        {
            {
                //Dirt
                ID.dirt, new Dictionary<IDProperties, int>
                {
                    { IDProperties.minValue, 0 },
                    { IDProperties.maxValue, 0 },
                    { IDProperties.minTemp, 0 },
                    { IDProperties.maxTemp, 0 }
                }
            },
            {
                //Stone
                ID.stone, new Dictionary<IDProperties, int>
                {
                    { IDProperties.minValue, 0 },
                    { IDProperties.maxValue, 0 },
                    { IDProperties.minTemp, 0 },
                    { IDProperties.maxTemp, 0 }
                }
            },
        };
    }

    public int GetValue(ID id, IDProperties idProperty)
    {
        tilesDictionary.TryGetValue(id, out Dictionary<IDProperties, int> propertiesDictionary);
        propertiesDictionary.TryGetValue(idProperty, out int value);
        return value;
    }
}