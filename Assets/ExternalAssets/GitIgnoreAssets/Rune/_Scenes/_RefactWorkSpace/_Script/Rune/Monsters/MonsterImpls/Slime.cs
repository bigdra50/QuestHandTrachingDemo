using System.Collections;
using System.Collections.Generic;
using NoonNight.Battle.Skill;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace NoonNight.Battle.Monsters
{
    public class Slime : MonsterCore
    {
        private ReactiveProperty<Parameters> _currentParams = new ReactiveProperty<Parameters>();
        public override ReactiveProperty<Parameters> CurrentParams => _currentParams;

        public void Initialize()
        {
            _currentParams.Value = _baseParams;
        }
        
        void Start()
        {
            Initialize();

            #region 攻撃された時
            
            this.GetComponentInChildren<Collider>()
                .OnTriggerEnterAsObservable()
                .Where(c => c.CompareTag("Skill"))
                .Subscribe(c =>
                {
                    Affect(c.gameObject.GetComponentInParent<SkillEntity>());
                });
            
            #endregion

            _currentParams.DistinctUntilChanged(p => p._hp)
                .Subscribe(p =>
                {
                    if (p._hp <= 0)
                    {
                        IsAlive.SetValueAndForceNotify(false);
                    }
                    print(p._hp);
                });

            IsAlive.Where(a => !a)
                .Subscribe(_ =>
                {
                    Dead();
                });
        }

        void Dead()
        {
            print($"{this.gameObject.name}: Dead");
            var startTime = Time.timeSinceLevelLoad;
            this.UpdateAsObservable()
                .Subscribe(_ =>
                {
                    var diff = Time.timeSinceLevelLoad - startTime;
                    var rate = diff / 1f;
                    gameObject.GetComponentInChildren<Renderer>().material.SetFloat("_Destruction", Mathf.Lerp(0f, 1f, rate));
                });
            // 消滅エフェクトとか
            
            Destroy(gameObject, 3f);
        }

    }
}
