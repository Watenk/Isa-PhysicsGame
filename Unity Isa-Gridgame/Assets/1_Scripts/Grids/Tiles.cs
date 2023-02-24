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
    isSolid,
    isLiquid,
    isGas,
    minValue,
    maxValue,
    freezeTemp,
    freezeID,
    meltTemp,
    meltID,
    evaporateTemp,
    evaporateID,
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
                    { IDProperties.isSolid, 1 },
                    { IDProperties.minValue, 0 },
                    { IDProperties.maxValue, 1 },
                    { IDProperties.meltTemp, 3000 },
                    { IDProperties.meltID, (int)ID.lava },
                }
            },
            {
                //Stone
                ID.stone, new Dictionary<IDProperties, int>
                {
                    { IDProperties.isSolid, 1 },
                    { IDProperties.minValue, 0 },
                    { IDProperties.maxValue, 1 },
                    { IDProperties.meltTemp, 3000 },
                    { IDProperties.meltID, (int)ID.lava },
                }
            },
            {
                //Lava
                ID.lava, new Dictionary<IDProperties, int>
                {
                    { IDProperties.isLiquid, 1 },
                    { IDProperties.minValue, 0 },
                    { IDProperties.maxValue, 9 },
                    { IDProperties.evaporateTemp, 10000 },
                    { IDProperties.freezeTemp, 2900 },
                    { IDProperties.evaporateID, (int)ID.none },
                    { IDProperties.freezeID, (int)ID.stone },
                }
            }
        };
    }

    public int GetValue(ID id, IDProperties idProperty)
    {
        tilesDictionary.TryGetValue(id, out Dictionary<IDProperties, int> propertiesDictionary);
        propertiesDictionary.TryGetValue(idProperty, out int value);
        return value;
    }
}