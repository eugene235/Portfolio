#pragma strict

private var HP : int;

function Start () {
}

function Update () {
	transform.localScale.x = HP * 0.004;
}

function sendHPtoHPbar(hp:int){
	HP = hp;
}