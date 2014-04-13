using UnityEngine;
using System.Collections;

public class Shots : MonoBehaviour {

	public bool sine = false;
	float rotationSpeed = 5.0f;
	bool isCharging = false;
	public GameObject sphere;
	float startTime;
	Transform childSphere;
	Vector3 clickedPosition;
	Vector3 characterPosition;
	public GameObject boundingBox;
	float dmg;
	int shotType;

	// Use this for initialization
	void Start () {
		boundingBox = GameObject.FindGameObjectWithTag ("BoundingBox");
	}
	
	// Update is called once per frame
	void Update () {

		movement();

		//Destroy Bullet when leaving Bounding Box
		if (!boundingBox.GetComponent<BoxCollider> ().bounds.Contains (this.transform.position))
						Destroy (gameObject);
	}
	
	public  void PassPositions(Vector3 click, Vector3 character)
	{
		this.clickedPosition = click;
		this.characterPosition = character;
	}
	
	public void movement()
	{
						Vector3 direction = clickedPosition - characterPosition;
		
						direction.y = 0;

						float speed = 20.0f;
		
						Vector3 move = direction.normalized * Time.deltaTime * speed;
		
						transform.position += move;	

						if (sine == true) {
							transform.Rotate(0,rotationSpeed*Time.deltaTime,0);
						}

						Debug.DrawLine (characterPosition, transform.position, Color.green, 100);
				
	}





	public void ChargeTest()
	{
//		while(Input.GetMouseButtonDown (0)) {
//						isCharging = true;
//						gameObject.transform.localScale += new Vector3 (Time.deltaTime, Time.deltaTime, Time.deltaTime);
//
//				}
//
//		isCharging = false;
//
//
//		if (isCharging == false && Input.GetMouseButtonUp(0)) {
//						Vector3 direction = clickPosition - charPosition;
//		
//						direction.y = 0;
//		
//						float distance = direction.magnitude;
//		
//						float speed = 12.0f;
//		
//						Vector3 move = direction.normalized * Time.deltaTime * speed;
//		
//						transform.position += move;	
//		
//						Debug.DrawLine (charPosition, transform.position, Color.green, 100);
//				}
	}


		void OnTriggerEnter(Collider collision)
		{
			if (collision.gameObject.tag == "Enemy") {

					collision.gameObject.GetComponent<Behaviour>().lowEnemyHealth -= 1;
				
			Destroy(gameObject);
					
				}
				

		}
		

	
	
	
}
