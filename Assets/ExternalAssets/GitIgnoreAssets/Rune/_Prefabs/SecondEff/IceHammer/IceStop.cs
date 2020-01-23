using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IceStop : MonoBehaviour {

	private float m_baseMoveSpeed;

	void OnTriggerStay (Collider other) {
		if (other.CompareTag ("Enemy")) {
			if (other.GetComponent<NavMeshAgent> ().enabled) {
				m_baseMoveSpeed = other.GetComponent<NavMeshAgent> ().speed;
				other.GetComponent<NavMeshAgent> ().speed = 0.0f;
			}
		}
	}

	void OnTriggerExit (Collider other) {
		if (other.CompareTag ("Enemy")) {
			if (other.GetComponent<NavMeshAgent> ().enabled) {
				other.GetComponent<NavMeshAgent> ().speed = m_baseMoveSpeed;
			}
		}
	}

}
