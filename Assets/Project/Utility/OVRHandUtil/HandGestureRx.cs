using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class HandGestureRx : MonoBehaviour
{
    [SerializeField] private OVRSkeleton _rSkeleton;
    [SerializeField] private OVRSkeleton _lSkeleton;
    [SerializeField] private OVRHand _rHand;
    [SerializeField] private OVRHand _lHand;
    [SerializeField] private OVRSkeletonBoneVisualiser _skeletonVisualiser;

    private readonly ReactiveProperty<bool> _isRFingerPatch = new ReactiveProperty<bool>();
    private readonly ReactiveProperty<bool> _isLFingerPatch = new ReactiveProperty<bool>();

    public IReadOnlyReactiveProperty<bool> IsRFingerPatch => _isRFingerPatch;
    public IReadOnlyReactiveProperty<bool> IsLFingerPatch => _isLFingerPatch;
    

    private Subject<OVRSkeleton.BoneId> _rTouchBoneStream = new Subject<OVRSkeleton.BoneId>();
    private Subject<OVRSkeleton.BoneId> _lTouchBoneStream = new Subject<OVRSkeleton.BoneId>();
    
    
    // TODO tmp あとで消す
    [SerializeField] private GameObject[] _effect;
    [SerializeField] private Deadable[] _deadable;

    void Start()
    {
        if (_skeletonVisualiser == null) _skeletonVisualiser = GetComponent<OVRSkeletonBoneVisualiser>();

        var rMiddleTipCol = _skeletonVisualiser.RBoneMap[OVRSkeleton.BoneId.Hand_MiddleTip].GetComponent<Collider>();
        var lMiddleTipCol = _skeletonVisualiser.LBoneMap[OVRSkeleton.BoneId.Hand_MiddleTip].GetComponent<Collider>();

        var touchMiddleTipStream = _skeletonVisualiser.RBoneMap[OVRSkeleton.BoneId.Hand_MiddleTip].GetComponent<Collider>()
            .OnTriggerEnterAsObservable()
            .Where(col => col.CompareTag("Finger"));

        var updateStream = this.UpdateAsObservable();

        // 右中指が親指付け根に触れたときOnNext
        rMiddleTipCol.OnTriggerEnterAsObservable()
            .Where(col => col.gameObject.name == OVRSkeleton.BoneId.Hand_Thumb2.ToString())
            //.Do(_ => print("Touch Thumb2"))
            .Subscribe(col => _rTouchBoneStream.OnNext(OVRSkeleton.BoneId.Hand_Thumb2));

        // 右中指が親指先に触れたときOnNext
        rMiddleTipCol.OnTriggerEnterAsObservable()
            .Where(col => col.gameObject.name == OVRSkeleton.BoneId.Hand_ThumbTip.ToString())
            //.Do(_ => print("Touch ThumbTip"))
            .Subscribe(col => _rTouchBoneStream.OnNext(OVRSkeleton.BoneId.Hand_ThumbTip));

        // 右手で指パッチン
        _rTouchBoneStream.TakeUntilDestroy(this)
            .DistinctUntilChanged()
            .Buffer(2, 1)
            .Where(b => b[0] == OVRSkeleton.BoneId.Hand_ThumbTip && b[1] == OVRSkeleton.BoneId.Hand_Thumb2)
            .Subscribe(b =>
            {
                print("Right finger patch");
                _isRFingerPatch.SetValueAndForceNotify(true);
                Observable.Timer(TimeSpan.FromSeconds(3f)).Subscribe(_ => _isRFingerPatch.SetValueAndForceNotify(false));
            });

        // 左中指が親指付け根に触れたときOnNext
        lMiddleTipCol.OnTriggerEnterAsObservable()
            .Where(col => col.gameObject.name == OVRSkeleton.BoneId.Hand_Thumb2.ToString())
            //.Do(_ => print("Touch Thumb2"))
            .Subscribe(col => _lTouchBoneStream.OnNext(OVRSkeleton.BoneId.Hand_Thumb2));

        // 左中指が親指先に触れたときOnNext
        lMiddleTipCol.OnTriggerEnterAsObservable()
            .Where(col => col.gameObject.name == OVRSkeleton.BoneId.Hand_ThumbTip.ToString())
            //.Do(_ => print("Touch ThumbTip"))
            .Subscribe(col => _lTouchBoneStream.OnNext(OVRSkeleton.BoneId.Hand_ThumbTip));

        // 左手で指パッチン
        _lTouchBoneStream.TakeUntilDestroy(this)
            .DistinctUntilChanged()
            .Buffer(2, 1)
            .Where(b => b[0] == OVRSkeleton.BoneId.Hand_ThumbTip && b[1] == OVRSkeleton.BoneId.Hand_Thumb2)
            .Subscribe(_ =>
            {
                print("Left finger patch");
                _isLFingerPatch.SetValueAndForceNotify(true);
                Observable.Timer(TimeSpan.FromSeconds(3f)).Subscribe(t => _isLFingerPatch.SetValueAndForceNotify(false));
                
                // tmp 後で消す
                foreach (var e in _effect)
                {
                   e.SetActive(true);
                   Observable.Timer(TimeSpan.FromSeconds(3f)).Subscribe(t => e.SetActive(false));
                }
                //foreach (var deadable in _deadable)
                //{
                //    deadable.Dead();
                //}
            });

//        var mergedTouchMiddleTipStream = Observable.Merge(
//            rMiddleTipCol.OnTriggerEnterAsObservable().Where(col => col.gameObject.name == OVRSkeleton.BoneId.Hand_ThumbTip.ToString())
//                .Do(col => print($"Touch {col.gameObject.name}")),
//            rMiddleTipCol.OnTriggerEnterAsObservable().Where(col => col.gameObject.name == OVRSkeleton.BoneId.Hand_Thumb2.ToString())
//                .Do(col => print($"Touch {col.gameObject.name}"))
//        );

        //mergedTouchMiddleTipStream.Buffer(2, 1)
        //    .Where(c => c[0].gameObject.name == OVRSkeleton.BoneId.Hand_ThumbTip.ToString() &&
        //                c[1].gameObject.name == OVRSkeleton.BoneId.Hand_RingTip.ToString())
        //    .First()
        //    .Subscribe(_ => print("Finger patch"));

//        mergedTouchMiddleTipStream.Buffer(mergedTouchMiddleTipStream.Throttle((TimeSpan.FromMilliseconds(200))))
//            .Where(c => c.Count == 2)
//            .Where(c => c[0].gameObject.name == OVRSkeleton.BoneId.Hand_ThumbTip.ToString() &&
//                        c[1].gameObject.name == OVRSkeleton.BoneId.Hand_Thumb2.ToString())
//            .First()
//            .Subscribe(_ => print("Finger patch"));

        //var fingerPatchInterval = 250d;
    }
}