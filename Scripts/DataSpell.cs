using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataSpell{
    public int Value;
    public TargetType Target;
    public SpellType Type;

    public DataSpell(int value,TargetType target,SpellType type)
    {
        Value = value;
        Target = target;
        Type = type;
    }
}
