using UnityEngine;
using System.Collections;

public class UserInformation : MonoBehaviour {

	[HideInInspector]
	public string s_id = null;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void GetUserId(string userId) {
		s_id = userId;
		//PlayerPrefs.SetString ("User ID", s_id);
		gameObject.SendMessage ("GetId", s_id);
	}
}
