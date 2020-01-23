using UnityEngine;
using System.Collections;

public class RFX4_DeactivateByTime : MonoBehaviour {

	public float DeactivateTime = 3;
	public float DeactivateCollTime;
	public GameObject Core;
	private bool canUpdateState;

	void OnEnable () {
		canUpdateState = true;
	}

	private void Update () {
		if (canUpdateState) {
			canUpdateState = false;
			Invoke ("DeactivateColl", DeactivateCollTime);
			Invoke ("DeactivateThis", DeactivateTime);
		}
	}

	void DeactivateColl () {
		Destroy (Core.gameObject);
	}

	void DeactivateThis () {
		Destroy (gameObject);
	}
}
