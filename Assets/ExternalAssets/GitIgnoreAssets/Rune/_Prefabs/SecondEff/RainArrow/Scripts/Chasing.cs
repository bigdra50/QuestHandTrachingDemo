using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chasing : MonoBehaviour {

	public Transform m_chaseTrans;

	void Update () {
		m_chaseTrans.position = new Vector3 (m_chaseTrans.position.x, 0.015f, m_chaseTrans.position.z);
		transform.position = m_chaseTrans.position;
	}
}
