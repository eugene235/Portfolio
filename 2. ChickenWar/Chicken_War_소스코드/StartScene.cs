using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StartScene : MonoBehaviour {

	//public Texture2D StartTexture;
	private Button StartButton;

	// Use this for initialization
	void Start () {
		StartButton= GetComponent("Button") as Button;
		StartButton.onClick.AddListener (StartButtonLis);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI() {
		//GUI.Label(new Rect(Screen.width/4+50, Screen.height/6, StartTexture.width, StartTexture.height), StartTexture);
		//GUI.Label(new Rect(90, 25, 110, 30), money.ToString());
	}

	void StartButtonLis (){
		Application.LoadLevel ("main");
	}

}
