using UnityEngine;
using System.Collections;

//Leader GameObject에 patrol 적용, wanderpoint들을 순회

public class LeaderPatrol : MonoBehaviour {

	protected GameObject[] WanderPointList;
	public Vector3 NextWanderPosition;
	private NavMeshAgent agent;

	// Use this for initialization
	void Start () {

		WanderPointList = GameObject.FindGameObjectsWithTag("wanderpoint");
		FindNextWanderPoint ();
	
		agent = GetComponent ("NavMeshAgent") as NavMeshAgent;
	}
	
	// Update is called once per frame
	void Update () {

		Quaternion wanderrotation = Quaternion.LookRotation (NextWanderPosition - transform.position);
		transform.rotation = Quaternion.Slerp (transform.rotation, wanderrotation, Time.deltaTime * 2.0f);
		transform.Translate (Vector3.forward * Time.deltaTime * 300.0f);

		if (Vector3.Distance (transform.position, NextWanderPosition) < 50.0f) {
			FindNextWanderPoint();
		}
	
		NavMeshHit hit;
		agent.FindClosestEdge (out hit);
		var rot = Quaternion.LookRotation(hit.normal+transform.forward);
		transform.rotation = Quaternion.Slerp (transform.rotation, rot, 5.0f * Time.deltaTime);
	}

	void FindNextWanderPoint (){
		int rnd = Random.Range (0, WanderPointList.Length);
		NextWanderPosition = WanderPointList [rnd].transform.position;
	}
}