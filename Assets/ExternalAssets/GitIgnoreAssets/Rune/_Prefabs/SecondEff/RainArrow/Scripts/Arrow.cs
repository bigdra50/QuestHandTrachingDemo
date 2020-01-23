using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

	public float m_speed;
	public GameObject m_audioBox;
	private bool m_canMove = true;

	void Update () {
		if (m_canMove)
			transform.position += transform.forward * Time.deltaTime * m_speed;
	}

	void OnTriggerEnter (Collider other) {
		if (other.CompareTag ("Floor")) {
			Instantiate (m_audioBox, transform.position, transform.rotation);
			GetComponent<Collider> ().enabled = false;
			m_canMove = false;
		}
	}
}
