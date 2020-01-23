using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace NoonNight.Battle.Inputs
{
    public interface IInputEventProvider 
    {
        IReadOnlyReactiveProperty<Vector3> MoveDirection { get; }
        IReadOnlyReactiveProperty<Vector3> RotDirection { get; }
        IReadOnlyReactiveProperty<bool> OnTeleportButtonPushed { get; }
        IReadOnlyReactiveProperty<bool> OnRuneButtonPushed { get; }
        IReadOnlyReactiveProperty<bool> OnOpenBookAction{ get; }
        
    }
}