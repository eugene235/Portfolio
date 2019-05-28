using UnityEngine;
using System.Collections;

public class CharacterState : MonoBehaviour {

	private float HP;
	private float M_HP;
	private int userCoin;
	public GUISkin skin;

	private float Health;
	public GameObject HealthBar;

	private int potion;
	private int shield;
	private string userCharacter;

	private string userId;
	private GameObject[] Player;

	private bool pb = true;
	private bool sb = true;

	private int deadScore = 1;

	[HideInInspector]
	public bool show = true;
	
	// Use this for initialization
	void Start () {
		HP = 100.0f;
		M_HP = 100.0f;
		userCoin = 0;
		if(show==false){
			gameObject.SetActive(false);
		}
			
		userId = PlayerPrefs.GetString ("User ID");
		potion = PlayerPrefs.GetInt ("potion");
		shield = PlayerPrefs.GetInt ("shield");
		userCharacter = PlayerPrefs.GetString ("char_name");

		Debug.Log (userId);
		PlayerPrefs.SetString ("UserID", userId);



		Player = GameObject.FindGameObjectsWithTag ("Player");
		foreach(GameObject player in Player){
			//Debug.Log(player);
			CharacterState c = player.GetComponent<CharacterState> ();

			if (player.gameObject.name.Equals (userCharacter)) {
				c.show = true;
			} else {
				c.show = false;
			}

		}
	}
	
	// Update is called once per frame
	void Update () {
		if(HP<=0.0f){
			Destroy(gameObject);
			deadScore = 0;
			PlayerPrefs.SetInt ("Dead State",deadScore);
			GameObject.Find ("Timer").SendMessage ("TheEnd");
			Application.LoadLevel ("End");

		} else if(HP<=20.0f){
			UsePotion();
		}
		PlayerPrefs.SetInt ("Player Coin", userCoin);
	}

	void UsePotion(){
		if(potion>=1&&pb==true){
			HP += 20.0f;
			potion -= 1;
			pb = false;
			PlayerPrefs.SetInt ("Potion", potion);
		}
	}

	void ApplyDamage(float damage){
		if ( (shield >= 1) && sb==true ) {
			shield -= 1;
			Debug.Log (shield);
			sb = false;
			PlayerPrefs.SetInt ("Shield", shield);
			return;
		}
		HP -= damage;
		Health = HP / M_HP;
		HealthBar.transform.localScale = new Vector3 (Health,HealthBar.transform.localScale.y,HealthBar.transform.localScale.z);
		//if character have shields, character don't apply damage and num of shield --;
	}

	void getCoin(int coin){
		userCoin += coin;
		Debug.Log (userCoin);
	}

	void OnGUI() {
		GUI.skin = skin;
		//string coinText = userId + " : " + userCoin.ToString ();
		string coinText = "Coin: " + userCoin.ToString ();
		GUI.Label (new Rect (0, 0, 100, 100), coinText, "coin");
	}
}