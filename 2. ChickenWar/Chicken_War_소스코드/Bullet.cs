using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	private float Speed = 600.0f;
	private float LifeTime = 3.0f;
	public float damage = 5.0f;

	// Use this for initialization
	void Start () {
		Debug.Log (damage);
		Destroy (gameObject, LifeTime);
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += transform.forward * Speed * Time.deltaTime;
	}
	
	void OnTriggerEnter(Collider other){
		if (other.CompareTag ("building")) {
			IDamageable damageTarget = (IDamageable)other.GetComponent(typeof(IDamageable));
			damageTarget.Damage(damage);
			Destroy(gameObject);
		}
	}
}
