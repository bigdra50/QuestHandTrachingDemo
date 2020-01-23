using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NoonNight.Battle.Skill.Physics
{
    public abstract class PhysicsEntity : SkillEntity
    {
        public override SkillType SkillType => SkillType.Physic;
    }
}
