using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UniRx;
using UnityEngine;

public class PlayerHandActions : MonoBehaviour
{
    [SerializeField] private Deadable[] _deadable;
    [SerializeField] private GameObject[] _effect;
    void Start()
    {
        var handGesture = GetComponent<HandGestureRx>();
        handGesture.IsLFingerPatch
            .Where(l => l)
            .Subscribe(_ =>
            {
                print("handpatch start");
                foreach (var e in _effect)
                {
                   e.SetActive(true);
                   Observable.Timer(TimeSpan.FromSeconds(3f)).Subscribe(t => e.SetActive(false));
                }
                foreach (var deadable in _deadable)
                {
                    deadable.Dead();
                }
            });

    }
}
