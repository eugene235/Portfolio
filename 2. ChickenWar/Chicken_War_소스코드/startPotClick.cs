using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class startPotClick : MonoBehaviour {
	
	private Button Pot;
	private float timer;
	private string t;
	GUIStyle largeFont;
	
	// Use this for initialization
	void Start () {
		Pot = GetComponent("Button") as Button;
		Pot.onClick.AddListener (makeMoney);
		timer = 10.0f;
		largeFont = new GUIStyle();
		largeFont.fontSize = 15;
		largeFont.normal.textColor = Color.cyan;
	}

	void OnGUI(){
		GUI.color = Color.white;
		GUI.Label (new Rect (20, 55, 100, 20), "항아리를 클릭하세요", largeFont);
		GUI.Label (new Rect (170, 55, 100, 100), t, largeFont);
	}
	
	void makeMoney(){
		GameObject.Find ("GameController").gameObject.SendMessage ("makeMoney");
	}
	
	// Update is called once per frame
	void Update () {
		t = (timer % 60).ToString("f0");
		timer -= Time.deltaTime;
		if (timer <= 0.0f) {
			timer = 0;
			Pot.onClick.RemoveListener(makeMoney);
		}
	}
}
