using UnityEngine;
using System.Collections;

public class GetDistractor : MonoBehaviour {

	private GUIText txt;

	// Use this for initialization
	void Start () {
		txt = null;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void getDistractor(string str){
		txt = gameObject.GetComponent<GUIText> ();
		txt.text = str;
	}
}
