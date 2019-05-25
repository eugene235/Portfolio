#pragma strict

function OnTriggerEnter(other:Collider){
	other.gameObject.SendMessage("CatchHoney",1);
	Destroy(gameObject);
}

function Start () {
	animation.Play("Take 001");
	Destroy(gameObject, 6.0f);
}

function freeze(){
	enabled = false;
}

function Update () {

}