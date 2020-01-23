using System;
using System.Collections;
using System.Collections.Generic;
using NoonNight.Battle.RuneCanvas;
using UnityEngine;

namespace NoonNight.Battle.Skill.Magics
{
    public abstract class MagicEntity : SkillEntity
    {
        public override SkillType SkillType => SkillType.Magic;
        public override ElementType ElementType { get; }
        public abstract EmitterPos Emitter { get; }
        public abstract MagicType MagicType { get; }

    }
}
