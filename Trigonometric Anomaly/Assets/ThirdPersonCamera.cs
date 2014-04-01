using UnityEngine;
using System.Collections;

public class ThirdPersonCamera : MonoBehaviour {
	public enum enumCameraType 
	{
		OrbitingCamera,
		FollowBehindCamera,
		PanningCamera
	}

	public enumCameraType cameraType = enumCameraType.OrbitingCamera;

	public GameObject targetObject;
	private Transform target;

	public float distance_behind_target = 6.0f;
	public float distance_above_target = 2.0f;
	public float distanceForOrbitCamera = 7.0f;
    public float translationRate = 100.0f;
	public bool hPanning = false;
	public bool vPanning = false;
	public float horizontalSpeed = 4.0f;
	public float verticalSpeed = 4.0f;
	public bool lockLookAtCharacter = true;

	// Use this for initialization
	void Start () 
	{
		target = targetObject.transform;
	}
	
	// Update is called once per frame
	void Update () 
	{

	}

	void LateUpdate()
	{
		switch (cameraType) 
		{
			case enumCameraType.OrbitingCamera:
				OrbitingUpdate();
				break;
			case enumCameraType.FollowBehindCamera:
				FollowBehindUpdate();
				break;
			case enumCameraType.PanningCamera:
				PanningUpdate();
				break;
			default:
				break;
		}

		if(target != null && lockLookAtCharacter)
        	this.transform.LookAt(target.position);
	}

	private void PanningUpdate()
	{
		//Rotation transformation
		float hAngle = 0;
		float vAngle = 0;
		
		if (hPanning) 
		{
			hAngle = horizontalSpeed * Input.GetAxis ("Mouse X");
		}
		if (vPanning) 
		{
			vAngle = verticalSpeed * Input.GetAxis ("Mouse Y");
		}

		transform.Rotate (vAngle, hAngle, 0);
	}

	private void FollowBehindUpdate()
	{
		float delta_time = Time.deltaTime;
		
		//Translation transformation
		Vector3 cameraVectZ = (-target.forward * distance_behind_target);
		Vector3 cameraVectY = target.up * distance_above_target;
		
		//Debugging sequence
		Debug.DrawLine(target.position + cameraVectZ, target.position, Color.blue);
		Debug.DrawLine(target.position + cameraVectY, target.position, Color.green);
		Debug.DrawLine(target.position + cameraVectY + cameraVectZ, target.position, Color.cyan);
		
		Vector3 newPosition = Vector3.Lerp(this.transform.position, target.position + cameraVectY + cameraVectZ, delta_time * translationRate);
		
		this.transform.position = newPosition;
	}

	private void OrbitingUpdate()
	{
		//Rotation transformation
		float hAngle = 0;
		float vAngle = 0;
		
		if (hPanning) 
		{
			hAngle = horizontalSpeed * Input.GetAxis ("Mouse X");
		}
		if (vPanning) 
		{
			vAngle = verticalSpeed * Input.GetAxis ("Mouse Y");
		}
		transform.RotateAround (target.position, this.transform.right, vAngle);
		transform.RotateAround (target.position, this.transform.up, hAngle);

		//Zooming in/out
		//Vector3.MoveTowards

	}
}
