using UnityEngine;
using System.Collections;

public class FollowCamera : MonoBehaviour 
{
	GameObject camera;
	
	void Start () 
	{
		camera = GameObject.FindGameObjectWithTag("MainCamera");
	}
	
	// Update is called once per frame
	void Update () 
	{
		
		transform.position = camera.transform.position + new Vector3(0,-50,0);
	}
}
