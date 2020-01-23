using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeDown : MonoBehaviour {

	public float m_startTime;
	public float m_speed;
	public Transform m_blackEff;

	void Start () {
		StartCoroutine (SizeChange ());		
	}

	IEnumerator SizeChange () {
		yield return new WaitForSeconds (m_startTime);
		while (transform.localScale.x > 0) {
			transform.localScale -= Vector3.one * m_speed * Time.deltaTime;
			yield return null;
		}
		m_blackEff.parent = null;
		Destroy (gameObject);
	}

}
