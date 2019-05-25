#pragma strict

private var interval : float = 0.5 ;
private var timer : float;
var barrelPrefab : GameObject;

function Start () {
	timer = 1.0;
}

function Update () {
	timer -= Time.deltaTime;
	if(timer<0.0){
		var offsx : float = Random.Range(-6.0,6.0);
		var offsy : float = Random.Range(-4.0,6.0);
		var offsz : float = Random.Range(20.0,25.0);
		var position : Vector3 = transform.position + Vector3(offsx,offsy,offsz);	
		var prefab : GameObject = barrelPrefab;
		Instantiate(prefab,position,Random.rotation);
		
		timer = interval;
	}
}

function GameOver (){
	enabled=false;
}

function afterTrip(nullStr : String){
	enabled = false;
}