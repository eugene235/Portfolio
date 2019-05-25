using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Upgrade : MonoBehaviour {
	private Button UpgradeBtn;
	private W_Bullet bulletcs;
	public GameObject WBullet;
	private GameObject gameController;
	private GameController gc;
	public static int num = 1;

	// Use this for initialization
	void Start () {
		gameController = GameObject.Find("GameController");
		gc = gameController.GetComponent<GameController>();

		UpgradeBtn = GetComponent<Button>();
		UpgradeBtn.onClick.AddListener (upgradeClicked);
		bulletcs = WBullet.GetComponent<W_Bullet>();
	}

	void upgradeClicked(){
		if (num == 1) {
			bulletcs.W_Bullet_damage = 10.0f;
			num++;
		}
		if (gc.getM () > 300) {
			bulletcs.W_Bullet_damage += 10;
			gc.upgrade (300);
			Debug.Log(bulletcs.W_Bullet_damage);
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}