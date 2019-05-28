#pragma strict

@HideInInspector
var coinscore : int;
var skin : GUISkin;

function Start () {

}

function Update () {

}

function getCoin ()
{
	Destroy(gameObject);
}


function OnGUI() {

	GUI.skin = skin;
	var sw : int = Screen.width;
	var sh : int = Screen.height;
	var coinText : String = "Coin: " + coinscore.ToString();
	GUI.Label(Rect(0,0,sw/2,sh/4), coinText, "coin");

}