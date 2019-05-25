using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Building : MonoBehaviour, IDamageable {
	
	private float M_HP;
	private float D_HP;
	private float Health;
	public GameObject HealthBar;

	// Use this for initialization
	void Start () {
		M_HP = 300;
		D_HP = M_HP;
		recoveryHP();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Damage(float damageTaken){
		D_HP -= damageTaken;
		if (D_HP <= 0) {
			D_HP =0;
			Destroy(gameObject);
		}
		Health = D_HP / M_HP;
		HealthBar.transform.localScale = new Vector3 (Health,HealthBar.transform.localScale.y,HealthBar.transform.localScale.z);
	}
	public void recoveryHP(){
		D_HP= M_HP;
	}
}