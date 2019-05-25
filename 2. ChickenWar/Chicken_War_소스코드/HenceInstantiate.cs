using UnityEngine;
using System.Collections;


public class HenceInstantiate : MonoBehaviour {

	public Transform prefab;	
	protected GameObject[] pointList;
	public float interval;
	private int Index;

	//Leader를 할 gameObject생성
	protected GameObject LeaderObject;

	// Use this for initialization
	void Start () {

		pointList = GameObject.FindGameObjectsWithTag("wanderpoint");
		//Debug.Log (pointList.Length);
		Index = Random.Range (0, pointList.Length);

		//검은 닭 생성할 position에 leader 배치 
		LeaderObject = GameObject.FindGameObjectWithTag ("Leader");
		//LeaderObject = new GameObject ();
		LeaderObject.transform.position = pointList [Index].transform.position;
		//LeaderObject.tag = "Leader";
	}
	
	// Update is called once per frame
	void Update () {

		interval -= Time.deltaTime;
		
		if (interval <= 0.0f) {
			Instantiate (prefab, pointList[Index].transform.position, Quaternion.identity);
			interval = 5.0f;
		}
	}
}

	
