using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyScript : MonoBehaviour {

	private float time;
	public float aTime;
	public float dTime;
	public GameObject key;

	// Use this for initialization
	void Start () {
		key.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;

		if (time > aTime) key.SetActive(true);

		if (Input.GetKeyDown(KeyCode.D))
		{
			Destroy(this.gameObject, dTime);
		} 
	}
}
