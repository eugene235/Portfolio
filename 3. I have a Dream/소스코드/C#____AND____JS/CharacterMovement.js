#pragma strict

private var anim : Animator;
private var moveSpeed : float = 1.0f;
private var h : float;
private var jumpSpeed : float = 3.0f;
private var gravity : float =5.0f;
private var curPos : Vector3;
private var cPos : Vector3;

function Start () {
	anim = gameObject.GetComponent(Animator);
	curPos = transform.position;
}

function Update () {
	h = Input.GetAxis("Horizontal") * moveSpeed;
	transform.Translate(Vector2.right*h);
	
	var angle = Input.acceleration.x;
	transform.Translate(angle,0,0);
	
	if(transform.position.y > curPos.y)
			transform.position.y -= gravity * Time.deltaTime;
}

function Jump(){
		transform.position.y = jumpSpeed;
}