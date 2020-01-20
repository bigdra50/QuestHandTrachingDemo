using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class QuestInputEventProvider : MonoBehaviour
{
    readonly ReactiveProperty<Vector3> _moveDirection = new ReactiveProperty<Vector3>();
    readonly ReactiveProperty<Vector3> _rotDirection = new ReactiveProperty<Vector3>();
    readonly ReactiveProperty<bool> _onPressActionButton = new ReactiveProperty<bool>();
    readonly ReactiveProperty<bool> _onPressCancelButton = new ReactiveProperty<bool>();
    readonly ReactiveProperty<float> _onGripRightHand = new ReactiveProperty<float>();
    readonly ReactiveProperty<float> _onGripLeftHand = new ReactiveProperty<float>();

    public IReadOnlyReactiveProperty<Vector3> MoveDirection => _moveDirection;
    public IReadOnlyReactiveProperty<Vector3> RotDirection => _rotDirection;
    public IReadOnlyReactiveProperty<bool> OnPressActionButton => _onPressActionButton;
    public IReadOnlyReactiveProperty<bool> OnPressCancelButton => _onPressCancelButton;
    public IReadOnlyReactiveProperty<float> OnGripRightHand => _onGripRightHand;
    public IReadOnlyReactiveProperty<float> OnGripLeftHand => _onGripLeftHand;

    void Start()
    {
        var updateStream = this.UpdateAsObservable();

        OVRInputRx.OnPressAxis2DAsObservable(OVRInput.Axis2D.SecondaryThumbstick)
            .Select(axis => new Vector3(axis.x, 0f, axis.y))
            .Do(axis => print($"Right Axis:{axis}"))
            .Subscribe(axis => { _rotDirection.SetValueAndForceNotify(axis); });

        OVRInputRx.OnPressAxis2DAsObservable(OVRInput.Axis2D.PrimaryThumbstick)
            .Select(axis => new Vector3(axis.x, 0f, axis.y))
            .Do(axis => print($"Left Axis: {axis}"))
            .Subscribe(axis => _moveDirection.SetValueAndForceNotify(axis));

        OVRInputRx.OnPressAxis1DAsObservable(OVRInput.Axis1D.SecondaryHandTrigger)
            .Do(axis => print($"Right Grip: {axis}"))
            .Subscribe(axis => { _onGripRightHand.SetValueAndForceNotify(axis);});
            
        OVRInputRx.OnPressAxis1DAsObservable(OVRInput.Axis1D.PrimaryHandTrigger)
            .Do(axis => print($"Left Grip: {axis}"))
            .Subscribe(axis => { _onGripLeftHand.SetValueAndForceNotify(axis);});
    }
}
