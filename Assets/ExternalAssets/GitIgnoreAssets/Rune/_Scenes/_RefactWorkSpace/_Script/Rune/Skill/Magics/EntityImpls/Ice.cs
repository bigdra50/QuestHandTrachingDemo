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
    public class Ice : MagicEntity, IDamager
    {
        private EmitterPos _emitter = EmitterPos.OnGroundForward;
        
        public override ElementType ElementType => ElementType.ICE;
        public override EmitterPos Emitter => _emitter;
        public override MagicType MagicType => MagicType.Ice;

        public override Parameters StartSkillEffect(Parameters targetParams)
        {
            throw new NotImplementedException();
        }

        public override void Initialize(Parameters userParams)
        {
            _userParams = userParams;
        }


        void Start()
        {
            try
            {

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public Parameters ApplyDamage(Parameters targetParameters)
        {
            throw new NotImplementedException();
        }
    }
}
