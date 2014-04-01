using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
	public float speed = 0.5f;
	public bool hMouseMovement = false;
	public bool vMouseMovement = false;
	public float horizontalSpeed = 4.0f;
	public float verticalSpeed = 4.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Rotation transformation
		float hAngle = 0;
		float vAngle = 0;

		if (hMouseMovement) 
		{
			hAngle = horizontalSpeed * Input.GetAxis ("Mouse X");
		}
		if (vMouseMovement) 
		{
			vAngle = verticalSpeed * Input.GetAxis ("Mouse Y");
		}
		transform.Rotate (vAngle, hAngle, 0);

		//Translation transformation
		if (Input.GetKey (KeyCode.W))
		{
			this.transform.Translate(new Vector3(0f, 0f, speed));
		}
		if (Input.GetKey (KeyCode.A))
		{
			this.transform.Translate(new Vector3(-speed, 0f, 0f));
		}
		if (Input.GetKey (KeyCode.S))
		{
			this.transform.Translate(new Vector3(0f, 0f, -speed));
		}
		if (Input.GetKey (KeyCode.D))
		{
			this.transform.Translate(new Vector3(speed, 0f, 0f));	
		}
	}
}
