using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NoonNight.Battle.RuneCanvas
{
	public class HaloWave : MonoBehaviour
	{

		private Light m_light;
		[SerializeField] private float m_intensity = 0.8f;
		[SerializeField] private float m_maxAdditive = 0.6f;

		void Start()
		{
			m_light = GetComponent<Light>();
		}

		void Update()
		{
			m_light.intensity = m_intensity + m_maxAdditive * Mathf.Abs(Mathf.Sin(Time.time));
		}
	}
}
