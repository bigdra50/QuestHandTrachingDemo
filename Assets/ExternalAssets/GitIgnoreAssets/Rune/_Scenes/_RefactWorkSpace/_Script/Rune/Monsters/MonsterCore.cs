using System.Collections;
using System.Collections.Generic;
using NoonNight.Battle.Skill;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace NoonNight.Battle.Monsters
{
    public abstract class MonsterCore : MonoBehaviour, ISkillEffectAffectable
    {
        public BoolReactiveProperty IsAlive = new BoolReactiveProperty(true);
        [SerializeField] protected Parameters _baseParams;
        
        public abstract ReactiveProperty<Parameters> CurrentParams { get; }

        public void Affect(SkillEntity skill)
        {
            print($"Affect {skill}");
            CurrentParams.SetValueAndForceNotify(skill.StartSkillEffect(CurrentParams.Value));

        }
    }
}
