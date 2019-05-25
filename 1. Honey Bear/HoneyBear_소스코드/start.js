#pragma strict

var skin : GUISkin;
private var textStyle : GUIStyle;

function Start () {
	textStyle = skin.GetStyle("text");
}

function Update () {
	if(Input.GetButtonDown("Fire1")){
		Application.LoadLevel("main");
	}
}

function OnGUI(){
	GUI.skin = skin;
	var rect1 : Rect = Rect(0,0,Screen.width,Screen.height);
	GUI.Label(rect1, "Click to Start","text");
}