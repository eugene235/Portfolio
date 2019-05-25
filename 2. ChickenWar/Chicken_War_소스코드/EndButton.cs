using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EndButton : MonoBehaviour {

	private Button GameEndButton;

	// Use this for initialization
	void Start () {

		GameEndButton = GetComponent("Button") as Button;
		GameEndButton.onClick.AddListener (EndButtonLis);
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	void EndButtonLis(){
		Debug.Log ("ye!!!!!!");
		Application.LoadLevel ("Start");
	}
}
