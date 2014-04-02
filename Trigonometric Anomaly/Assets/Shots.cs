using UnityEngine;
using System.Collections;

public class Shots : MonoBehaviour {

	public bool charge = false;
	bool isCharging = false;
	GameObject obj;
	public GameObject sphere;
	float startTime;
	Transform childSphere;
	Vector3 clickPosition;
	Vector3 charPosition;
	public GameObject boundingBox;

	public float CurveSpeed = 5;
	public float MoveSpeed = 2;
	
	float fTime = 0;
	Vector3 vLastPos = Vector3.zero;


	// Use this for initialization
	void Start () {
		boundingBox = GameObject.FindGameObjectWithTag ("BoundingBox");
	}
	
	// Update is called once per frame
	void Update () {

		//print (isCharging);
		KTest();
		//SinTest ();
		//ChargeTest ();

		if (!boundingBox.GetComponent<BoxCollider> ().bounds.Contains (this.transform.position))
						Destroy (gameObject);
	}
	
	public  void PassPositions(Vector3 click, Vector3 character)
	{
		this.clickPosition = click;
		this.charPosition = character;
	}
	
	public void KTest()
	{
		
		Vector3 direction = clickPosition - charPosition;
		
		direction.y = 0;
		
		float distance = direction.magnitude;
		
		float speed = 12.0f;
		
		Vector3 move = direction.normalized * Time.deltaTime * speed;
		
		transform.position += move;	

		Debug.DrawLine(charPosition, transform.position, Color.green, 100);
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


	
	
	
}
