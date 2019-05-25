#pragma strict

private var interval : float = 0.5 ;
private var timer : float;
var honeyPrefab : GameObject;
private var addHeight : float = 0.1; //honey가 생성되는 높이를 점점 키우기 위해

private var flag : boolean;

function Start () {
	timer = 1.0;
	flag = false;
}

function startGenerate(){
	flag=true;
}

function freeze(){
	enabled = false;
}

function Update () {
	if(flag==true){
		timer -= Time.deltaTime;
		if(timer<=0.0){
			addHeight += 0.5;
			var offsx : float = Random.Range(-4.0,4.0);
			var offsy : float = Random.Range(4.0 + addHeight, 7.0 + addHeight);
			var offsz : float = Random.Range(20.0,25.0);
			var position : Vector3 = transform.position + Vector3(offsx,offsy,offsz);	
			var prefab : GameObject = honeyPrefab;
			Instantiate(prefab,position,Random.rotation);
			
			timer = interval;
		}
	}
}

function GameOver (){
	enabled=false;
}