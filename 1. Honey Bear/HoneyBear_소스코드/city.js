#pragma strict

function OnTriggerEnter (other : Collider){
		other.gameObject.BroadcastMessage("ApplyDamage",10);
}

function Start () {

}

function Update () {

}