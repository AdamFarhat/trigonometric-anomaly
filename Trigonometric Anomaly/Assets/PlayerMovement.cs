﻿using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
	[SerializeField] private float maxSpeed = 0.5f;
	[SerializeField] private float maxAngularVelocity = 10f;
	[SerializeField] public Vector3 currentVelocity = Vector3.zero;
	[SerializeField] private Vector3 currentDirection = Vector3.zero;
	[SerializeField] private float mass = 10.0f;
	[SerializeField] Vector3 steering = Vector3.zero;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		float delta_time = Time.deltaTime;
		Vector3 myPosition = this.transform.position;
		Vector3 targetPosition = this.transform.position;

		//Translation transformation
		if (Input.GetKey (KeyCode.W))
		{
			targetPosition += new Vector3(0f, 0f, 1f);
		}
		if (Input.GetKey (KeyCode.A))
		{
			targetPosition += new Vector3(-1f, 0f, 0f);
		}
		if (Input.GetKey (KeyCode.S))
		{
			targetPosition += new Vector3(0f, 0f, -1f);
		}
		if (Input.GetKey (KeyCode.D))
		{
			targetPosition += new Vector3(1f, 0f, 0f);
		}

		Vector3 direction = targetPosition - myPosition;

		direction.Normalize();

		if(targetPosition != myPosition)
		{
			this.transform.rotation = Quaternion.Slerp (this.transform.rotation, Quaternion.LookRotation (direction), delta_time * maxAngularVelocity);
			currentDirection = direction;
		}

		direction *= maxSpeed;
		steering = direction - currentVelocity;

		Debug.DrawLine(myPosition, myPosition + this.transform.forward * 5f, Color.black);
		Debug.DrawLine(myPosition, direction, Color.red);
		Debug.DrawLine(myPosition, steering, Color.green);

		currentVelocity += steering/mass;

		Vector3 newPosition = myPosition + currentVelocity * delta_time;
		this.transform.position = newPosition;
	}
}