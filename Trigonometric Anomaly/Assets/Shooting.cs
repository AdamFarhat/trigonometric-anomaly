using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Shooting : MonoBehaviour {

	//Used for Bullet Types
	int shotType = 0;
	int MachineGun;
	int wavyMachineGun;
	bool machineGun = false;
	bool chargeShot = false;
	public bool sineShot = false;
	float timer = 0.0f;
	float timeInterval = 0.2f;

	//For Direction of Bullet
	Ray ray;
	RaycastHit hit;


	//Player and other GameObjects
	public GameObject prefab;
	GameObject obj;
	GameObject Player;
	GameObject parentObj;
	
	// Use this for initialization
	void Start () {
		MachineGun = 0;
		wavyMachineGun = 0;
		Player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {

		print(wavyMachineGun);
        //Single Shot
		if(Input.GetKeyUp (KeyCode.Alpha1))
		   {
			MachineGun = 1 - MachineGun;
			shotType = 0;
			}

//        //Charge Shot (Not Implemented Yet)
//		if (Input.GetKey (KeyCode.Alpha3)) {
//			shotType = 2;
//		}

        //Wavy Shot
		if (Input.GetKeyUp (KeyCode.Alpha2)) {
			wavyMachineGun = 1 - wavyMachineGun;
			shotType = 1;
		}

		timer += Time.deltaTime;

		ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		
		hit = new RaycastHit ();

		if(Physics.Raycast(ray,out hit))
		{
			if(shotType == 0)
			{
			if(MachineGun == 0){
				if(Input.GetMouseButtonDown(0))
				{
					obj = Instantiate(prefab,new Vector3(Player.GetComponent<PlayerMovement>().transform.position.x, Player.GetComponent<PlayerMovement>().transform.position.y ,Player.GetComponent<PlayerMovement>().transform.position.z), Quaternion.identity) as GameObject;
					obj.AddComponent<Shots>();
					obj.GetComponent<Shots>().PassPositions(hit.point, Player.GetComponent<PlayerMovement>().transform.position);

				}
			}
			else if(MachineGun == 1)
			{
				if(Input.GetKey(KeyCode.Mouse0))
				{
					if (timer >= timeInterval){
						obj = Instantiate(prefab,new Vector3(Player.GetComponent<PlayerMovement>().transform.position.x, Player.GetComponent<PlayerMovement>().transform.position.y ,Player.GetComponent<PlayerMovement>().transform.position.z), Quaternion.identity) as GameObject;
						obj.AddComponent<Shots>();
						obj.GetComponent<Shots>().PassPositions(hit.point, Player.GetComponent<PlayerMovement>().transform.position);
						timer = 0;
					}
				
				}
			}
			}

//			if(chargeShot == true && shotType == 2)
//			{
//				if(Input.GetMouseButtonDown(0))
//				{
//					obj = Instantiate(prefab,new Vector3(Player.GetComponent<PlayerMovement>().transform.position.x, Player.GetComponent<PlayerMovement>().transform.position.y ,Player.GetComponent<PlayerMovement>().transform.position.z), Quaternion.identity) as GameObject;
//					obj.AddComponent<Shots>();
//					obj.GetComponent<Shots>().PassPositions(hit.point, Player.GetComponent<PlayerMovement>().transform.position);
//
//					
//				}
//			}
			if(shotType == 1){
				if(wavyMachineGun == 0)
				{
					if(Input.GetMouseButtonDown(0))
					{
                   	 //Create empty Object for bullet to follow
						parentObj = new GameObject();
						parentObj.AddComponent<Shots>();
						parentObj.GetComponent<Shots>().sine = sineShot;
						parentObj.GetComponent<Shots>().PassPositions(hit.point, Player.GetComponent<PlayerMovement>().transform.position);
						parentObj.transform.position = Player.transform.position;

                   	 //Create the Bullet, set the parent to the Empty Object and offset the position to allow for Wavy-like movement
						obj = Instantiate(prefab,new Vector3(Player.GetComponent<PlayerMovement>().transform.position.x, Player.GetComponent<PlayerMovement>().transform.position.y ,Player.GetComponent<PlayerMovement>().transform.position.z), Quaternion.identity) as GameObject;
						obj.transform.parent = parentObj.transform;
						Vector3 holder = obj.transform.parent.position;

						obj.transform.position = holder - new Vector3(0.75f,0,0.75f);
						obj.AddComponent<WavyMovement>();
					}
				}
				else if(wavyMachineGun == 1)
				{
					if(Input.GetKey(KeyCode.Mouse0))
						{
							if (timer >= timeInterval){
								//Create empty Object for bullet to follow
								parentObj = new GameObject();
								parentObj.AddComponent<Shots>();
								parentObj.GetComponent<Shots>().sine = sineShot;
								parentObj.GetComponent<Shots>().PassPositions(hit.point, Player.GetComponent<PlayerMovement>().transform.position);
								parentObj.transform.position = Player.transform.position;
						
								//Create the Bullet, set the parent to the Empty Object and offset the position to allow for Wavy-like movement
								obj = Instantiate(prefab,new Vector3(Player.GetComponent<PlayerMovement>().transform.position.x, Player.GetComponent<PlayerMovement>().transform.position.y ,Player.GetComponent<PlayerMovement>().transform.position.z), Quaternion.identity) as GameObject;
								obj.transform.parent = parentObj.transform;
								Vector3 holder = obj.transform.parent.position;
						
								obj.transform.position = holder - new Vector3(0.75f,0,0.75f);
								obj.AddComponent<WavyMovement>();
								timer = 0;
						}
					}

				}

			}
			


		}
		
		
	}
	
	
}
