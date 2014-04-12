using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Shooting : MonoBehaviour {

	int shotType = 0;
	bool machineGun = false;
	bool chargeShot = false;
	public bool sineShot = false;
	float timer = 0.0f;
	float timeMax = 0.2f;
	Ray ray;
	RaycastHit hit;
	public GameObject prefab;
	GameObject obj;
	GameObject Player;
	GameObject testObj;
	
	// Use this for initialization
	void Start () {
		Player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetKey (KeyCode.Alpha1))
		   {
			shotType = 0;
			}

		if (Input.GetKey (KeyCode.Alpha2)) {
			shotType = 1;
				}

		if (Input.GetKey (KeyCode.Alpha3)) {
			shotType = 2;
		}
		if (Input.GetKey (KeyCode.Alpha4)) {
			shotType = 3;
		}
		timer += Time.deltaTime;

		ray = Camera.main.ScreenPointToRay (Input.mousePosition);

		
		hit = new RaycastHit ();

		switch (shotType) {

			case 0:
				machineGun = false;
				chargeShot = false;
				sineShot = false;
				break;

			case 1:
				machineGun = true;
				chargeShot = false;
				sineShot = false;
				break;
			case 2:
				machineGun = false;
				chargeShot = true;
				sineShot = false;

				break;
			case 3:
				machineGun = false;
				chargeShot = false;
				sineShot = true;
				break;

				}

		if(Physics.Raycast(ray,out hit))
		{

			if(machineGun == false && shotType == 0){
				if(Input.GetMouseButtonDown(0))
				{
					obj = Instantiate(prefab,new Vector3(Player.GetComponent<PlayerMovement>().transform.position.x, Player.GetComponent<PlayerMovement>().transform.position.y ,Player.GetComponent<PlayerMovement>().transform.position.z), Quaternion.identity) as GameObject;
					obj.AddComponent<Shots>();
					obj.GetComponent<Shots>().PassPositions(hit.point, Player.GetComponent<PlayerMovement>().transform.position);

				}
			}

			if(machineGun == true && shotType == 1)
			{
				if(Input.GetKey(KeyCode.Mouse0))
				{

					if (timer >= timeMax){
						obj = Instantiate(prefab,new Vector3(Player.GetComponent<PlayerMovement>().transform.position.x, Player.GetComponent<PlayerMovement>().transform.position.y ,Player.GetComponent<PlayerMovement>().transform.position.z), Quaternion.identity) as GameObject;
						obj.AddComponent<Shots>();
						obj.GetComponent<Shots>().PassPositions(hit.point, Player.GetComponent<PlayerMovement>().transform.position);
						timer = 0;
					}
				
				}
			}

			if(chargeShot == true && shotType == 2)
			{
				if(Input.GetMouseButtonDown(0))
				{
					obj = Instantiate(prefab,new Vector3(Player.GetComponent<PlayerMovement>().transform.position.x, Player.GetComponent<PlayerMovement>().transform.position.y ,Player.GetComponent<PlayerMovement>().transform.position.z), Quaternion.identity) as GameObject;

					obj.AddComponent<Shots>();
					obj.GetComponent<Shots>().PassPositions(hit.point, Player.GetComponent<PlayerMovement>().transform.position);

					
				}
			}

			if(sineShot == true && shotType == 3)
			{
				if(Input.GetMouseButtonDown(0))
				{


					testObj = new GameObject();
					testObj.AddComponent<Shots>();
					testObj.GetComponent<Shots>().sine = sineShot;
					testObj.GetComponent<Shots>().PassPositions(hit.point, Player.GetComponent<PlayerMovement>().transform.position);
					testObj.transform.position = Player.transform.position;

					obj = Instantiate(prefab,new Vector3(Player.GetComponent<PlayerMovement>().transform.position.x, Player.GetComponent<PlayerMovement>().transform.position.y ,Player.GetComponent<PlayerMovement>().transform.position.z), Quaternion.identity) as GameObject;
					obj.transform.parent = testObj.transform;
					Vector3 holder = obj.transform.parent.position;

					obj.transform.position = holder - new Vector3(0.5f,0,0.5f);
					obj.AddComponent<WavyMovement>();
				}
			}


		}
		
		
	}
	
	
}
