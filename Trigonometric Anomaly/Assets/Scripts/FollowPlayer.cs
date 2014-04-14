using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour {	
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		this.transform.position = PlayerMovement.Instance.position;
		this.transform.rotation = PlayerMovement.Instance.transform.rotation;
	}
}
