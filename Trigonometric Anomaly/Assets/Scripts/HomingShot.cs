using UnityEngine;
using System.Collections;

public class HomingShot : MonoBehaviour {

	float maxAcceleration = 8.0f;
	float maxVelocity = 4.0f;
	Vector3 cVelocity = new Vector3(2.5f, 0, 2.5f);
	float timeBetweenUpdates = 1.0f/4.0f; //1/4
	Vector3 characterPosition;
	// the tag to search for (set this value in the inspector)
	string searchTag = "Enemy";
	Vector3 targetPosition;
	float speed;
	float Turn;
	
	Vector3 clickedPosition;

	
	// the current target
	private Transform target;
	
	
	void Start() {

	}
	
	void Update() {
		if ( Time.timeScale <= 0 ) return;
			movement();
		if(target != null)
			transform.LookAt(target);
	}



	public void movement()
	{

		Vector3 direction = clickedPosition - characterPosition;
		
		direction.y = 0;
		
		float speed = 30.0f;
		
		Vector3 move = direction.normalized * Time.deltaTime * speed;

		target = getTarget();
		
		if ((target.position - transform.position).magnitude < 8)
				{
					direction = target.position - characterPosition;
					move = direction.normalized * Time.deltaTime *0.75f* speed;
					transform.position += move;

				} else
				{
						transform.position += move;	
				}
		
		transform.rotation = Quaternion.LookRotation(move);

		Debug.DrawLine (characterPosition, transform.position, Color.green, 100);


//		if(targetPosition != null){
//			Vector3 distance = targetPosition - gameObject.transform.position;
//			Vector3 acceleration = (distance / distance.magnitude) * maxAcceleration;
//			Vector3 velocity = cVelocity + (acceleration * timeBetweenUpdates);
//			gameObject.transform.LookAt (targetPosition);
//			if(velocity.magnitude < maxVelocity){
//				gameObject.transform.position = gameObject.transform.position + (velocity * timeBetweenUpdates);
//			}
//		}
		
	}

	void OnTriggerEnter(Collider collision)
	{
		if (collision.gameObject.tag == "Enemy") {

				collision.gameObject.GetComponent<Behaviour>().lowEnemyHealth -= 1f;

			//Destroy(transform.parent.gameObject);
			Destroy(gameObject);
			
			
		}

	}

	public  void PassPositions(Vector3 click, Vector3 character)
	{
		this.clickedPosition = click;
		this.characterPosition = character;
	}

	Transform getTarget()
	{
		GameObject[] targets = GameObject.FindGameObjectsWithTag("Enemy");
		GameObject closest = null;
		float closestDist = Mathf.Infinity;
		
		foreach (GameObject target in targets)
		{
			float distance = (transform.position - target.transform.position).sqrMagnitude;
			if(distance < closestDist)
			{
				closestDist = distance;
				closest = target;
			}
		}

		return closest.transform;
	}
}
