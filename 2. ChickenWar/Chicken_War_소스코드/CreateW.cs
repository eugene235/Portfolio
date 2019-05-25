using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CreateW : MonoBehaviour{

	public GameObject gameController;
	private GameController gc;

	public GameObject prefab;
	private Vector3 pos;
	private Button myButton;

	Plane playerPlane;
	Ray RayCast;
	float HitDist;
	
	// Use this for initialization
	void Start () {

		gc = gameController.GetComponent<GameController>();
		pos = Vector3.zero;
		myButton = this.GetComponent("Button") as Button;
	
		myButton.onClick.AddListener (create);
		playerPlane = new Plane(Vector3.up, transform.position);

	}
	// Update is called once per frame
	void Update () {

	}

	void create (){
		if(gc.getM()>=30){
			RayCast = Camera.main.ScreenPointToRay(Input.mousePosition);
			HitDist = 0;

			if(playerPlane.Raycast(RayCast, out HitDist)){
				Vector3 RayHitPoint = RayCast.GetPoint(HitDist);
				pos = RayHitPoint;
				pos.y = 0;
			}
			Instantiate(prefab, pos ,Quaternion.identity);
			Destroy (gameObject);
			gc.minusM(30);
		}
	}
}