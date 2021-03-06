﻿using UnityEngine;
using System.Collections;

public class RoadMovement2 : MonoBehaviour {
	
	public float speed = 4.0f;
	Vector3 position;
	
	// Use this for initialization
	void Start () {
		
		position = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		position.z -= Time.deltaTime * speed;    
		
		if (position.z < 17.0f)
			position.z += 7.0f;
		
		transform.position = position;
	}

	IEnumerator Booster(){
		speed = 20.0f;
		yield return new WaitForSeconds(1.0f);
		speed = 4.0f;
	}
}