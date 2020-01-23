using System.Collections;
using System.Collections.Generic;
using NoonNight.Battle.Skill;
using NoonNight.Utilities;
using UniRx;
using UnityEngine;

namespace NoonNight.Battle.Players
{
		public class PlayerCore : Singleton<PlayerCore>, ISkillEffectAffectable
	{
		[SerializeField] private Parameters _baseParams;
		
		private BoolReactiveProperty _isAlive;
		private ReactiveProperty<Parameters> _currentParams; 
		
		public BoolReactiveProperty IsAlive => _isAlive;
		public ReactiveProperty<Parameters> CurrentParams => _currentParams;

		public void Initialize()
		{
			_isAlive = new BoolReactiveProperty(true);
			_currentParams = new ReactiveProperty<Parameters>(_baseParams);
		}
		
		void Start()
		{
			Initialize();
			
			_currentParams.Where(p => p._hp <= 0)
				.Subscribe(_ => _isAlive.SetValueAndForceNotify(false));

			_isAlive.Where(x => !x)
				.Subscribe(_ => print("しんでしまった..."));

		}

		public void Affect(SkillEntity skill)
		{
			throw new System.NotImplementedException();
		}
	}
}
