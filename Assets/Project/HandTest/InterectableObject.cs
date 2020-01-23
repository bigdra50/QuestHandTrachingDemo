using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class InterectableObject : MonoBehaviour
{
    [SerializeField] private GameObject _gpupCircle;
    void Start()
    {
        var OnTriggerEnterFinger = this.OnTriggerEnterAsObservable()
            .Select(col => col.tag)
            .Where(tag => tag == "Finger");

        OnTriggerEnterFinger
            .Do(_ => print("Touch"))
            .ThrottleFirst(TimeSpan.FromSeconds(5))
            .Subscribe(_ =>
            {
                var gpup = Instantiate(_gpupCircle, this.transform.position, Quaternion.identity, transform);
                Destroy(gpup, 5f);
            });
    }
}
