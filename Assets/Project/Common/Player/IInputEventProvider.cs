using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public interface IInputEventProvider
{
    IReadOnlyReactiveProperty<Vector3> MoveDirection { get; }
    IReadOnlyReactiveProperty<Vector3> RotDirection { get; }
    IReadOnlyReactiveProperty<bool> OnPressActionButton { get; }
    IReadOnlyReactiveProperty<bool> OnPressCancelButton { get; }
    IReadOnlyReactiveProperty<bool> OnPressRightMenuButton { get; }
    IReadOnlyReactiveProperty<bool> OnPressLeftMenuButton { get; }
}