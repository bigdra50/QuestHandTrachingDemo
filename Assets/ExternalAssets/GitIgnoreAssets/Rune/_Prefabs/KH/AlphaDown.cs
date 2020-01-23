using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphaDown : MonoBehaviour {

	public float startTime;
	public float speed;

	private float time;
	private float alpha;
	private bool state;

	// Use this for initialization
	void Start () {
		alpha = 0.5f;
		state = true;
	}
	
	// Update is called once per frame
	void Update () {

		time += Time.deltaTime;

		if(time >= startTime/4 && state)
		{
			alpha -= Time.deltaTime * speed * 4;
			this.GetComponent<Renderer>().material.SetFloat("_Alpha", alpha);
			if(alpha <= 0)
			{
				state = false;
			}
		}

	}
}
