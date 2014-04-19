using UnityEngine;
using System.Collections;

public class AllyScript : MonoBehaviour {
	
	[SerializeField] GameObject explosion;
	public GameObject prefab;
	public Transform target;
	public float rotSpeed = 100f;
	public float WaveSpeed = 400f;
	public float minDistance;
	GameObject camera;

	public GameObject EnemyTarget;
	AllyScript ally1;
	AllyScript ally2;

	bool enemyTargetSet = false;
	

	// Use this for initialization
	void Start () {
		camera = GameObject.FindGameObjectWithTag("MainCamera");
		target = transform.parent.parent;
		minDistance = Vector3.Distance(target.position , transform.position);
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(!enemyTargetSet)
		{
			if ( Time.timeScale <= 0 ) return;
			//transform.Rotate(Vector3.up, rotSpeed * Time.deltaTime);
			transform.RotateAround(target.position, Vector3.up, WaveSpeed * Time.deltaTime);
			
			//fix possible changes in distance
			float currentDistance = Vector3.Distance(target.position, transform.position);
			Vector3 towardsTarget = transform.position - target.position;
			transform.rotation = Quaternion.LookRotation(towardsTarget);
		}
		else
		{
			SteeringArrive (EnemyTarget.transform.position);
		}
	}
	
	void OnTriggerEnter(Collider collision)
	{
		if (collision.gameObject.tag == "Enemy") 
		{
			if(!enemyTargetSet)
			{
				//check to see if this enemy is currently being targeted by other allies
				if(collision.gameObject != ally1.EnemyTarget && collision.gameObject != ally2.EnemyTarget)
				{
					EnemyTarget = collision.gameObject;
					Destroy(collider);
					gameObject.AddComponent<BoxCollider>();
					enemyTargetSet = true;
					transform.parent = GameObject.Find("AttackingAllies").transform;
				}
			}
			//UnityEditor.EditorApplication.isPaused = true;
			else
			{
				if(EnemyTarget != null)
				{
					//collision.gameObject.GetComponent<Behaviour>().lowEnemyHealth -= 3f;
					
					//Destroy(transform.parent.gameObject);
					Destroy(collision.gameObject);
					Destroy(gameObject);
					Instantiate(explosion,transform.position,new Quaternion());
					
					camera.GetComponent<Shooting>().nbAlly -= 1;
				}
				else
				{//if the enemy has been killed
					Destroy(gameObject);
					Instantiate(explosion,transform.position,new Quaternion());
				}
			}
		}
	}

	Vector3 cVelocity = new Vector3(0.5f, 0, 0.5f);
	Vector3 acceleration;
	float maxAcceleration = 16.0f;
	float maxVelocity = 25.0f;
	float timeBetweenUpdates = 1.0f/2.0f; //1/4
	const int mass = 2;

	void seek(Vector3 position)
	{
		Vector3 distance = position - gameObject.transform.position;
		Vector3 acceleration = (distance / distance.magnitude) * maxAcceleration;
		Vector3 velocity = cVelocity  + (acceleration * timeBetweenUpdates);
		this.transform.rotation = Quaternion.Slerp (this.transform.rotation, Quaternion.LookRotation (velocity), Time.deltaTime);
		if(velocity.magnitude < maxVelocity){
			gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, position, 15f * Time.deltaTime);   //gameObject.transform.position + (velocity * timeBetweenUpdates);
			
		}
	}

	private void SteeringArrive(Vector3 target) 
	{
		Vector3 targetOffset = target - transform.position;
		
		float distance = targetOffset.magnitude;
		//float rampedSpeed = MaxSpeed * (distance / 2);
		float rampedSpeed = maxVelocity;
		float clippedSpeed = Mathf.Min(rampedSpeed, maxVelocity);
		Vector3 desiredVelocity = (clippedSpeed / distance) * targetOffset;
		Vector3 steering;

		steering = desiredVelocity - cVelocity;
		
		steering = Vector3.ClampMagnitude(steering, maxVelocity);
		acceleration = steering / mass;
		cVelocity = Vector3.ClampMagnitude((cVelocity + acceleration), maxVelocity);
		transform.position = transform.position + cVelocity *Time.deltaTime;
		transform.LookAt(transform.position + cVelocity *Time.deltaTime);
	}

	public void SetAllies(AllyScript one, AllyScript two)
	{
		ally1 = one;
		ally2 = two;
	}
}
