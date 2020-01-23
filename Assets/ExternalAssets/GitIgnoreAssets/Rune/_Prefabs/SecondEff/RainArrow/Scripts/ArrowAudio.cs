using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowAudio : MonoBehaviour {

	public float m_waitTime;
	public GameObject m_audioBox;

	void Start () {
		StartCoroutine (PlayAudio ());
	}

	IEnumerator PlayAudio () {
		yield return new WaitForSeconds (m_waitTime);
		while (true) {
			yield return new WaitForSeconds (0.1f);
			Instantiate (m_audioBox, transform.position + new Vector3 (Random.Range (-10.0f, 10.0f), transform.position.y - 300.0f, Random.Range (-10.0f, 10.0f)), Quaternion.identity);
			yield return null;
		}
	}
}
