using UnityEngine;
using System.Collections;

public class W_Bullet : MonoBehaviour {
	private float W_Bullet_Speed = 600.0f;
	private float W_Bullet_LifeTime = 3.0f;
	public float W_Bullet_damage = 10.0f;

	// Use this for initialization
	void Start () {
		Destroy (gameObject, W_Bullet_LifeTime);
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += transform.forward * W_Bullet_Speed * Time.deltaTime;
	}

	void OnTriggerEnter(Collider other){
		if (other.CompareTag ("Enemy")) {
			IDamageable damageTarget = (IDamageable)other.GetComponent(typeof(IDamageable));
			damageTarget.Damage(W_Bullet_damage);
			Destroy(gameObject);
		}
	}
}