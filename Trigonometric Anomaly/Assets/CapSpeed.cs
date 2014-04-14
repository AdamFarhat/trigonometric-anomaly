using UnityEngine;
using System.Collections;

public class CapSpeed : MonoBehaviour {
	[SerializeField] private float speed = 10f;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector3 velocity = this.rigidbody.velocity;
		if (velocity.magnitude > speed)
		{
			this.rigidbody.velocity = velocity.normalized * speed;
		}
	}
}
