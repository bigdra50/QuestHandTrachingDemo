using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class LogMenuTest : MonoBehaviour
{

    void Start()
    {
        Observable.Interval(TimeSpan.FromSeconds(1))
            .Subscribe(_ =>
            {
                Debug.LogFormat($"Time: {System.DateTime.Now}");
            });
    }
}
