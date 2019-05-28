using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StartButton : MonoBehaviour {

//	private static int num = 1;

	private Button startBtn;
	private string userId = null;
	private string URL;
	public WWW www;
	private string text;
	private string[] infoTable;

	private int potion, shield;
	private string character, car;

	private string char_name;

	// Use this for initialization
	void Start () {
		startBtn = GetComponent<Button>();
		startBtn.onClick.AddListener (onclickStart);

		userId = null;
		URL = "http://192.168.100.246:8080/GameDB/GetItemInfo.jsp?userId=";
		StartCoroutine(getInfo());
		//userId = PlayerPrefs.GetString ("User ID");
		//URL += userId;

		//StartCoroutine(getInfo());
	}

	void onclickStart(){
		Debug.Log (userId);
		PlayerPrefs.SetString ("User ID", userId);
		PlayerPrefs.SetString ("char_name", char_name);
		PlayerPrefs.SetInt ("potion", potion);
		PlayerPrefs.SetInt ("shield", shield);
		Application.LoadLevel ("Main");
	}
	
	// Update is called once per frame
	void Update () {
	}


	void GetId(string id){
		userId = id;
		URL += userId;
		Debug.Log ("URL == " + URL);
		StartCoroutine(getInfo());
	}


	IEnumerator getInfo(){

		Debug.Log ("=====GETINFO()======");
		www = new WWW(URL);
		yield return www;
		text = StripHtml (www.text);
		infoTable = text.Split( new string[]{ System.Environment.NewLine }, System.StringSplitOptions.RemoveEmptyEntries);

		potion = int.Parse (infoTable [0].Trim ());
		shield = int.Parse (infoTable [1].Trim ());
		character = infoTable [2].Trim ();
		car = infoTable [3].Trim ();
		SetCharacter ();
	}

	void SetCharacter(){
		if (character.Equals ("ch1") && car.Equals ("airballoon")) {
			char_name = "C1Airballoon";
		} else if (character.Equals ("ch1") && car.Equals ("rocket")) {
			char_name = "C1Rocket";
		} else if (character.Equals ("ch1") && car.Equals ("ufo")) {
			char_name = "C1Spaceship";
		}

		else if (character.Equals ("ch2") && car.Equals ("airballoon")) {
			char_name = "C2Airballoon";
		} else if (character.Equals ("ch2") && car.Equals ("rocket")) {
			char_name = "C2Rocket";
		} else if (character.Equals ("ch2") && car.Equals ("ufo")) {
			char_name = "C2Spaceship";
		}

		else if (character.Equals ("ch3") && car.Equals ("airballoon")) {
			char_name = "C3Airballoon";
		} else if (character.Equals ("ch3") && car.Equals ("rocket")) {
			char_name = "C3Rocket";
		} else if (character.Equals ("ch3") && car.Equals ("ufo")) {
			char_name = "C3Spaceship";
		}
	}

	public static string StripHtml(string Html)
	{
		string output;
		output = System.Text.RegularExpressions.Regex.Replace(Html, "<[^>]*>", string.Empty);
		output = System.Text.RegularExpressions.Regex.Replace(output, @"^\s*$\n", string.Empty, System.Text.RegularExpressions.RegexOptions.Multiline);
		return output;
	}
}