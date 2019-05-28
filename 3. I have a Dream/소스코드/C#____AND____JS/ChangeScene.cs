using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChangeScene : MonoBehaviour {

	private Button startBtn;

	// Use this for initialization
	void Start () {
		startBtn = GetComponent<Button>();
		startBtn.onClick.AddListener (onclickStart);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void onclickStart(){
		Application.LoadLevel ("TestScene");
	}
}
