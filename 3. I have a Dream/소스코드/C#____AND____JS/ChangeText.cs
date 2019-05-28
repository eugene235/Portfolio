using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChangeText : MonoBehaviour {

	private string s_id = null;
	private Text txt;

	// Use this for initialization
	void Start () {
		s_id = PlayerPrefs.GetString ("s_id");
		txt = GameObject.Find ("Text").GetComponent<Text> ();
		txt.text = s_id;

		Debug.Log (s_id);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
