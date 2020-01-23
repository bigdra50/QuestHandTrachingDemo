using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using UnityEngine.UI;

public class SearchingEnemy : MonoBehaviour {

	private SpriteRenderer m_spriteRenderer;
	private float m_baseDistance = 8.8f;
	public GameObject m_foundArc;
	public GameObject m_damyEff;

	void Start () {
		m_spriteRenderer = GetComponent<SpriteRenderer> ();

		this.UpdateAsObservable ()
			.SkipWhile (_ => m_spriteRenderer.color.a == 1.0f)
			.First ()
			.Subscribe (_ => {
			GameObject[] m_allEnemy = GameObject.FindGameObjectsWithTag ("Enemy");
			List<Transform> m_selectedEnemyTransList = new List<Transform> ();
			foreach (var g in m_allEnemy) {
				if (Vector3.Distance (g.transform.position, transform.position) < m_baseDistance) {
					m_selectedEnemyTransList.Add (g.transform);
				}
			}
			if (m_selectedEnemyTransList.Count != 0) {
				GameObject damy = Instantiate (m_damyEff, transform.position + new Vector3 (0.0f, 150.0f, 0.0f), Quaternion.Euler (90.0f, 0.0f, 0.0f)) as GameObject;
				damy.transform.parent = transform.root;
			}
			foreach (var t in m_selectedEnemyTransList) {
				GameObject obj = Instantiate (m_foundArc, t.position, Quaternion.Euler (-90.0f, 0.0f, 0.0f)) as GameObject;
				t.position = t.position + t.up * 0.015f;
				obj.GetComponent<Chasing> ().m_chaseTrans = t;
			}
		});
	}

}
