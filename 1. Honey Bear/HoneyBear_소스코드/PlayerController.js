#pragma strict

@script RequireComponent(PlayerStatus)

private var walkSpeed : float = 20.0;
private var gravity : float = 10.0;
private var jumpSpeed : float = 80.0;
private var velocity : Vector3;
private var forwardSpeed : int = 10;
private var playerStatus : PlayerStatus;

private var timer : float = 45.0;//honey생성되기 전까지 플레이타임

function Start () {
	animation["Idle"].speed = 2.0;
	playerStatus = GetComponent(PlayerStatus) as PlayerStatus;	
}

function Update () {
	var controller : CharacterController = GetComponent(CharacterController);
		
		timer -= Time.deltaTime;
		
		if(controller.isGrounded){
			BroadcastMessage("GameOver");
			SendMessageUpwards("GameOver");
		}
		
		velocity = Vector3(Input.GetAxis("Horizontal"), 0, 0);
		//velocity = Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
		velocity *= walkSpeed;
		
		controller.Move(Vector3.forward * Time.deltaTime * forwardSpeed);
			
		if(timer<=0.0){
			SendMessageUpwards("afterTrip","true");
			BroadcastMessage("afterTrip","null");
		}
		if(Input.GetKey(KeyCode.LeftArrow)){
			animation.CrossFade("Left");
		}
		else if(Input.GetKey(KeyCode.RightArrow)){
			animation.CrossFade("Right");
		}
		else if(Input.GetButtonDown("Jump")&&playerStatus.tempFuel>=3){
			velocity.y = jumpSpeed;	//급가속
			gameObject.SendMessage("Jump",3);
		}
		else{
			animation.CrossFade("Idle",0.1);
		}

	velocity.y -= gravity * Time.deltaTime * 10;
	controller.Move(velocity*Time.deltaTime);
}