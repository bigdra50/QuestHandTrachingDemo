//using System;
//using System.Collections;
//using System.Collections.Generic;
//using NoonNight.Battle.Inputs;
//using UniRx;
//using UniRx.Triggers;
//using UnityEngine;
//
//namespace NoonNight.Battle.Players.InputImpls
//{
//	public class OVRInputEventProvider : MonoBehaviour, IInputEventProvider
//	{
//		readonly ReactiveProperty<Vector3> _moveDirection = new ReactiveProperty<Vector3>();
//		readonly ReactiveProperty<Vector3> _rotDirection = new ReactiveProperty<Vector3>();
//		readonly ReactiveProperty<bool> _onTeleportButtonPushed = new ReactiveProperty<bool>();
//		readonly ReactiveProperty<bool> _onRuneButtonPushed = new ReactiveProperty<bool>();
//		readonly ReactiveProperty<bool> _onOpenBookAction = new ReactiveProperty<bool>();
//
//		public IReadOnlyReactiveProperty<Vector3> MoveDirection => _moveDirection;
//		public IReadOnlyReactiveProperty<Vector3> RotDirection => _rotDirection;
//		public IReadOnlyReactiveProperty<bool> OnPressActionButton { get; }
//		public IReadOnlyReactiveProperty<bool> OnPressCancelButton { get; }
//		public IReadOnlyReactiveProperty<bool> OnPressRightMenuButton { get; }
//		public IReadOnlyReactiveProperty<bool> OnPressLeftMenuButton { get; }
//		public IReadOnlyReactiveProperty<bool> OnTeleportButtonPushed => _onTeleportButtonPushed;
//		public IReadOnlyReactiveProperty<bool> OnRuneButtonPushed => _onRuneButtonPushed;
//		public IReadOnlyReactiveProperty<bool> OnOpenBookAction => _onOpenBookAction;
//
//		private void Start()
//		{
//
//			var updateStream = this.UpdateAsObservable();
//			
//			
//			var leftPressUp = OVRInputRx.OnLeftTouchPadPressUpAsObservable()
//				.Subscribe(_ =>
//				{
//					_moveDirection.SetValueAndForceNotify(Vector3.zero);
//					_onTeleportButtonPushed.SetValueAndForceNotify(false);
//				});
//			
//			updateStream
//				.Where(_ => this.LeftTouchPadPress().Value)		
//				.Select(o => new Vector3(this.LeftTouchPosition().Value.x, 0f, this.LeftTouchPosition().Value.y))
//				.Do(axis => print(axis))
//				.Subscribe(axis =>
//				{
//					_moveDirection.SetValueAndForceNotify(axis);
//					_onTeleportButtonPushed.SetValueAndForceNotify(true);
//				});
//				
//
//
//			this.OnRightTouchPadPressDownAsObservable()
//				.Select(_ => new Vector3(this.RightTouchPosition().Value.x, 0f, this.RightTouchPosition().Value.y))
//				.Subscribe(axis => { _rotDirection.SetValueAndForceNotify(axis); });
//		}
//	}
//}
