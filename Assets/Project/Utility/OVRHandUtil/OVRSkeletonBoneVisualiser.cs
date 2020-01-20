using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UniRx.Triggers;
using Unity.UIWidgets.foundation;
using UnityEngine;

public class OVRSkeletonBoneVisualiser : MonoBehaviour
{
    [SerializeField] private OVRSkeleton _rSkeleton;
    [SerializeField] private OVRSkeleton _lSkeleton;

    void Start()
    {
        var rBoneMap = new Dictionary<OVRSkeleton.BoneId, GameObject>();
        var lBoneMap = new Dictionary<OVRSkeleton.BoneId, GameObject>();

        foreach (OVRSkeleton.BoneId bone in Enum.GetValues(typeof(OVRSkeleton.BoneId)))
        {
            if (bone == OVRSkeleton.BoneId.Invalid || bone == OVRSkeleton.BoneId.Max || bone == OVRSkeleton.BoneId.Hand_Start ||
                bone == OVRSkeleton.BoneId.Hand_MaxSkinnable || bone == OVRSkeleton.BoneId.Hand_End) continue;

            var skeletonSphereScale = new Vector3(.02f, .02f, .02f);
            rBoneMap[bone] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            rBoneMap[bone].transform.parent = _rSkeleton.transform;
            rBoneMap[bone].name = bone.ToString();
            rBoneMap[bone].tag = "Finger";
            rBoneMap[bone].GetComponent<Renderer>().material.color = Color.red;
            rBoneMap[bone].GetComponent<Collider>().isTrigger = true;
            rBoneMap[bone].AddComponent<Rigidbody>();
            var rRigid = rBoneMap[bone].GetComponent<Rigidbody>();
            rRigid.useGravity = false;
            rRigid.isKinematic = true;
            rBoneMap[bone].transform.localScale = skeletonSphereScale;

            lBoneMap[bone] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            lBoneMap[bone].transform.parent = _lSkeleton.transform;
            lBoneMap[bone].name = bone.ToString();
            lBoneMap[bone].tag = "Finger";
            lBoneMap[bone].GetComponent<Renderer>().material.color = Color.blue;
            lBoneMap[bone].GetComponent<Collider>().isTrigger = true;
            lBoneMap[bone].AddComponent<Rigidbody>();
            var lRigid = lBoneMap[bone].GetComponent<Rigidbody>();
            lRigid.useGravity = false;
            lRigid.isKinematic = true;
            lBoneMap[bone].transform.localScale = skeletonSphereScale;
        }

        this.UpdateAsObservable()
            .Where(_ => !rBoneMap.isEmpty() || !lBoneMap.isEmpty())
            .Subscribe(_ =>
            {
                foreach (OVRSkeleton.BoneId bone in Enum.GetValues(typeof(OVRSkeleton.BoneId)))
                {
                    if (bone == OVRSkeleton.BoneId.Invalid || bone == OVRSkeleton.BoneId.Max || bone == OVRSkeleton.BoneId.Hand_Start ||
                        bone == OVRSkeleton.BoneId.Hand_MaxSkinnable || bone == OVRSkeleton.BoneId.Hand_End) continue;

                    rBoneMap[bone].transform.position = _rSkeleton.Bones[(int) bone].Transform.position;
                    rBoneMap[bone].transform.rotation = _rSkeleton.Bones[(int) bone].Transform.rotation;
                    rBoneMap[bone].transform.localScale = new Vector3(.02f, .02f, .02f);

                    lBoneMap[bone].transform.position = _lSkeleton.Bones[(int) bone].Transform.position;
                    lBoneMap[bone].transform.rotation = _lSkeleton.Bones[(int) bone].Transform.rotation;
                    lBoneMap[bone].transform.localScale = new Vector3(.02f, .02f, .02f);
                }
            });
    }
}