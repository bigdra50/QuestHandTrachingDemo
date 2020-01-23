using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ModestTree;
using NoonNight.Battle.Skill.Magics;
using NoonNight.Battle.RuneCanvas;
using NoonNight.Battle.Skill;
using NoonNight.Utilities;
using UnityEngine;
using MagicType = NoonNight.Battle.Skill.Magics.MagicType;

namespace NoonNight.Battle.Managers
{
    public class MagicGenerator : MonoBehaviour
    {
        [SerializeField] private MagicTable _magicTable;
        [SerializeField] private EmitterTable _magicEmitters;

        public void TryGenerateMagic(Queue<MagicType> magicTypes, Parameters userParams)
        {
            var magicType = MagicType.NONE;
            while (!magicTypes.IsEmpty())
            {
                magicType |= magicTypes.Dequeue();
            }

            if (magicType == MagicType.NONE) return;
            try
            {
                var magic = _magicTable.GetTable()[magicType];
                magic = Instantiate(magic, _magicEmitters.GetTable()[magic.Emitter].transform.position, Quaternion.identity, null);
                magic.Initialize(userParams);
            }
            catch (KeyNotFoundException e)
            {
                Debug.LogError(e);
            }

        }
        
    }

    #region SerializableTable
    [System.Serializable]
    public class MagicTable : Utilities.TableBase<MagicType, MagicEntity, MagicPair>
    {
        
    }

    [System.Serializable]
    public class MagicPair : Utilities.KeyAndValue<MagicType, MagicEntity>
    {
        public MagicPair(MagicType key, MagicEntity value) : base(key, value)
        {
        }

        public MagicPair(KeyValuePair<MagicType, MagicEntity> pair) : base(pair)
        {
        }
    }
    
    [System.Serializable]
    public class EmitterTable : Utilities.TableBase<EmitterPos, Transform, EmitterPair>
    {
        
    }

    [System.Serializable]
    public class EmitterPair : Utilities.KeyAndValue<EmitterPos, Transform>
        {
            public EmitterPair(EmitterPos key, Transform value) : base(key, value)
            {
            }

            public EmitterPair(KeyValuePair<EmitterPos, Transform> pair) : base(pair)
            {
            }

        }
    #endregion
}
