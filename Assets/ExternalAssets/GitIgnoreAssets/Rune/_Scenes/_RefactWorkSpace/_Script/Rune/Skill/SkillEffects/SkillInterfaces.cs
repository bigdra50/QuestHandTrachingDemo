using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NoonNight.Battle.Skill.SkillEffects
{
    /// <summary>
    /// ダメージを与えるスキル
    /// </summary>
    public interface IDamager
    {
        Parameters ApplyDamage(Parameters targetParameters);
    }

    /// <summary>
    /// 移動させるスキル
    /// </summary>
    public interface IMover
    {

    }

    /// <summary>
    /// 回復効果
    /// </summary>
    public interface IHealer
    {
        
    }

    /// <summary>
    /// 状態変化させるスキル
    /// </summary>
    public interface IStatusChanger
    {
        
    }
    
}
