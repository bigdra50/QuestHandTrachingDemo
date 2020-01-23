using System;
using System.Collections;
using System.Collections.Generic;
using NoonNight.Battle.Skill;
using UnityEngine;

namespace NoonNight.Battle.Skill
{
    //[CreateAssetMenu(menuName = "BaseParameters", fileName = "BaseParameters")]
    [Serializable]
    public struct Parameters
    {
        //public string _name;
        public int _lv;
        public int _exp;
        public float _hp;
        public float _atk;
        public float _def;
        public float _speed;
        public ElementType _element;
        
    }
}
