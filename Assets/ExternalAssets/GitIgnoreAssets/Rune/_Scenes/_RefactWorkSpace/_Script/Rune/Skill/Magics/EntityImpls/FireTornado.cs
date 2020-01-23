using System.Collections;
using System.Collections.Generic;
using NoonNight.Battle.RuneCanvas;
using NoonNight.Battle.Skill.SkillEffects;
using UnityEngine;

namespace NoonNight.Battle.Skill.Magics.EntityImpls
{


    public class FireTornado : MagicEntity, IDamager
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public override Parameters StartSkillEffect(Parameters targetParams)
        {
            throw new System.NotImplementedException();
        }

        public override void Initialize(Parameters userParams)
        {
            throw new System.NotImplementedException();
        }

        public override EmitterPos Emitter => EmitterPos.OnGroundCanvas;
        public override MagicType MagicType => MagicType.FireTornado;
        public Parameters ApplyDamage(Parameters targetParameters)
        {
            throw new System.NotImplementedException();
        }
    }
}
