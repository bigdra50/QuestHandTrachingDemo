using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NoonNight.Battle.Skill
{
    public enum SkillType
    {
        Magic,
        Physic,
    }

    public enum ElementType
    {
        // Magic
        NONE = 1 << 0,
        FLAME = 1 << 1,
        ICE = 1 << 2,
        WATER = 1 << 3,
        DARK = 1 << 4,
        LIGHT = 1 << 5,

        // Physical
        SLASH = 1 << 6,
        BLOW = 1 << 7,
        PIERCE = 1 << 8
    }

}
