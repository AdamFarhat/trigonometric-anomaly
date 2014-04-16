using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Flocking coding:http://gamedevelopment.tutsplus.com/tutorials/the-three-simple-rules-of-flocking-behaviors-alignment-cohesion-and-separation--gamedev-3444

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
	const float MIN_RANGE = 25.0f;
	float fleeAcceleration = 2.0f;
	float fleeVelocity = 2.0f;
	Vector3 playerPosition;
	Vector3 cVelocity = new Vector3(0.1f, 0, 0.1f);
	float maxAcceleration = 1.0f;
	float maxVelocity = 5.0f;
	float timeBetweenUpdates = 1.0f/2.0f; //1/4
	float wanderDegAngle;
	float wanderRadianAngle;
	float wanderRadius = 2.0f;
	float rotationSpeed = 1.0f;
	
	float time = 0.0f;
	
	Vector3 wayPoint;
	
	GameObject reds;
	GameObject blues;
	float alignmentThreshold = 3f;
	float cohesionThreshold = 25.0f;
	float separationThreshold = 5f;

	GameObject camera;
	int points;
	
	// Use this for initialization
	void Start () {
		lowEnemyHealth = 4f;
		camera = GameObject.FindGameObjectWithTag("MainCamera");
		blueMat = Resources.Load ("Materials/blue") as Material;
		greenMat = Resources.Load("Materials/green") as Material;
		redMat = Resources.Load ("Materials/red") as Material;
		box = GameObject.Find ("BoundingBox").GetComponent<BoxCollider>();
		
		wanderDegAngle = Random.Range (0, 359);
		wanderRadianAngle = wanderDegAngle * Mathf.Deg2Rad;
		intToEnum();		
		
		reds = GameObject.Find("Reds");
		blues = GameObject.Find("Blues");
	}
	
	// Update is called once per frame
	void Update () {

		if (ShopWindow.Instance.enabled == true)
				{
						if (renderer.isVisible)
						{
							Destroy(gameObject);
						}
				}
				
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
				//Flocking behaviour
			case EnumScript.EnemyType.RED_ENEMY:
				redBehaviour();
				break;
			case EnumScript.EnemyType.NONE:
				break;
		}
	}
	
	void blueBehaviour(){
		blues = GameObject.Find("Blues");
		float separationWeight = 0.2f;
		
		Vector3 separation = computeSeparation(blues);
		Vector3 targetDirection = separation * separationWeight;
		targetDirection.Normalize();
		
		
		playerPosition = GameObject.Find("Player").transform.position;
		Vector3 direction = playerPosition - gameObject.transform.position;
		if(direction.magnitude < MIN_RANGE){
			seek(playerPosition + targetDirection* maxVelocity*2);
		}else{
			wander();
		}
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
	
	void redBehaviour(){		
		reds = GameObject.Find("Reds");
		blues = GameObject.Find("Blues");
		
		float alignmentWeight = 0.1f;
		float cohesionWeight = 0.15f;
		float separationWeight = 0.2f;
		
		Vector3 alignment = computeAlignment(reds) + computeAlignment(blues);
		Vector3 cohesion = computeCohesion(reds) + computeCohesion(blues);
		Vector3 separation = computeSeparation(reds) + computeSeparation(blues);
		Vector3 targetDirection = alignment * alignmentWeight + cohesion * cohesionWeight + separation * separationWeight;
		targetDirection.Normalize();
		Debug.DrawLine(this.transform.position, this.transform.position + targetDirection * maxVelocity, Color.green);
		if (targetDirection != Vector3.zero)
		{
			seek(this.transform.position + targetDirection * maxVelocity);
		} 
		else
		{
			wander();
		}
	}
	
	Vector3 computeAlignment(GameObject group){
		Vector3 v = new Vector3();
		int nbCount = 0;
		
		for (int i = 0; i < group.transform.childCount; i++)
		{
			Transform current = group.transform.GetChild(i);
			float distance = Vector3.Distance(current.position, this.transform.position);
			if(distance < alignmentThreshold)
			{
				v += current.forward;
				nbCount++;
			}
		}
		
		if(nbCount == 0)
			return v;
		v /= nbCount;
		v.Normalize();
		return v;
	}
	
	Vector3 computeCohesion(GameObject group){
		Vector3 v = new Vector3();
		int nbCount = 0;
		for (int i = 0; i < group.transform.childCount; i++)
		{
			Transform current = group.transform.GetChild(i);
			if(current.transform.position != this.transform.position){
				float distance = Vector3.Distance(current.position, this.transform.position);
				if(distance < cohesionThreshold){
					v += current.position;
					nbCount++;
				}
			}
		}
		if(nbCount == 0)
			return v;
		v /= nbCount;
		v -= this.transform.position;
		v.Normalize();
		return v;
	}
	
	Vector3 computeSeparation(GameObject group){
		Vector3 pos = new Vector3();
		int nbCount = 0;
		
		for (int i = 0; i < group.transform.childCount; i++)
		{
			Transform current = group.transform.GetChild(i);
			if(current.transform.position != this.transform.position){
				float distance = Vector3.Distance(current.position, this.transform.position);
				if(distance < separationThreshold){
					pos += current.position - this.transform.position;
					nbCount++;
				}
			}
		}
		
		if(nbCount == 0)
			return pos;
		pos /= nbCount;
		pos *= -1;
		pos.Normalize();
		return pos;
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
		
		if (direction.magnitude < FLEE_DISTANCE){
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);
			Vector3 move = direction.normalized * 20f * Time.deltaTime;
			transform.position += move;
		}
	}
	
	Vector3 computeTargetPosition(){
		wanderDegAngle += Random.Range (-5.0f, 5.0f);
		wanderRadianAngle = wanderDegAngle * Mathf.Deg2Rad;
		
		float x = Mathf.Round (gameObject.transform.position.x + (wanderRadius * Mathf.Sin (wanderRadianAngle)));
		float y = gameObject.transform.position.y;
		float z = Mathf.Round (gameObject.transform.position.z + (wanderRadius * Mathf.Cos(wanderRadianAngle)));
		
		return new Vector3(x,y,z);
	}
	
	void wander(){
		seek(computeTargetPosition());
	}
	
	void Wander(){
		time += Time.deltaTime;
		if (time > 3){
			wayPoint = Random.insideUnitSphere * 47;
			wayPoint.y = 1.0f;
			transform.LookAt(wayPoint);
			
			time = 0;
		}
	}
	
	void seek(Vector3 position){
		Vector3 distance = position - gameObject.transform.position;
		Vector3 acceleration = (distance / distance.magnitude) * maxAcceleration;
		Vector3 velocity = cVelocity + (acceleration * timeBetweenUpdates);
		this.transform.rotation = Quaternion.Slerp (this.transform.rotation, Quaternion.LookRotation (velocity), Time.deltaTime);
		if(velocity.magnitude < maxVelocity){
			gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, position, 10f * Time.deltaTime);   //gameObject.transform.position + (velocity * timeBetweenUpdates);
			
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
			case 2: //RED
				enemyType = EnumScript.EnemyType.RED_ENEMY;
				gameObject.renderer.material = redMat;
				break;
			case 3:	//NONE
				enemyType = EnumScript.EnemyType.NONE;
				gameObject.renderer.material = redMat;
				break;
		}
		gameObject.transform.name = enemyType.ToString();
	}
	
		void OnTriggerEnter(Collider collision)
		{
			if (collision.gameObject.tag == "Bullet") {
				if (lowEnemyHealth <= 0)
				{
					if(behaviourInt == 0)
					{
						ScoreController.Instance.addScore(1000);
					}
					if(behaviourInt == 1)
					{
						ScoreController.Instance.addScore(2000);
					}
					if(behaviourInt == 2)
					{
						ScoreController.Instance.addScore(3000);
					}
				Destroy(gameObject);
				}
			}

			if (collision.gameObject.layer == 13)
				{

					bool has = false;
					Destroy(collision.gameObject);
					camera.GetComponent<Shooting>().hasShield = false;
					camera.GetComponent<Shooting>().shieldSet = false;
					Destroy(gameObject);
				}
			
		}
}

