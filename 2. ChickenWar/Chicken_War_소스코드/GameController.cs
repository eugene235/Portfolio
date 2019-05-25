using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	private int money;
	public Texture2D textureToDisplay;

	private float timer;
	private float interval;

	private Bullet bullet;
	public GameObject bulletobj;
	public static int num1 = 1;

	private float time;
	GUIStyle largeFont;
	private bool flag;

	// Use this for initialization
	void Start () {
		flag = false;
		money = 0;
		timer = 30.0f;
		interval = 30.0f;
		bullet = bulletobj.GetComponent<Bullet>();
		money = PlayerPrefs.GetInt("Player money");
		Debug.Log(PlayerPrefs.GetInt("Player money"));
		largeFont = new GUIStyle();
		largeFont.fontSize = 30;
		largeFont.normal.textColor = Color.white;
	}

	// Update is called once per frame
	void Update () {
		if (num1 == 1) {
			bullet.damage = 5.0f;
			num1++;
			money = 0;
		}
		timer -= Time.deltaTime;
		if (timer <= 0.0f) {
			bullet.damage+=5.0f;
			timer = interval;
			Debug.Log(bullet.damage);
		}

		PlayerPrefs.SetInt("Player money", money);
		time += Time.deltaTime;
	}

	void makeMoney(){
		money += 2;
	}

	void OnGUI() {
		GUI.Label(new Rect(20, 20, textureToDisplay.width, textureToDisplay.height), textureToDisplay);
		GUI.color = Color.black;
		GUI.Label(new Rect(85, 25, 110, 30), money.ToString());
		
		if (flag) {
			GUI.Label (new Rect (Screen.width/3, Screen.height/3, 100, 100), "Game Over", largeFont);
		}
	}

	public int getM(){
		return money;
	}

	public void minusM(int price){
		money -= price;
	}

	public void getCoin(int price){
		money += price;
	}

	public void upgrade(int price){
		money -= price;
	}

	public IEnumerator GameEnd(){
		flag = true;
		GameObject.Find("Canvas2").SendMessage("finish");
		yield return new WaitForSeconds (5.0f);
		Application.LoadLevel ("End");
	}
}
