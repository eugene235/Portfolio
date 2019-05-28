#pragma strict

var skin : GUISkin;
var countup:boolean = true; 

var hours:float = 0f;
var minutes:float = 1f;
var seconds:float = 30f;

/// CALCULATION VARIABLES
private var pauseTimer:boolean = false;

private var timer:float = 0f;
private var hrs:float = 0f;
private var min:float = 0f;
private var sec:float = 0f;

private var strHrs:String = "00";
private var strMin:String = "00";
private var strSec:String = "00";


// Use this for initialization
function Start () {	
	if(countup) {
		Debug.Log("Count Up Timer initiated");
		sec = 0f;
		min = 0f;
		hrs = 0f;
	} 
}

// Update is called once per frame
function Update () {
	if(Input.GetKeyUp("space")) {
		if(pauseTimer) {
			pauseTimer = false;
		} else {
			pauseTimer = true;
		}
	}
	
	if(pauseTimer) {
		Time.timeScale = 0;
	} else {
		Time.timeScale = 1;
	}//end if
	
	if(seconds > 59) {
		Debug.Log("Seconds must be less than 59!");
		return;
	} else if (minutes > 59) {
		Debug.Log( "Minutes must be less than 59!");
	} else {
		FindTimer();
	}
	PlayerPrefs.SetString("Player Hours", strHrs);
	PlayerPrefs.SetString("Player Minutes", strMin);
	PlayerPrefs.SetString("Player Seconds", strSec);
}

//TIMER FUNCTIONS
//Checks which Timer has been initiated
function FindTimer() {
	if(countup) {
		CountUp();
	}
}

//Timer starts at 00:00:00 and counts up until reaches Time limit
function CountUp() {
	timer += Time.deltaTime;
	
	if(timer >= 1f) {
		sec++;
		timer = 0f;
	}
	
	if(sec >= 60) {
		min++;
		sec = 0f;
	}
	
	if(min >= 60) {
		hrs++;
		min = 0f;
	}
}


function FormatTimer () {
	if(sec < 10) {
		strSec = "0" + sec.ToString();
	} else {
		strSec = sec.ToString();
	}
	
	if(min < 10) {
		strMin = "0" + min.ToString();
	} else {
		strMin = min.ToString();
	}
	
	if(hrs < 10) {
		strHrs = "0" + hrs.ToString();
	} else {
		strHrs = hrs.ToString();
	}	
}

function TheEnd(){
	gameObject.SetActive(false);
}

//DISPLAY TIMER
function OnGUI () {
    
    GUI.skin = skin;
    var sw : int = Screen.width;
    var sh : int = Screen.height;
	FormatTimer();
	if(countup) {		
		GUI.Label(new Rect(0,0,sw/1,sh/8), 
		strHrs + ":" + strMin + ":" + strSec , "Time");
	} 
}

//GETTERS & SETTERS 
public function GetPaused() { return pauseTimer; }
public function SetPaused(val:boolean) { pauseTimer = val; }

public function GetSec() { return sec; }
public function GetMin() { return min; }
public function GetHrs() { return hrs; }