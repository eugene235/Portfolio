using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DevilMovement : MonoBehaviour {
	
	public float speed = 4.0f;
	private  Button btn;
	Vector3 position;
	private float timer;
	private float interval;
	private Button Btn;
	
	// Use this for initialiation
	void Start () {
		position = transform.position;
		timer = 1.0f;
		interval = 1.0f;

		Btn = GameObject.Find("BoosterBtn").GetComponent<Button>();
		Btn.onClick.AddListener (Booster);
	}
	
	// Update is called once per frame
	void Update () {
		position.z -= Time.deltaTime * speed;    
		
		if (position.z < -2.6f) {
			Destroy (gameObject);
		}

		timer -= Time.deltaTime;

		if (timer <= 0.0f)
			timer = interval;

		transform.position = position;
	}

	/*void OnTriggerEnter(Collider other) {
		Destroy(gameObject);
		other.SendMessage("ApplyDamage", 5);
	}*/

	void OnCollisionEnter(Collision other) {
		Destroy(gameObject);
		other.gameObject.SendMessage("ApplyDamage", 10.0f);
	}

	void Booster(){
		speed = 20.0f;

		if(timer<=0.0){
			speed = 4.0f;
		}
	}
}