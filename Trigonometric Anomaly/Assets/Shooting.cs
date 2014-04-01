using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Shooting : MonoBehaviour {
	
	
	Ray ray;
	RaycastHit hit;
	public GameObject prefab;
	GameObject obj;
	GameObject Player;
	
	// Use this for initialization
	void Start () {
		
		Player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		
		ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		
		//ray=Camera.main.ScreenPointToRay(Input.mousePosition);
		
		hit = new RaycastHit ();
		
		
		if(Physics.Raycast(ray,out hit))
		{
			
			if(Input.GetMouseButtonDown(0))
			{
				obj = Instantiate(prefab,new Vector3(Player.GetComponent<PlayerMovement>().transform.position.x, Player.GetComponent<PlayerMovement>().transform.position.y ,Player.GetComponent<PlayerMovement>().transform.position.z), Quaternion.identity) as GameObject;
				obj.AddComponent<Shots>();
				obj.GetComponent<Shots>().PassPositions(hit.point, Player.GetComponent<PlayerMovement>().transform.position);

			}
			
		}
		
		
	}
	
	
}
