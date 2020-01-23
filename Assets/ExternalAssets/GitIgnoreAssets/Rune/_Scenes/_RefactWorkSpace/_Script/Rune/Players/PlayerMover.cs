using System;
using System.Collections;
using System.Collections.Generic;
using NoonNight.Battle.Inputs;
using NoonNight.Battle.Players.InputImpls;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using NoonNight.Battle.Inputs;

namespace NoonNight.Battle.Players
{

	/// <summary>
	/// プレイヤーの移動系
	/// </summary>
	
	public class PlayerMover : MonoBehaviour
	{
		[SerializeField] private float _defaultMoveSpeed = 1f;
		[SerializeField] private float _defaultRotDegree = 1f;
		 [Inject]private NoonNight.Battle.Inputs.IInputEventProvider _inputEvent;
		private float _currentMoveSpeed;
		private Vector3 _moveVelocity;
		private readonly ReactiveProperty<PlayerMoveType> _currentMoveType = new ReactiveProperty<PlayerMoveType>(PlayerMoveType.Walk);
		
		public IReadOnlyReactiveProperty<PlayerMoveType> MoveType
		{
			get => _currentMoveType;
			set => _currentMoveType.SetValueAndForceNotify(value.Value);
		}


		 private void Awake()
		 {
		 }

		 void Start()
		{
			#region Walk
			_inputEvent
				.MoveDirection
				.Where(_ => MoveType.Value == PlayerMoveType.Walk)
				.Do(_ => print(_inputEvent))
				.Select(axis => axis /= 10)
				.Subscribe(axis =>
				{
					var cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1f, 0f, 1f)).normalized;
					_moveVelocity = cameraForward * axis.z + Camera.main.transform.right * axis.x;
					_moveVelocity.y = 0f;
				});

			this.UpdateAsObservable()
				.Subscribe(_ => { transform.Translate(_moveVelocity * _defaultMoveSpeed, Space.World); });

			#endregion

			#region Teleport

			_inputEvent
				.OnTeleportButtonPushed
				.Where(_ => MoveType.Value == PlayerMoveType.Teleport)
				.Subscribe(_ =>{} );

			#endregion

			#region Rotation
			_inputEvent
				.RotDirection
				.Do(_ => print("rot"))
				.Subscribe(axis => transform.Rotate(0f, axis.x * _defaultRotDegree, 0f));
			
			#endregion
		}

	}
}
