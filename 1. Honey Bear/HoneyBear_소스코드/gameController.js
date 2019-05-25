#pragma strict

var skin : GUISkin;
private var textStyle : GUIStyle;
private var over : boolean = false;
private var state : String;

private var scoreHP : int;
private var scoreFuel : int;
private var scoreHoney : int;
private var score : int;

private var HoneyGenerate : float = 15.0;	//honey가 생성되는 시간
private var state2 : String;
private var state3 : String;

private var flag : boolean = true;

function Start () {
	textStyle = skin.GetStyle("gameover");
}

function Update () {
	if(state2=="true"){
		HoneyGenerate -= Time.deltaTime;
		if(HoneyGenerate<=0.0&&flag==true){
			TimeUp();
			flag = false;
		}
	}
}

function GameOver() {
	if(!((state=="TimeUp")||(state=="Show Score"))){
		over=true;
		yield WaitForSeconds(5.0);
		Application.LoadLevel("restart");
	}
}

function afterTrip(a : String){
	state2 = a;
	BroadcastMessage("startGenerate");
}

function TimeUp(){
	state = "TimeUp";
	yield WaitForSeconds(3.0);
	BroadcastMessage("Score");
	BroadcastMessage("freeze");
	state = "Show Score";
	while(!(Input.GetButtonDown("Fire1"))){
		yield;
	}Application.LoadLevel("restart");
}

function GetHP(HP : int){
	scoreHP = HP;
}

function GetFuel(Fuel : int){
	scoreFuel = Fuel;
}

function GetHoney(Honey : int){
	scoreHoney = Honey;
	score = (scoreHP*5) + (scoreFuel*7) + (scoreHoney*10);
}

function OnGUI(){

	var sw : int = Screen.width;
	var sh : int = Screen.height;
	
	if(over==true){
		GUI.skin = skin;
		var rect1 : Rect = Rect(0,0,Screen.width,Screen.height);
		GUI.Label(rect1,"Game Over","gameover");
	}
	else if(state=="TimeUp"){
			GUI.skin = skin;
			var rect2 : Rect = Rect(0,0,Screen.width,Screen.height);
			GUI.Label(rect2, "Time Up!","text");
	}
	else if(state=="Show Score"){
			GUI.skin = skin;
			var scoreText : String = "Your Score is " + score.ToString();
			GUI.Label(Rect(0,sh/4,sw,sh/4),scoreText,"text");
			GUI.Label(Rect(0,sh/2,sw,sh/4),"Click to Exit", "text");
	}
}