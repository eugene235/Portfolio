#pragma strict

private var walkSpeed : float = 20.0;
private var gravity : float = 10.0;
private var jumpSpeed : float = 80.0;
private var velocity : Vector3;
private var forwardSpeed : int = 10;

function Start () {
	animation["Idle"].speed = 2.0;
}

function Update () {
	var controller : CharacterController = GetComponent(CharacterController);
	
		//velocity = Vector3(Input.GetAxis("Horizontal"), 0, 0);
		velocity = Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
		velocity *= walkSpeed;
		
		//controller.Move(Vector3.forward * Time.deltaTime * forwardSpeed);
			
		if(Input.GetKey(KeyCode.LeftArrow)){
			animation.CrossFade("Left");
		}
		else if(Input.GetKey(KeyCode.RightArrow)){
			animation.CrossFade("Right");
		}
		else if(Input.GetButtonDown("Jump")){
			velocity.y = jumpSpeed;	//급가속
		}
		else{
			animation.CrossFade("Idle",0.1);
		}

	velocity.y -= gravity * Time.deltaTime * 10;
	controller.Move(velocity*Time.deltaTime);
}