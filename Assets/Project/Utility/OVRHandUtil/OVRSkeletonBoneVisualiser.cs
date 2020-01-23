using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UniRx;
using UniRx.Triggers;
using Unity.UIWidgets.foundation;
using UnityEngine;

public class OVRSkeletonBoneVisualiser : MonoBehaviour
{
    [SerializeField] private OVRSkeleton _rSkeleton;
    [SerializeField] private OVRSkeleton _lSkeleton;
//    [SerializeField] private GameObject _fingerParticle;

    Dictionary<OVRSkeleton.BoneId, GameObject> _rBoneMap = new Dictionary<OVRSkeleton.BoneId, GameObject>();
    Dictionary<OVRSkeleton.BoneId, GameObject> _lBoneMap = new Dictionary<OVRSkeleton.BoneId, GameObject>();

    public Dictionary<OVRSkeleton.BoneId, GameObject> RBoneMap => _rBoneMap;
    public Dictionary<OVRSkeleton.BoneId, GameObject> LBoneMap => _lBoneMap;

    void Start()
    {
        GameObject p1 = null;
        GameObject p2 = null; //tmp

        foreach (OVRSkeleton.BoneId bone in Enum.GetValues(typeof(OVRSkeleton.BoneId)))
        {
            //if (bone == OVRSkeleton.BoneId.Invalid || bone == OVRSkeleton.BoneId.Max || bone == OVRSkeleton.BoneId.Hand_Start ||
            //     bone == OVRSkeleton.BoneId.Hand_MaxSkinnable || bone == OVRSkeleton.BoneId.Hand_End) continue;


            var skeletonSphereScale = new Vector3(.02f, .02f, .02f);
            _rBoneMap[bone] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            _rBoneMap[bone].transform.parent = _rSkeleton.transform;
            _rBoneMap[bone].name = bone.ToString();
            _rBoneMap[bone].tag = "Finger";
            _rBoneMap[bone].GetComponent<Renderer>().material.color = Color.red;
            if (bone == OVRSkeleton.BoneId.Hand_ThumbTip || bone == OVRSkeleton.BoneId.Hand_MiddleTip || bone == OVRSkeleton.BoneId.Hand_IndexTip ||
                bone == OVRSkeleton.BoneId.Hand_PinkyTip || bone == OVRSkeleton.BoneId.Hand_RingTip)
                _rBoneMap[bone].GetComponent<Renderer>().material.color = Color.cyan;
            _rBoneMap[bone].GetComponent<Collider>().isTrigger = true;
            _rBoneMap[bone].AddComponent<Rigidbody>();
            var rRigid = _rBoneMap[bone].GetComponent<Rigidbody>();
            rRigid.useGravity = false;
            rRigid.isKinematic = true;
            _rBoneMap[bone].transform.localScale = skeletonSphereScale;
            if (bone == OVRSkeleton.BoneId.Hand_Thumb2)
            {
                _rBoneMap[bone].transform.localScale *= 1.5f;
            }

            _lBoneMap[bone] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            _lBoneMap[bone].transform.parent = _lSkeleton.transform;
            _lBoneMap[bone].name = bone.ToString();
            _lBoneMap[bone].tag = "Finger";
            if (bone == OVRSkeleton.BoneId.Hand_ThumbTip || bone == OVRSkeleton.BoneId.Hand_MiddleTip || bone == OVRSkeleton.BoneId.Hand_IndexTip ||
                bone == OVRSkeleton.BoneId.Hand_PinkyTip || bone == OVRSkeleton.BoneId.Hand_RingTip)
            {
                _rBoneMap[bone].GetComponent<Renderer>().material.color = Color.cyan;
           //     p1 = Instantiate(_fingerParticle, _rSkeleton.Bones[(int) bone].Transform.position, Quaternion.identity, transform);
           //     p2 = Instantiate(_fingerParticle, _lSkeleton.Bones[(int) bone].Transform.position, Quaternion.identity, transform);
            }
            

            _lBoneMap[bone].GetComponent<Renderer>().material.color = Color.blue;
            _lBoneMap[bone].GetComponent<Collider>().isTrigger = true;
            _lBoneMap[bone].AddComponent<Rigidbody>();
            var lRigid = _lBoneMap[bone].GetComponent<Rigidbody>();
            lRigid.useGravity = false;
            lRigid.isKinematic = true;
            _lBoneMap[bone].transform.localScale = skeletonSphereScale;
            if (bone == OVRSkeleton.BoneId.Hand_Thumb2)
            {
                _rBoneMap[bone].transform.localScale *= 1.5f;
                _lBoneMap[bone].transform.localScale *= 1.5f;
            }
        }

        this.UpdateAsObservable()
            .Where(_ => !_rBoneMap.isEmpty() || !_lBoneMap.isEmpty())
            .Subscribe(_ =>
            {
                foreach (OVRSkeleton.BoneId bone in Enum.GetValues(typeof(OVRSkeleton.BoneId)))
                {
                    //if (bone == OVRSkeleton.BoneId.Invalid || bone == OVRSkeleton.BoneId.Max || bone == OVRSkeleton.BoneId.Hand_Start ||
                    //    bone == OVRSkeleton.BoneId.Hand_MaxSkinnable || bone == OVRSkeleton.BoneId.Hand_End) continue;


                    if (!_rBoneMap.ContainsKey(bone)) continue;
                    if (!_lBoneMap.ContainsKey(bone)) continue;

                    _rBoneMap[bone].transform.position = _rSkeleton.Bones[(int) bone].Transform.position;
                    _rBoneMap[bone].transform.rotation = _rSkeleton.Bones[(int) bone].Transform.rotation;
                    //_rBoneMap[bone].transform.localScale = new Vector3(.03f, .03f, .03f);

                    _lBoneMap[bone].transform.position = _lSkeleton.Bones[(int) bone].Transform.position;
                    _lBoneMap[bone].transform.rotation = _lSkeleton.Bones[(int) bone].Transform.rotation;
                    //_lBoneMap[bone].transform.localScale = new Vector3(.03f, .03f, .03f);
                  //  if (bone == OVRSkeleton.BoneId.Hand_ThumbTip || bone == OVRSkeleton.BoneId.Hand_MiddleTip ||
                  //      bone == OVRSkeleton.BoneId.Hand_IndexTip ||
                  //      bone == OVRSkeleton.BoneId.Hand_PinkyTip || bone == OVRSkeleton.BoneId.Hand_RingTip)
                  //  {
                  //     if(p1 != null) 
                  //      p1.transform.position = _rSkeleton.Bones[(int) bone].Transform.position;
                  //     if(p2 != null) 
                  //      p2.transform.position = _lSkeleton.Bones[(int) bone].Transform.position;
                  //  }
                }
            });
    }
}