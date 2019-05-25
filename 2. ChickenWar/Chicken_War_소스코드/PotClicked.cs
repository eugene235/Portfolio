using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PotClicked : MonoBehaviour {

	private Button Pot;

	// Use this for initialization
	void Start () {
		Pot = GetComponent("Button") as Button;
		Pot.onClick.AddListener (makeMoney);
	}

	void makeMoney(){
		GameObject.Find ("GameController").gameObject.SendMessage ("makeMoney");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
