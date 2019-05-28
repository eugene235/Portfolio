using UnityEngine;
using System.Collections;

public class EndingScene : MonoBehaviour {
	private int userCoin;
	private string mins, secs;
	private int score = 0;

	private string url = "http://192.168.100.246:8080/GameDB/ResetGame.jsp";
	public WWW www;
	private string userId;

	private int potion, shield;
	public GUISkin skin;

	// Use this for initialization
	void Start () {
		userCoin = PlayerPrefs.GetInt ("Player Coin");
		Debug.Log (userCoin);

		mins = PlayerPrefs.GetString("Player Minutes");
		secs = PlayerPrefs.GetString("Player Seconds");

		score = PlayerPrefs.GetInt ("Dead State");
		Debug.Log (score);
		if(score != 0){
			score = (int.Parse (mins) * 60) + int.Parse (secs);
		}
		//Debug.Log (hrs+mins+secs);
		userId = PlayerPrefs.GetString("UserID");
		potion = PlayerPrefs.GetInt ("Potion");
		shield = PlayerPrefs.GetInt ("Shield");

		Debug.Log (score);
		Debug.Log (userId);
		Debug.Log (potion);
		Debug.Log (shield);

		StartCoroutine (updateUser ());
	}

	void OnGUI(){ 
		GUI.skin = skin;
		int sw = Screen.width;
		int sh = Screen.height;
		GUI.Label (new Rect(sw/2-100,sh/2-100,180,100), "Game Over","End");
		if (score != 0) {
			GUI.Label (new Rect (sw / 2 - 100, sh / 2 - 50, 180, 100), "Your Score is " + mins + ":" + secs, "End");
		} else {
			GUI.Label (new Rect (sw / 2 - 100, sh / 2 - 50, 180, 100), "Your Score is " + score, "End");
		}
		GUI.Label (new Rect (sw / 2-100, sh / 2, 180, 100), "Coin " + userCoin,"End");
	}
	
	// Update is called once per frame
	void Update () {
	}
	/*
	IEnumerator resetGame(){
		www = new WWW(url);
		yield return www;
	}
	*/

	IEnumerator updateUser(){
		WWWForm form = new WWWForm();
		form.AddField ("userId", userId);
		form.AddField ("potion", potion);
		form.AddField ("shield", shield);
		form.AddField ("coin", userCoin);
		form.AddField ("score", score);

		WWW w = new WWW(url, form);
		yield return w;
		if (w.error != null){
			print(w.error);
		} else{
			Debug.Log ("성공");
		}
	}
}