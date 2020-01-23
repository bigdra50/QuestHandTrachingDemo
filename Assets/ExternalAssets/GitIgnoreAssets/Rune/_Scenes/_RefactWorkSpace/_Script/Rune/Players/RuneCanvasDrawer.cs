using System.Collections;
using System.Collections.Generic;
using NoonNight.Battle.Inputs;
using NoonNight.Battle.RuneCanvas;
using UniRx;
using UnityEngine;
using Zenject;

namespace NoonNight.Battle.Players
{
	/// <summary>
	/// RuneCanvasの呼び出し
	/// </summary>
	public class RuneCanvasDrawer : MonoBehaviour
	{
		[Inject] private NoonNight.Battle.Inputs.IInputEventProvider _inputEvent;
		[SerializeField] private GameObject _runeCanvas;

		void Start()
		{
			_inputEvent
				.OnRuneButtonPushed
				.SkipLatestValueOnSubscribe()
				.Subscribe(x =>
				{
					_runeCanvas.SetActive(x);
				});

		}

	}
}
