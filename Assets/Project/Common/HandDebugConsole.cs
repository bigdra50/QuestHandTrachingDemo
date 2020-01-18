using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class HandDebugConsole : MonoBehaviour
{
    [SerializeField] private OVRHand _rHand;
    [SerializeField] private OVRHand _lHand;
    [SerializeField] private OVRSkeleton _rSkelton;
    [SerializeField] private OVRSkeleton _lSkelton;
    [SerializeField] private TMP_Text _tmpText;

    void Start()
    {
        var console = OVRLipSyncDebugConsole.instance;
        var indexTipPos = _rSkelton.Bones[(int) OVRSkeleton.BoneId.Hand_IndexTip].Transform.position;

        this.UpdateAsObservable()
            .Where(_ => false)
            .Subscribe(_ =>
            {
                console.AddMessage($"ThumbPinchingStrength: {_lHand.GetFingerPinchStrength(OVRHand.HandFinger.Thumb):F2}", Color.red);
                console.AddMessage($"IndexPinchingStrength: {_lHand.GetFingerPinchStrength(OVRHand.HandFinger.Index):F2}", Color.red);
                console.AddMessage($"MiddlePinchingStrength: {_lHand.GetFingerPinchStrength(OVRHand.HandFinger.Middle):F2}", Color.red);
                console.AddMessage($"RingPinchingStrength: {_lHand.GetFingerPinchStrength(OVRHand.HandFinger.Ring):F2}", Color.red);
                console.AddMessage($"PinkyPinchingStrength: {_lHand.GetFingerPinchStrength(OVRHand.HandFinger.Pinky):F2}", Color.red);

                console.AddMessage("\n", Color.red);

                console.AddMessage($"ThumbPinchingStrength: {_rHand.GetFingerPinchStrength(OVRHand.HandFinger.Thumb):F2}", Color.cyan);
                console.AddMessage($"IndexPinchingStrength: {_rHand.GetFingerPinchStrength(OVRHand.HandFinger.Index):F2}", Color.cyan);
                console.AddMessage($"MiddlePinchingStrength: {_rHand.GetFingerPinchStrength(OVRHand.HandFinger.Middle):F2}", Color.cyan);
                console.AddMessage($"RingPinchingStrength: {_rHand.GetFingerPinchStrength(OVRHand.HandFinger.Ring):F2}", Color.cyan);
                console.AddMessage($"PinkyPinchingStrength: {_rHand.GetFingerPinchStrength(OVRHand.HandFinger.Pinky):F2}", Color.cyan);
            });

        var handGesture = new OVRHandGestureIdentifier();
        this.UpdateAsObservable()
            .Where(_ => _tmpText != null)
            .Where(_=>false)
            .Subscribe(_ =>
            {
                //Debug.Log("HandGesture");
                //Debug.Log($"<R> Thumb: {handGesture.GetConditionBendFinger(_rSkelton, OVRPlugin.HandFinger.Thumb):F2} Index: {handGesture.GetConditionBendFinger(_rSkelton, OVRPlugin.HandFinger.Index):F2} Middle: {handGesture.GetConditionBendFinger(_rSkelton, OVRPlugin.HandFinger.Middle):F2} Ring: {handGesture.GetConditionBendFinger(_rSkelton, OVRPlugin.HandFinger.Ring):F2} Pinky: {handGesture.GetConditionBendFinger(_rSkelton, OVRPlugin.HandFinger.Pinky):F2}");
                //Debug.Log("\n");
                //Debug.Log($"<L> Thumb: {handGesture.GetConditionBendFinger(_lSkelton, OVRPlugin.HandFinger.Thumb):F2} Index: {handGesture.GetConditionBendFinger(_lSkelton, OVRPlugin.HandFinger.Index):F2} Middle: {handGesture.GetConditionBendFinger(_lSkelton, OVRPlugin.HandFinger.Middle):F2} Ring: {handGesture.GetConditionBendFinger(_lSkelton, OVRPlugin.HandFinger.Ring):F2} Pinky: {handGesture.GetConditionBendFinger(_lSkelton, OVRPlugin.HandFinger.Pinky):F2}");
                _tmpText.text = "";
                _tmpText.text += "HandGestureObserver";
                _tmpText.text += "\n";
                _tmpText.text +=
                    $"<R> Thumb: {handGesture.GetConditionBendFinger(_rSkelton, OVRPlugin.HandFinger.Thumb):F2} Index: {handGesture.GetConditionBendFinger(_rSkelton, OVRPlugin.HandFinger.Index):F2} Middle: {handGesture.GetConditionBendFinger(_rSkelton, OVRPlugin.HandFinger.Middle):F2} Ring: {handGesture.GetConditionBendFinger(_rSkelton, OVRPlugin.HandFinger.Ring):F2} Pinky: {handGesture.GetConditionBendFinger(_rSkelton, OVRPlugin.HandFinger.Pinky):F2}";
                _tmpText.text += "\n";
                _tmpText.text +=
                    $"<L> Thumb: {handGesture.GetConditionBendFinger(_lSkelton, OVRPlugin.HandFinger.Thumb):F2} Index: {handGesture.GetConditionBendFinger(_lSkelton, OVRPlugin.HandFinger.Index):F2} Middle: {handGesture.GetConditionBendFinger(_lSkelton, OVRPlugin.HandFinger.Middle):F2} Ring: {handGesture.GetConditionBendFinger(_lSkelton, OVRPlugin.HandFinger.Ring):F2} Pinky: {handGesture.GetConditionBendFinger(_lSkelton, OVRPlugin.HandFinger.Pinky):F2}";
            });

        this.UpdateAsObservable()
            .Where(_ => _tmpText != null)
            .Subscribe(_ =>
            {
                _tmpText.text = "";
                _tmpText.text += "Finger Bone Vector\n";
                var fingerBones = GetFingerBoneVectors(_rSkelton, OVRPlugin.HandFinger.Thumb);
                _tmpText.text += $"a1 = {fingerBones[0]:F2}, a2 = {fingerBones[1]:F2}, a3 = {fingerBones[2]:F2}\n";

                fingerBones = GetFingerBoneVectors(_rSkelton, OVRPlugin.HandFinger.Index);
                _tmpText.text += $"b1 = {fingerBones[0]:F2}, b2 = {fingerBones[1]:F2}, b3 = {fingerBones[2]:F2}\n";

                fingerBones = GetFingerBoneVectors(_rSkelton, OVRPlugin.HandFinger.Middle);
                _tmpText.text += $"c1 = {fingerBones[0]:F2}, c2 = {fingerBones[1]:F2}, c3 = {fingerBones[2]:F2}\n";


                fingerBones = GetFingerBoneVectors(_rSkelton, OVRPlugin.HandFinger.Ring);
                _tmpText.text += $"d1 = {fingerBones[0]:F2}, d2 = {fingerBones[1]:F2}, d3 = {fingerBones[2]:F2}\n";

                fingerBones = GetFingerBoneVectors(_rSkelton, OVRPlugin.HandFinger.Pinky);
                _tmpText.text += $"e1 = {fingerBones[0]}, e2 = {fingerBones[1]}, e3 = {fingerBones[2]}\n";
                _tmpText.text += "\n";
                
                _tmpText.text += "HandGestureObserver\n";
                _tmpText.text +=
                    $"<R> Thumb: {handGesture.GetConditionBendFinger(_rSkelton, OVRPlugin.HandFinger.Thumb):F2} Index: {handGesture.GetConditionBendFinger(_rSkelton, OVRPlugin.HandFinger.Index):F2} Middle: {handGesture.GetConditionBendFinger(_rSkelton, OVRPlugin.HandFinger.Middle):F2} Ring: {handGesture.GetConditionBendFinger(_rSkelton, OVRPlugin.HandFinger.Ring):F2} Pinky: {handGesture.GetConditionBendFinger(_rSkelton, OVRPlugin.HandFinger.Pinky):F2}\n";
                _tmpText.text +=
                    $"<L> Thumb: {handGesture.GetConditionBendFinger(_lSkelton, OVRPlugin.HandFinger.Thumb):F2} Index: {handGesture.GetConditionBendFinger(_lSkelton, OVRPlugin.HandFinger.Index):F2} Middle: {handGesture.GetConditionBendFinger(_lSkelton, OVRPlugin.HandFinger.Middle):F2} Ring: {handGesture.GetConditionBendFinger(_lSkelton, OVRPlugin.HandFinger.Ring):F2} Pinky: {handGesture.GetConditionBendFinger(_lSkelton, OVRPlugin.HandFinger.Pinky):F2}\n";

                _tmpText.text += "\n";
                _tmpText.text +=
                    $"IsStraight? T: {handGesture.IsStraightFinger(_rSkelton, OVRPlugin.HandFinger.Thumb)}, I: {handGesture.IsStraightFinger(_rSkelton, OVRPlugin.HandFinger.Index)}, M: {handGesture.IsStraightFinger(_rSkelton, OVRPlugin.HandFinger.Middle)}, R: {handGesture.IsStraightFinger(_rSkelton, OVRPlugin.HandFinger.Ring)}, P: {handGesture.IsStraightFinger(_rSkelton, OVRPlugin.HandFinger.Pinky)}\n";
            });
    }

    List<Vector3> GetFingerBoneVectors(OVRSkeleton skeleton, OVRPlugin.HandFinger fingerType)
    {
        var fingerBones = new List<Vector3>();
        var fingers = new List<OVRSkeleton.BoneId>();
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
            fingerBones.Add(v);
        }

        return fingerBones;
    }
}