using System;
using System.Collections;
using System.Collections.Generic;
using NoonNight.Battle.Managers;
using NoonNight.Battle.Monsters;
using NoonNight.Battle.RuneCanvas;
using NoonNight.Battle.Skill.SkillEffects;
using UnityEngine;
using Zenject;

namespace NoonNight.Battle.Skill.Magics.EntityImpls
{
    public class Fire : MagicEntity, IDamager
    {
        private EmitterPos _emitter = EmitterPos.OnCanvas;
        public override ElementType ElementType => ElementType.FLAME;
        public override EmitterPos Emitter => _emitter;
        public override MagicType MagicType => MagicType.Fire;

        public override Parameters StartSkillEffect(Parameters targetParams)
        {
            targetParams = ApplyDamage(targetParams);
            return targetParams;
        }

        public override void Initialize(Parameters userParams)
        {
            _userParams = userParams;
        }

        public Parameters ApplyDamage(Parameters targetParameters)
        {
            var damageValue = _userParams._atk - targetParameters._def;
            targetParameters._hp -= damageValue;
            return targetParameters;
        }
    }
}
