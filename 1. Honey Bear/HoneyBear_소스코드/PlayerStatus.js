#pragma strict

private var HP : int = 100;
private var fuel : int = 100;
private var effectFlag : boolean;
private var effectFlag2 : boolean;
private var originalFuelColor : Color;
private var originalFuelColor2 : Color;
private var fuelStyle : GUIStyle;
private var fuelStyle2 : GUIStyle;
private var honey : int = 0;
private var time : float = 60;

var tempFuel : int;
var SmokeParticlePrefab : GameObject;
var HoneyParticlePrefab : GameObject;
var skin : GUISkin;
var barrelSE : AudioClip;
var damageSE : AudioClip;

function CatchBarrel(amount : int){
	effectFlag2 = true;
	audio.PlayOneShot(barrelSE);
	yield WaitForSeconds(0.3);
	effectFlag2 = false;
	Instantiate(SmokeParticlePrefab, transform.position, transform.rotation);
	fuelStyle2.normal.textColor = originalFuelColor2;
	fuel += amount;
}

function CatchHoney(amount:int){
	audio.PlayOneShot(barrelSE);
	yield WaitForSeconds(0.1);
	Instantiate(SmokeParticlePrefab, transform.position, transform.rotation);
	honey+=amount;
}

function Jump(amount:int){
	fuel-=amount;	
}

function ApplyDamage(amount : int){
	effectFlag = true;
	audio.PlayOneShot(damageSE);
	yield WaitForSeconds(0.3);
	effectFlag = false;
	fuelStyle.normal.textColor = originalFuelColor;
	HP -= amount;
	if(HP<=0){
		BroadcastMessage("GameOver");
		SendMessageUpwards("GameOver");
	}
}

function GameOver(){
	enabled=false;
}

function OnGUI(){
	GUI.skin = skin;
	var rect : Rect = Rect(0,0,Screen.width,Screen.height);
	GUI.Label(rect, "HP: " + HP.ToString(),"HP");
	GUI.Label(rect, "FUEL : " + fuel.ToString(),"Fuel");
	GUI.Label(rect, "HONEY :" + honey.ToString(),"Honey");
	var Timer : String = "Left Time : " + time.ToString("0");
	if(time>=0){
		GUI.Label(rect,Timer,"time");
	}
}

function Start () {
	fuelStyle = skin.GetStyle("HP");
	fuelStyle2 = skin.GetStyle("FUEL");
	originalFuelColor = fuelStyle.normal.textColor;
	originalFuelColor2 = fuelStyle2.normal.textColor;
	effectFlag = false;
}

function freeze(){
	enabled = false;
}

function Update () {
	if(effectFlag)
		fuelStyle.normal.textColor = Color.red*Mathf.Abs(Mathf.Sin(40.0*Time.time));
	if(effectFlag2)
		fuelStyle2.normal.textColor = Color.yellow*Mathf.Abs(Mathf.Sin(40.0*Time.time));
	
	BroadcastMessage("sendHPtoHPbar",HP);
	BroadcastMessage("sendFueltoFuelbar",fuel);
	
	tempFuel = fuel;
	time-=Time.deltaTime;
}

function Score(){
	SendMessageUpwards("GetHP",HP);
	SendMessageUpwards("GetFuel",fuel);
	SendMessageUpwards("GetHoney",honey);
}