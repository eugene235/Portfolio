#pragma strict

function OnTriggerEnter(other:Collider){
	other.gameObject.SendMessage("CatchBarrel",10); //연료를 먹으면 에너지 10 증가
	Destroy(gameObject);
}

function Start () {
	Destroy(gameObject, 5.0f);
}

function Update () {

}