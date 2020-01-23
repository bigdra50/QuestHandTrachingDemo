using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour {

	public float m_speed;

	void Update () {
		transform.position += transform.forward * m_speed * Time.deltaTime;
	}
}
