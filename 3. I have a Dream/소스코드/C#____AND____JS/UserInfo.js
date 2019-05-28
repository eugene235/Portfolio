#pragma strict

function Start () {

}

function Update () {

}

function GetUserId(userId : String){

	gameObject.SendMessage("GetId", userId);

}