using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NoonNight.Battle.Skill
{
    public interface ISkillEffectAffectable
    {
        void Affect(SkillEntity skill);
    }
}
