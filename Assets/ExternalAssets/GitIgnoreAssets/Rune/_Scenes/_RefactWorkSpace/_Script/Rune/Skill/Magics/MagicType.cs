using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NoonNight.Battle.Skill.Magics
{

    [Flags]
    public enum MagicType
    {
        NONE = 0,
        Fire = 1 << 0,
        FireTornado = 1 << 1,
        Ice = 1 << 2,
        WaterLaser = 1 << 3,
        Sword = 1 << 4,
        Shield = 1 << 5,
        Horse = 1 << 6,
        Gear = 1 << 7,
        Heal = 1 << 8,
        Card = 1 << 9,
        RainArrow = WaterLaser | Sword,
        Crescent = Horse | Sword,
        IceHammer = Ice | Gear,
        DarkScull = FireTornado | Sword,
    }

}
