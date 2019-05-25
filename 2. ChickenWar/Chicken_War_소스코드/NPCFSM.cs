using UnityEngine;
using System.Collections;

public class NPCFSM : MonoBehaviour, IDamageable {
	public Vector3 destPos, buildingPos;
	protected GameObject pointList;
	private Transform turret{ get; set; }
	private Transform bulletSpawnPoint{ get; set; }
	public GameObject Bullet;

	protected GameObject[] playerpointList;

	private NavMeshAgent agent;
	private GameObject [] building;	//Scene에 배치된 빌딩을 불러옴
	private Transform [] buildingsT;	//가장 가까운 빌딩을 찾기 위해 필요
	private Transform closestB;	//검은닭이 공격할 빌딩

	private GameObject [] enemies;
	private Transform [] enemiesT;
	private Transform closestWH;

	private float timer;
	private float interval;

	//Enemy 닭 체력 계산 변수들
	private float Enemy_M_HP=50;
	private float Enemy_D_HP=20;

	public GameObject gameController;
	private GameController gc;

	// Use this for initialization
	void Start () {
		//리더 
		pointList = GameObject.FindGameObjectWithTag("Leader");
		agent = GetComponent ("NavMeshAgent") as NavMeshAgent;

		gameController = GameObject.Find("GameController");
		gc = gameController.GetComponent<GameController>();

		turret = gameObject.transform.GetChild(0).transform;
		bulletSpawnPoint = turret.gameObject.transform.GetChild(0).transform;

		interval = 0.5f;
		timer = 0.0f;

		//Enemy 닭 현재 체력에 전체 체력 할당
		//Enemy_D_HP = Enemy_M_HP;
	}
	
	// Update is called once per frame
	void Update () {

		Quaternion targetRotation = Quaternion.LookRotation (pointList.transform.position - transform.position);
		transform.rotation = Quaternion.Slerp (transform.rotation, targetRotation, Time.deltaTime * 2.0f);
		transform.Translate (Vector3.forward * Time.deltaTime * 150.0f);

		building = GameObject.FindGameObjectsWithTag ("building");
		if (building.Length <= 0) {
			GameObject.Find("GameController").gameObject.SendMessage("GameEnd");
		}
		buildingsT = new Transform[building.Length];
		
		for (int i = 0; i<building.Length; i++) {
			buildingsT[i] = building[i].transform;
		}

		closestB = GetClosestEnemy (buildingsT);
		timer -= Time.deltaTime;

		if (timer <= 0.0f) {
			if (Vector3.Distance (transform.position, closestB.position) < 300.0f) {
				Quaternion turretRotation = Quaternion.LookRotation (closestB.position - turret.position);
				turret.rotation = Quaternion.Slerp (turret.rotation, turretRotation, Time.deltaTime * 100.0f);
				ShootBullet ();
			}
			timer = interval;
		}

		NavMeshHit hit;
		agent.FindClosestEdge (out hit);
		var rot = Quaternion.LookRotation(hit.normal+transform.forward);
		transform.rotation = Quaternion.Slerp (transform.rotation, rot, 3.0f * Time.deltaTime);

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

	public void Damage(float damageTaken){
		Enemy_D_HP -= damageTaken;
		Debug.Log (Enemy_D_HP);
		if (Enemy_D_HP <= 0) {
			Enemy_D_HP =0;
			gc.getCoin(20);
			Destroy(gameObject);
		}
	}
}
