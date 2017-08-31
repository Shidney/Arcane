using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardActionSet
{
    public enum SetType { SPELL, MINION }
    public static Dictionary<int, DataSpell> SpellSet = new Dictionary<int, DataSpell>();
    public static Dictionary<int, Dictionary<GameTag, bool>> MinionSet = new Dictionary<int, Dictionary<GameTag, bool>>();
    public static Dictionary<int, Dictionary<GameTag, bool>> WeaponSet = new Dictionary<int, Dictionary<GameTag, bool>>();

    public static void SetSpellProperty(int id, DataSpell data)
    {
        if (SpellSet.ContainsKey(id))
        {
            SpellSet[id].Value = data.Value;
            SpellSet[id].Target = data.Target;
            SpellSet[id].Type = data.Type;
        }
        else
        {
            SpellSet.Add(id, data);
        }

    }
    public static DataSpell GetSpellProperty(int id)
    {
        DataSpell dataspell = null;

        SpellSet.TryGetValue(id, out dataspell);

        return dataspell;
    }

    public static void SetMinionProperty(int id, Dictionary<GameTag,bool> properties)
    {
        if (MinionSet.ContainsKey(id))
        {
                MinionSet[id] = properties;         
        }
        else
        {
            MinionSet.Add(id, properties);
        }

    }
    public static Dictionary<GameTag,bool> GetMinionProperty(int id)
    {
        Dictionary<GameTag, bool> minionProperty = null;

        MinionSet.TryGetValue(id, out minionProperty);

        return minionProperty;
    }

    public static void SetWeaponProperty(int id, Dictionary<GameTag, bool> properties)
    {
        if (WeaponSet.ContainsKey(id))
        {
            WeaponSet[id] = properties;
        }
        else
        {
            WeaponSet.Add(id, properties);
        }

    }
    public static Dictionary<GameTag, bool> GetWeaponProperty(int id)
    {
        Dictionary<GameTag, bool> weaponProperty = null;

        WeaponSet.TryGetValue(id, out weaponProperty);

        return weaponProperty;
    }
}

