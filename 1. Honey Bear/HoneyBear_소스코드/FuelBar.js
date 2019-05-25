#pragma strict

private var fuel : int;

function Start () {
}

function Update () {
	if(fuel<=100){
		transform.localScale.x = fuel * 0.004;
	}else
		transform.localScale.x = 0.4;
}

function sendFueltoFuelbar(Fuel:int){
	fuel = Fuel;
}