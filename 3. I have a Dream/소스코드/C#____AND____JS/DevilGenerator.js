#pragma strict

public var prefab : GameObject;
private var pos : Vector3;
private var randomX : float;
private var timer : float;
private var interval : float;


function Start () {
	pos = transform.position;
	interval = 2.0f;
	timer = 0.0f;
}

function Update () {
	randomX = Random.Range(-5.0f,5.0f);
	pos.x = randomX;
	timer -= Time.deltaTime;
	
	if(timer<0.0f){
		Instantiate(prefab, pos, Quaternion.Euler(90,-180,0));
		timer = interval;
	}
}