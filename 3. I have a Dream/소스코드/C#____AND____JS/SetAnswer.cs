using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SetAnswer : MonoBehaviour {

	[HideInInspector]
	public bool isAnswer = false;

	private Text t;
	private float timer = 3.0f;
	private bool isSet = false;
	private bool grading = false;

	private string txt;

	// Use this for initialization
	void Start () {
		txt = null;
		t = gameObject.transform.GetChild (0).transform.GetComponent<Text>();

	}
	
	// Update is called once per frame
	void Update () {

		if (isSet) {
			timer -= Time.deltaTime;
		}
	
	}

	void IsAnswer (bool b) {
		isAnswer = b;
	}

	void DisplayAnswer(string s){
		t.text = s;
		timer = 3.0f;
		isSet = true;
		grading = false;
	}

	void OnTriggerEnter(Collider other){
		
		if (other.gameObject.tag == "Player") {

			if (timer <= 0.0f ){

				if (isAnswer) {
					txt = "정답입니다";
					t.text = txt;
					grading = true;
				} 

				else {
					txt = "오답입니다";
					t.text = txt;
					other.gameObject.SendMessage ("ApplyDamage", 25.0f);
					grading = true;
				}
					
			}

		}
	}
}