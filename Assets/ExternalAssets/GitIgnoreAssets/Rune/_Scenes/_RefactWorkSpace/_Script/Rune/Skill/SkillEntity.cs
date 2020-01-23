using System.Collections;
using System.Collections.Generic;
using NoonNight.Battle.Monsters;
using UnityEngine;

namespace NoonNight.Battle.Skill
{
    public abstract class SkillEntity : MonoBehaviour
    {
        protected Parameters _userParams;

        public Parameters UserParameters => _userParams;
        
        public abstract SkillType SkillType { get; }
        public abstract ElementType ElementType { get; }
        
        public abstract Parameters StartSkillEffect(Parameters targetParams);    // まだ型は適当

        public abstract void Initialize(Parameters userParams);
    }
}
