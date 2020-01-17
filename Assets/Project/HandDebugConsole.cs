using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class HandDebugConsole: MonoBehaviour
{
    [SerializeField] private OVRHand _rHand;
    [SerializeField] private OVRHand _lHand;
    [SerializeField] private OVRSkeleton _rSkelton;
    [SerializeField] private OVRSkeleton _lSkelton;

    void Start()
    {
        var console = OVRLipSyncDebugConsole.instance;
        var indexTipPos = _rSkelton.Bones[(int) OVRSkeleton.BoneId.Hand_IndexTip].Transform.position;

        this.UpdateAsObservable()
            .Where(_=> false)
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
            .Subscribe(_ =>
            {
                console.AddMessage("HandGesture", Color.white);
                console.AddMessage($"<R> Thumb: {handGesture.GetConditionBendFinger(_rSkelton, OVRPlugin.HandFinger.Thumb):F2} Index: {handGesture.GetConditionBendFinger(_rSkelton, OVRPlugin.HandFinger.Index):F2} Middle: {handGesture.GetConditionBendFinger(_rSkelton, OVRPlugin.HandFinger.Middle):F2} Ring: {handGesture.GetConditionBendFinger(_rSkelton, OVRPlugin.HandFinger.Ring):F2} Pinky: {handGesture.GetConditionBendFinger(_rSkelton, OVRPlugin.HandFinger.Pinky):F2}", Color.white);
                console.AddMessage("\n", Color.red);
                console.AddMessage($"<L> Thumb: {handGesture.GetConditionBendFinger(_lSkelton, OVRPlugin.HandFinger.Thumb):F2} Index: {handGesture.GetConditionBendFinger(_lSkelton, OVRPlugin.HandFinger.Index):F2} Middle: {handGesture.GetConditionBendFinger(_lSkelton, OVRPlugin.HandFinger.Middle):F2} Ring: {handGesture.GetConditionBendFinger(_lSkelton, OVRPlugin.HandFinger.Ring):F2} Pinky: {handGesture.GetConditionBendFinger(_lSkelton, OVRPlugin.HandFinger.Pinky):F2}", Color.white);
            });

    }
    
}
