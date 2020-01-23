using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour {

	public GameObject tera;
	public GameObject perticle;

	private GameObject ins;
	private GameObject per;
	private bool state;

	// Use this for initialization
	void Start () {
		state = false;
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown(KeyCode.A) && !state)
		{
			ins = Instantiate(tera);
			state = true;
		}
		else if(Input.GetKeyDown(KeyCode.D) && state){
			per = Instantiate(perticle);
			state = false;
		}

	}
}
