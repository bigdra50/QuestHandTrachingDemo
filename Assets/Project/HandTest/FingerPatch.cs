using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class FingerPatch : MonoBehaviour
{
    private ReactiveProperty<bool> _isFingerPatch = new ReactiveProperty<bool>();

    public IReadOnlyReactiveProperty<bool> IsFingerPatch => _isFingerPatch;

    void Start()
    {
        var updateStream = this.UpdateAsObservable();
        
        
    }
}