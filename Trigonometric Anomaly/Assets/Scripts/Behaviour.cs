using UnityEngine;
using System.Collections;

public class Behaviour : MonoBehaviour {

	//default behaviour int to NONE (size - 1)
	public int behaviourInt = System.Enum.GetNames(typeof(EnumScript.EnemyType)).Length - 1;
	EnumScript.EnemyType enemyType;
	BoxCollider box;
	Material blueMat;
	Material greenMat;
	Material redMat;

	public float lowEnemyHealth;

	const float FLEE_DISTANCE = 15.0f;
	float fleeAcceleration = 2.0f;
	float fleeVelocity = 2.0f;
	Vector3 playerPosition;
	Vector3 targetPosition;
	Vector3 cVelocity = new Vector3(0.1f, 0, 0.1f);
	float maxAcceleration = 1.0f;
	float maxVelocity = 1.0f;
	float timeBetweenUpdates = 1.0f/2.0f; //1/4
	float wanderDegAngle;
	float wanderRadianAngle;
	float wanderRadius = 2.0f;
	float rotationSpeed = 1.0f;
	
	float time = 0.0f;
	
	Vector3 wayPoint;
	

	// Use this for initialization
	void Start () {
		lowEnemyHealth = 4f;
		blueMat = Resources.Load ("Materials/blue") as Material;
		greenMat = Resources.Load("Materials/green") as Material;
		redMat = Resources.Load ("Materials/red") as Material;
		box = GameObject.Find ("BoundingBox").GetComponent<BoxCollider>();

		wanderDegAngle = Random.Range (0, 359);
		wanderRadianAngle = wanderDegAngle * Mathf.Deg2Rad;
		intToEnum();		
	}
	
	// Update is called once per frame
	void Update () {

		if (lowEnemyHealth == 0)
						Destroy(gameObject);
//		boundaryCheck();
	
		switch(enemyType){
			//Most basic enemy type; random movement.
			case EnumScript.EnemyType.BLUE_ENEMY:
				blueBehaviour();
				break;
			//Random movement but will try to evade player.
			case EnumScript.EnemyType.GREEN_ENEMY:
				greenBehaviour();
				break;
			case EnumScript.EnemyType.NONE:
				break;
		}
	}

	void blueBehaviour(){
		transform.position += transform.TransformDirection(Vector3.forward) * 20f * Time.deltaTime;
		Wander();
	}

	void greenBehaviour(){
		playerPosition = GameObject.Find("Player").transform.position;
		Vector3 direction = gameObject.transform.position - playerPosition;
		if(direction.magnitude < FLEE_DISTANCE){
			//flee(direction);
			Sflee(playerPosition);
		}else{
			wander();
		}
	}

	void flee(Vector3 direction){
		Vector3 acceleration = (direction / direction.magnitude) * fleeAcceleration;
		Vector3 velocity = cVelocity + (acceleration * timeBetweenUpdates);
		if(velocity.magnitude < fleeVelocity){
			gameObject.transform.position = gameObject.transform.position + (velocity * timeBetweenUpdates);
		}


	}

	void Sflee(Vector3 position){

		Vector3 direction = transform.position - position;
		
		direction.y = 0;
		
		if (direction.magnitude < FLEE_DISTANCE)
		{
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);
			
			Vector3 move = direction.normalized * 20f * Time.deltaTime;
			
			transform.position += move;
		}
		
		
	}

	void wander(){
		wanderDegAngle += Random.Range (-5.0f, 5.0f);
		wanderRadianAngle = wanderDegAngle * Mathf.Deg2Rad;

		float x = Mathf.Round (gameObject.transform.position.x + (wanderRadius * Mathf.Sin (wanderRadianAngle)));
		float y = gameObject.transform.position.y;
		float z = Mathf.Round (gameObject.transform.position.z + (wanderRadius * Mathf.Cos(wanderRadianAngle)));

		targetPosition = new Vector3(x,y,z);
		seek();
	}

	void Wander()
	{
		time += Time.deltaTime;
		
		if (time > 3)
		{
			wayPoint = Random.insideUnitSphere * 47;
			wayPoint.y = 1.0f;
			transform.LookAt(wayPoint);
			
			time = 0;
		}
	}

	void seek(){
		if(targetPosition != null){
			Vector3 distance = targetPosition - gameObject.transform.position;
			Vector3 acceleration = (distance / distance.magnitude) * maxAcceleration;
			Vector3 velocity = cVelocity + (acceleration * timeBetweenUpdates);
			gameObject.transform.LookAt (targetPosition);
			if(velocity.magnitude < maxVelocity){
				gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, targetPosition, 10f * Time.deltaTime);   //gameObject.transform.position + (velocity * timeBetweenUpdates);

			}
		}
	}

	void boundaryCheck(){
		if(gameObject.transform.position.x > box.bounds.max.x){
			gameObject.transform.position = new Vector3(box.bounds.min.x + 0.1f, gameObject.transform.position.y, gameObject.transform.position.z);
		}if(gameObject.transform.position.x < box.bounds.min.x){
			gameObject.transform.position = new Vector3(box.bounds.max.x - 0.1f, gameObject.transform.position.y, gameObject.transform.position.z);
		}if(gameObject.transform.position.z > box.bounds.max.z){
			gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, box.bounds.min.z + 0.1f);
		}if(gameObject.transform.position.z < box.bounds.min.z){
			gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, box.bounds.max.z - 0.1f);
		}
	}
	
	void intToEnum(){
		switch(behaviourInt){
			case 0:	//BLUE
				enemyType = EnumScript.EnemyType.BLUE_ENEMY;
				gameObject.renderer.material = blueMat;
				break;
			case 1: //GREEN
				enemyType = EnumScript.EnemyType.GREEN_ENEMY;
				gameObject.renderer.material = greenMat;
				break;
			case 2:	//NONE
				enemyType = EnumScript.EnemyType.NONE;
				gameObject.renderer.material = redMat;
				break;
		}
	}

//	void OnTriggerEnter(Collider collision)
//	{
//		if (collision.gameObject.tag == "Bullet") {
//			Destroy(gameObject);
//				}
//	}
}
