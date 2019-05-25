using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//체력회복 Script
public class bBtn : MonoBehaviour {
	private GameObject[] GO;
	private Building[] building;
	private Button myButton;
	private bool flag;
	private GameObject gameController;
	private GameController gc;
	
	// Use this for initialization
	void Start () {
		flag = false;
		myButton = GetComponent<Button>();
		myButton.onClick.AddListener (treat);
		gameController = GameObject.Find("GameController");
		gc = gameController.GetComponent<GameController>();
	}

	void treat(){
		flag = true;
	}
	
	// Update is called once per frame
	void Update () {
		GO = GameObject.FindGameObjectsWithTag("building");
		building = new Building[GO.Length];
		for(int i = 0; i<GO.Length; i++){
			building[i] = GO[i].GetComponent<Building>();
		}
		if ((flag == true)&&(gc.getM()>=300)) {
			for(int j = 0; j<GO.Length; j++){
				building[j].recoveryHP();
			}
			gc.minusM(300);
			flag = false;
		}
	}
}