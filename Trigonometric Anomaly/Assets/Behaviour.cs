using UnityEngine;
using System.Collections;

public class Behaviour : MonoBehaviour {

	//default behaviour int to NONE (size - 1)
	public int behaviourInt = System.Enum.GetNames(typeof(EnumScript.EnemyType)).Length - 1;
	EnumScript.EnemyType enemyType;
	BoxCollider box;
	Material blueMat;
	Material redMat;

	Vector3 targetPosition;
	Vector3 cVelocity = new Vector3(0.1f, 0, 0.1f);
	float maxAcceleration = 1.0f;
	float maxVelocity = 1.0f;
	float timeBetweenUpdates = 1.0f/4.0f; //1/4
	float wanderDegAngle;
	float wanderRadianAngle;
	float wanderRadius = 2.0f;
	

	// Use this for initialization
	void Start () {
		blueMat = Resources.Load ("Materials/blue") as Material;
		redMat = Resources.Load ("Materials/red") as Material;
		box = GameObject.Find ("BoundingBox").GetComponent<BoxCollider>();

		wanderDegAngle = Random.Range (0, 359);
		wanderRadianAngle = wanderDegAngle * Mathf.Deg2Rad;
		intToEnum();		
	}
	
	// Update is called once per frame
	void Update () {
		boundaryCheck();
	
		switch(enemyType){
			case EnumScript.EnemyType.WANDER:
				wander();
				break;
			case EnumScript.EnemyType.NONE:
				break;
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

	void seek(){
		if(targetPosition != null){
			Vector3 distance = targetPosition - gameObject.transform.position;
			Vector3 acceleration = (distance / distance.magnitude) * maxAcceleration;
			Vector3 velocity = cVelocity + (acceleration * timeBetweenUpdates);
			gameObject.transform.LookAt (targetPosition);
			if(velocity.magnitude < maxVelocity){
				gameObject.transform.position = gameObject.transform.position + (velocity * timeBetweenUpdates);
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
			case 0:	//WANDER
				enemyType = EnumScript.EnemyType.WANDER;
				gameObject.renderer.material = blueMat;
				break;
			case 1:	//NONE
				enemyType = EnumScript.EnemyType.NONE;
				gameObject.renderer.material = redMat;
				break;
		}
	}

	void OnTriggerEnter(Collider collision)
	{
		if (collision.gameObject.tag == "Bullet") {
			Destroy(gameObject);
				}
	}
}
