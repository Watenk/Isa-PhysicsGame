using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ID
{
    none,
    dirt,
    stone,
    water,
    lava,
    ice,
    steam,
}

public enum IDProperties
{
    isSolid,
    isLiquid,
    isGas,
    minValue,
    maxValue,
    speed,
    freezeTemp,
    freezeID,
    meltTemp,
    meltID,
    evaporateTemp,
    evaporateID,
}

public class TilesDictionary : BaseClass
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
                //Water
                ID.water, new Dictionary<IDProperties, int>
                {
                    { IDProperties.isLiquid, 1 },
                    { IDProperties.minValue, 0 },
                    { IDProperties.maxValue, 9 },
                    { IDProperties.speed, 20 },
                    { IDProperties.evaporateTemp, 100 },
                    { IDProperties.freezeTemp, 0 },
                    { IDProperties.evaporateID, (int)ID.steam },
                    { IDProperties.freezeID, (int)ID.ice },
                }
            },
            {
                //Lava
                ID.lava, new Dictionary<IDProperties, int>
                {
                    { IDProperties.isLiquid, 1 },
                    { IDProperties.minValue, 0 },
                    { IDProperties.maxValue, 9 },
                    { IDProperties.speed, 5 },
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
        if (tilesDictionary.TryGetValue(id, out Dictionary<IDProperties, int> propertiesDictionary))
        {
            if (propertiesDictionary.TryGetValue(idProperty, out int value))
            {
                return value;
            }
            return -1;
        }
        return -1;
    }
}