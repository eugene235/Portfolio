using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Timer : MonoBehaviour {
	public Text timerText;
	private float startTime;
	private bool finished;
	GUIStyle fontStyle;
	private bool flag;
	
	// Use this for initialization
	void Start () {
		flag = false;
		startTime = Time.time;
		fontStyle = new GUIStyle();
		fontStyle.fontSize = 25;
		fontStyle.normal.textColor = Color.white;
	}
	
	// Update is called once per frame
	void Update () {
		if (finished) {
			flag = true;
			return;
		}
		float t = Time.time - startTime;
		string minutes = ((int)t / 60).ToString ();
		string seconds = (t % 60).ToString ("f0");
		timerText.text = minutes + ":" + seconds;
	}
	public void finish(){
		finished = true;
		timerText.color = Color.red;
	}

	void OnGUI(){
		if (flag) {
			GUI.Label (new Rect (Screen.width / 2 - 30, Screen.height / 3 + 30, 100, 100), timerText.text, fontStyle);
		}
	}
}
