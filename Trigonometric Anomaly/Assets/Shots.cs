using UnityEngine;
using System.Collections;

public class Shots : MonoBehaviour {
	
	
	Vector3 clickPosition;
	Vector3 charPosition;
	public GameObject boundingBox;
	
	// Use this for initialization
	void Start () {

		boundingBox = GameObject.FindGameObjectWithTag ("BoundingBox");
	}
	
	// Update is called once per frame
	void Update () {
		KTest();

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
	}
	
	
	
}
