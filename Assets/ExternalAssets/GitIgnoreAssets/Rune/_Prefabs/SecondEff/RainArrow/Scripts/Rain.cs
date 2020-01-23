using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class Rain : MonoBehaviour {

	public GameObject m_arrowObject;
	public float m_waitTime;
	private float m_timer = 0.0f;

	void Start () {
		Observable.Timer (System.TimeSpan.FromSeconds (1.3f))
			.Subscribe (a => {
			this.UpdateAsObservable ()
			.Subscribe (_ => {
				m_timer += Time.deltaTime;
				if (m_timer > 0.05f) {
					m_timer = 0;
					Instantiate (
						m_arrowObject,
						transform.position + new Vector3 (Random.Range (-1.5f, 1.5f), 0.0f, Random.Range (-1.5f, 1.5f)),
						Quaternion.Euler (90.0f, Random.Range (0.0f, 90.0f), Random.Range (-180.0f, 180.0f))
					);
					Instantiate (
						m_arrowObject,
						transform.position + new Vector3 (Random.Range (-1.0f, 1.0f), 0.0f, Random.Range (-1.0f, 1.0f)),
						Quaternion.Euler (90.0f + Random.Range (-2.0f, 2.0f), Random.Range (0.0f, 90.0f), Random.Range (-180.0f, 180.0f))
					);
				}
			});
		});
	}
}
