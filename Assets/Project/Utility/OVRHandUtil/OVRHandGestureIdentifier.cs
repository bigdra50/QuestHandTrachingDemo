using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityScript.Lang;

public class OVRHandGestureIdentifier 
{
    public float GetConditionBendFinger(OVRSkeleton skeleton, OVRPlugin.HandFinger fingerType)
    {
        Vector3? oldVec = null;
        var dot = 1.0f;
        List<OVRSkeleton.BoneId> fingers = new List<OVRSkeleton.BoneId>();
        switch (fingerType)
        {
            case OVRPlugin.HandFinger.Thumb:
                fingers = new List<OVRSkeleton.BoneId>()
                {
                    OVRSkeleton.BoneId.Hand_ThumbTip,
                    OVRSkeleton.BoneId.Hand_Thumb3,
                    OVRSkeleton.BoneId.Hand_Thumb2,
                    OVRSkeleton.BoneId.Hand_Thumb1,
                };
                break;
            case OVRPlugin.HandFinger.Index:
                fingers = new List<OVRSkeleton.BoneId>()
                {
                    OVRSkeleton.BoneId.Hand_IndexTip,
                    OVRSkeleton.BoneId.Hand_Index3,
                    OVRSkeleton.BoneId.Hand_Index2,
                    OVRSkeleton.BoneId.Hand_Index1,
                };
                break;
            case OVRPlugin.HandFinger.Middle:
                fingers = new List<OVRSkeleton.BoneId>()
                {
                    OVRSkeleton.BoneId.Hand_MiddleTip,
                    OVRSkeleton.BoneId.Hand_Middle3,
                    OVRSkeleton.BoneId.Hand_Middle2,
                    OVRSkeleton.BoneId.Hand_Middle1,
                };
                break;
            case OVRPlugin.HandFinger.Ring:
                fingers = new List<OVRSkeleton.BoneId>()
                {
                    OVRSkeleton.BoneId.Hand_RingTip,
                    OVRSkeleton.BoneId.Hand_Ring3,
                    OVRSkeleton.BoneId.Hand_Ring2,
                    OVRSkeleton.BoneId.Hand_Ring1,
                };
                break;
            case OVRPlugin.HandFinger.Pinky:
                fingers = new List<OVRSkeleton.BoneId>()
                {
                    OVRSkeleton.BoneId.Hand_PinkyTip,
                    OVRSkeleton.BoneId.Hand_Pinky3,
                    OVRSkeleton.BoneId.Hand_Pinky2,
                    OVRSkeleton.BoneId.Hand_Pinky1,
                };
                break;
        }

        for (var i = 0; i < fingers.Count - 1; i++)
        {
            var v = (skeleton.Bones[(int) fingers[i + 1]].Transform.position - skeleton.Bones[(int) fingers[0]].Transform.position).normalized;
            if (oldVec.HasValue)
            {
                dot *= Vector3.Dot(v, oldVec.Value);
            }

            oldVec = v; //ひとつ前の指ベクトル
        }

        return dot;
    }

    public bool IsExtendedFinger(OVRSkeleton skeleton, OVRPlugin.HandFinger fingerType)
    {
        //var threshold = fingerType == OVRPlugin.HandFinger.Thumb ? .94f : .96f;
        var threshold = .95f;
        
        return GetConditionBendFinger(skeleton, fingerType) >= threshold;
    }
    
    public bool IsExtendedFinger(OVRSkeleton skeleton, OVRPlugin.HandFinger fingerType, float threshold)
    {
        return GetConditionBendFinger(skeleton, fingerType) >= threshold;
    }
}