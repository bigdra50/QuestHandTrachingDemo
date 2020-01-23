using System;
using System.Collections;
using System.Collections.Generic;
using NoonNight.Battle.Inputs;
using UniRx;
using UniRx.Diagnostics;
using UniRx.Triggers;
using UnityEngine;

namespace NoonNight.Battle.Players.InputImpls
{
	public class DebugKeyInputEventProvider : MonoBehaviour, NoonNight.Battle.Inputs.IInputEventProvider
	{
		private readonly ReactiveProperty<Vector3> _moveDirection = new ReactiveProperty<Vector3>();
		private readonly ReactiveProperty<Vector3> _rotDirection = new ReactiveProperty<Vector3>();
		private readonly ReactiveProperty<bool> _onTeleportButtonPushed = new BoolReactiveProperty();
		private readonly ReactiveProperty<bool> _onRuneButtonPushed = new ReactiveProperty<bool>();
		private readonly ReactiveProperty<bool> _onOpenBookAction = new BoolReactiveProperty();

		public IReadOnlyReactiveProperty<Vector3> MoveDirection => _moveDirection;
		public IReadOnlyReactiveProperty<Vector3> RotDirection => _rotDirection;
		public IReadOnlyReactiveProperty<bool> OnTeleportButtonPushed => _onTeleportButtonPushed;
		public IReadOnlyReactiveProperty<bool> OnRuneButtonPushed => _onRuneButtonPushed;
		public IReadOnlyReactiveProperty<bool> OnOpenBookAction => _onOpenBookAction;

		protected void Start()
		{
			var updateStream = this.UpdateAsObservable();
			
			updateStream
				.Select(_ => new Vector3(Input.GetAxisRaw("Horizontal1"), 0f, Input.GetAxisRaw("Vertical1")))
//				.Do(x =>print(x))
				.Subscribe(x => _moveDirection.SetValueAndForceNotify(x));

			updateStream
				.Select(_ => new Vector3(Input.GetAxisRaw("Horizontal2"), 0f, Input.GetAxisRaw("Vertical2")))
				.Subscribe(x => _rotDirection.SetValueAndForceNotify(x));
			
			
            updateStream.Select(_ => Input.GetKey(KeyCode.LeftShift))
                .DistinctUntilChanged()
                .Subscribe(x => { _onRuneButtonPushed.SetValueAndForceNotify(x); });
		}
	}
}
