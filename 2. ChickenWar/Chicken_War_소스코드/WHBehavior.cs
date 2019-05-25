using UnityEngine;
using System.Collections;

//흰색 닭의 행동 규정
public class WHBehavior : MonoBehaviour {

	private NavMeshAgent agent;

	private Transform turret{ get; set; }
	private Transform bulletSpawnPoint{ get; set; }
	public GameObject Bullet;
	
	private GameObject [] enemies;
	private Transform [] enemiesT;
	private Transform closestBH;

	private float timer;
	private float interval;
	
	// Use this for initialization
	void Start () {
		agent = GetComponent ("NavMeshAgent") as NavMeshAgent;

		interval = 0.5f;
		timer = 0.0f;

		turret = gameObject.transform.GetChild(0).transform;
		bulletSpawnPoint = turret.gameObject.transform.GetChild(0).transform;
	}
	
	// Update is called once per frame
	void Update () {
		NavMeshHit hit;
		agent.FindClosestEdge (out hit);
		var rot = Quaternion.LookRotation(hit.normal);
		transform.rotation = Quaternion.Slerp (transform.rotation, rot, 3.0f * Time.deltaTime);


		enemies = GameObject.FindGameObjectsWithTag ("Enemy");
		enemiesT = new Transform[enemies.Length];
		for (int i = 0; i<enemies.Length; i++) {
			enemiesT [i] = enemies [i].transform;
		}
		closestBH = GetClosestEnemy (enemiesT);
		
		
		timer -= Time.deltaTime;
		
		if (timer <= 0.0f) {
			if((closestBH!=null)&&Vector3.Distance (transform.position, closestBH.position) < 150.0f){
				Quaternion turretRotation = Quaternion.LookRotation (closestBH.position - turret.position);
				turret.rotation = Quaternion.Slerp (turret.rotation, turretRotation, Time.deltaTime * 500.0f);
				ShootBullet ();
			}
			timer = interval;
		}

	}

	void ShootBullet(){
		Instantiate (Bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
	}

	Transform GetClosestEnemy(Transform[] enemies)
	{
		Transform tMin = null;
		float minDist = Mathf.Infinity;
		Vector3 currentPos = transform.position;
		foreach (Transform t in enemies)
		{
			float dist = Vector3.Distance(t.position, currentPos);
			if (dist < minDist)
			{
				tMin = t;
				minDist = dist;
			}
		}
		return tMin;
	}

}
