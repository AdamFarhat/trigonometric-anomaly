using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Shooting : MonoBehaviour {
	
	AudioClip Trigger;
	//Used for Bullet Types
	int shotType = 0;
	int MachineGun;
	int wavyMachineGun;
	int doubleWavyMachineGun;
	int homingGun;
	bool machineGun = false;
	bool chargeShot = false;
	public bool sineShot = false;
	public bool tripleShotPurchased = false;
	public bool spiralShotPurchased = false;
	public bool hasAlly = false;
	float timer = 0.0f;
	float timeInterval = 0.2f;
	int score;
	public ScoreController scoreController = ScoreController.Instance;
	public int numberBombs;
	public GameObject bombPrefab;
	public GameObject allyPrefab;
	public GameObject shieldPrefab;
	public bool allySet;
	public int nbAlly;
	public bool shieldSet;
	public bool hasShield;
	
	//For Direction of Bullet
	Ray ray;
	RaycastHit hit;
	
	
	//Player and other GameObjects
	public GameObject prefab;
	GameObject obj;
	GameObject obj2;
	GameObject obj3;
	GameObject ally1;
	GameObject ally2;
	GameObject ally3;
	GameObject Player;
	GameObject shield;
	GameObject parentObj;

	GameObject shotHierarchy;
	
	Vector3 explosionPosition;
	
	float explosionRadius = 25.0f;
	
	Collider[] colliders;
	
	void Awake()
	{
		scoreController = ScoreController.Instance;
	}
	// Use this for initialization
	void Start () 
	{
		shotHierarchy = new GameObject("ShotList");

		hasShield = false;
		allySet = false;
		shieldSet = false;
		numberBombs = 0;
		MachineGun = 0;
		wavyMachineGun = 0;
		doubleWavyMachineGun = 0;
		homingGun = 0;
		Player = GameObject.FindGameObjectWithTag("Player");
	}
	
	
	
	// Update is called once per frame
	void Update () {

		if (hasShield == true)
				{
			createShield();
				}
		if (nbAlly == 0)
		{
			hasAlly = false;
			allySet = false;
		}

		explosionPosition = Player.transform.position;
		colliders = Physics.OverlapSphere(explosionPosition, explosionRadius);
		if ( Time.timeScale <= 0 ) return;
		//Single Shot
		if(Input.GetKeyUp (KeyCode.Alpha1))
		{
			MachineGun = 1 - MachineGun;
			shotType = 0;
		}
		
		
		//Wavy Shot
		if (Input.GetKeyUp (KeyCode.Alpha2)) {
			wavyMachineGun = 1 - wavyMachineGun;
			shotType = 1;
		}
		
		//Double Wavy Shot
		if (Input.GetKeyUp (KeyCode.Alpha3)) {
			doubleWavyMachineGun = 1 - doubleWavyMachineGun;
			shotType = 2;
		}
		//Homing Shot
		if (Input.GetKeyUp (KeyCode.Alpha4)) {
			homingGun = 1 - homingGun;
			shotType = 3;
		}
		
		if (Input.GetKeyUp(KeyCode.Space))
		{
			if(numberBombs > 0)
			{
				numberBombs--;
				Bomb();
				Instantiate(bombPrefab, explosionPosition, Quaternion.identity);
			}
		}
		
		
		
		
		
		timer += Time.deltaTime;
		
		ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		
		hit = new RaycastHit ();
		
		if(Physics.Raycast(ray,out hit))
		{
			if(shotType == 0)
			{
				if(MachineGun == 0){
					if(Input.GetMouseButtonDown(0)== true)
					{
						gameObject.GetComponent<AudioSource>().Play();
						obj = Instantiate(prefab,new Vector3(Player.GetComponent<PlayerMovement>().transform.position.x, Player.GetComponent<PlayerMovement>().transform.position.y ,Player.GetComponent<PlayerMovement>().transform.position.z), Quaternion.identity) as GameObject;
						obj.AddComponent<Shots>();
						obj.GetComponent<Shots>().PassPositions(hit.point, Player.GetComponent<PlayerMovement>().transform.position);
						obj.transform.parent = shotHierarchy.transform;
						
						
					}
				}
				else if(MachineGun == 1)
				{
					if(Input.GetKey(KeyCode.Mouse0))
					{
						if (timer >= timeInterval){
							gameObject.GetComponent<AudioSource>().Play();
							obj = Instantiate(prefab,new Vector3(Player.GetComponent<PlayerMovement>().transform.position.x, Player.GetComponent<PlayerMovement>().transform.position.y ,Player.GetComponent<PlayerMovement>().transform.position.z), Quaternion.identity) as GameObject;
							obj.AddComponent<Shots>();
							obj.GetComponent<Shots>().PassPositions(hit.point, Player.GetComponent<PlayerMovement>().transform.position);
							obj.transform.parent = shotHierarchy.transform;
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
				if(wavyMachineGun == 0 && tripleShotPurchased)
				{
					if(Input.GetMouseButtonDown(0))
					{
						//						gameObject.GetComponent<AudioSource>().Play();
						//						//Create empty Object for bullet to follow
						//						parentObj = new GameObject();
						//						parentObj.AddComponent<Shots>();
						//						parentObj.GetComponent<Shots>().sine = sineShot;
						//						parentObj.GetComponent<Shots>().PassPositions(hit.point, Player.GetComponent<PlayerMovement>().transform.position);
						//						parentObj.transform.position = Player.transform.position;
						//						
						//						//Create the Bullet, set the parent to the Empty Object and offset the position to allow for Wavy-like movement
						//						obj = Instantiate(prefab,new Vector3(Player.GetComponent<PlayerMovement>().transform.position.x, Player.GetComponent<PlayerMovement>().transform.position.y ,Player.GetComponent<PlayerMovement>().transform.position.z), Quaternion.identity) as GameObject;
						//						obj.transform.parent = parentObj.transform;
						//						Vector3 holder = obj.transform.parent.position;
						//						
						//						obj.transform.position = holder - new Vector3(1.5f, 0, 0.25f);
						//						obj.AddComponent<WavyMovement>();
						//						obj.GetComponent<WavyMovement>().isSingle = true;
						
						gameObject.GetComponent<AudioSource>().Play();
						//Create empty Object for bullet to follow
						parentObj = new GameObject();
						parentObj.AddComponent<Shots>();
						//parentObj.GetComponent<Shots>().sine = sineShot;
						parentObj.GetComponent<Shots>().PassPositions(hit.point, Player.GetComponent<PlayerMovement>().transform.position);
						parentObj.transform.position = Player.transform.position;
						
						//Create the Bullet, set the parent to the Empty Object and offset the position to allow for Wavy-like movement
						obj = Instantiate(prefab,new Vector3(Player.GetComponent<PlayerMovement>().transform.position.x, Player.GetComponent<PlayerMovement>().transform.position.y ,Player.GetComponent<PlayerMovement>().transform.position.z), Quaternion.identity) as GameObject;
						obj2 = Instantiate(prefab,new Vector3(Player.GetComponent<PlayerMovement>().transform.position.x, Player.GetComponent<PlayerMovement>().transform.position.y ,Player.GetComponent<PlayerMovement>().transform.position.z), Quaternion.identity) as GameObject;
						obj3 = Instantiate(prefab,new Vector3(Player.GetComponent<PlayerMovement>().transform.position.x, Player.GetComponent<PlayerMovement>().transform.position.y ,Player.GetComponent<PlayerMovement>().transform.position.z), Quaternion.identity) as GameObject;

						obj.transform.parent = parentObj.transform;
						obj2.transform.parent = parentObj.transform;
						obj3.transform.parent = parentObj.transform;
						parentObj.transform.parent = shotHierarchy.transform;

						Vector3 holder = obj.transform.parent.position;
						Vector3 holder1 = obj2.transform.parent.position;
						Vector3 holder2 = obj2.transform.parent.position;
						
						obj.transform.position = holder - new Vector3(0.75f,0,0.75f);
						obj2.transform.position = holder1 + new Vector3(0.75f,0,0.75f);
						obj3.transform.position = holder2;
						
						obj.AddComponent<Shots>();
						obj2.AddComponent<Shots>();
						obj3.AddComponent<Shots>();
						
					}
				}
				else if(wavyMachineGun == 1 && tripleShotPurchased)
				{
					if(Input.GetKey(KeyCode.Mouse0))
					{
						if (timer >= timeInterval){
							//							gameObject.GetComponent<AudioSource>().Play();
							//							//Create empty Object for bullet to follow
							//							parentObj = new GameObject();
							//							parentObj.AddComponent<Shots>();
							//							parentObj.GetComponent<Shots>().sine = sineShot;
							//							parentObj.GetComponent<Shots>().PassPositions(hit.point, Player.GetComponent<PlayerMovement>().transform.position);
							//							parentObj.transform.position = Player.transform.position;
							//							
							//							//Create the Bullet, set the parent to the Empty Object and offset the position to allow for Wavy-like movement
							//							obj = Instantiate(prefab,new Vector3(Player.GetComponent<PlayerMovement>().transform.position.x, Player.GetComponent<PlayerMovement>().transform.position.y ,Player.GetComponent<PlayerMovement>().transform.position.z), Quaternion.identity) as GameObject;
							//							obj.transform.parent = parentObj.transform;
							//							Vector3 holder = obj.transform.parent.position;
							//							
							//							obj.transform.position = holder - new Vector3(0.75f,0,0.75f);
							//							obj.AddComponent<WavyMovement>();
							//							obj.GetComponent<WavyMovement>().isSingle = true;
							
							gameObject.GetComponent<AudioSource>().Play();
							//Create empty Object for bullet to follow
							parentObj = new GameObject();
							parentObj.AddComponent<Shots>();
							//parentObj.GetComponent<Shots>().sine = sineShot;
							parentObj.GetComponent<Shots>().PassPositions(hit.point, Player.GetComponent<PlayerMovement>().transform.position);
							parentObj.transform.position = Player.transform.position;
							
							//Create the Bullet, set the parent to the Empty Object and offset the position to allow for Wavy-like movement
							obj = Instantiate(prefab,new Vector3(Player.GetComponent<PlayerMovement>().transform.position.x, Player.GetComponent<PlayerMovement>().transform.position.y ,Player.GetComponent<PlayerMovement>().transform.position.z), Quaternion.identity) as GameObject;
							obj2 = Instantiate(prefab,new Vector3(Player.GetComponent<PlayerMovement>().transform.position.x, Player.GetComponent<PlayerMovement>().transform.position.y ,Player.GetComponent<PlayerMovement>().transform.position.z), Quaternion.identity) as GameObject;
							obj3 = Instantiate(prefab,new Vector3(Player.GetComponent<PlayerMovement>().transform.position.x, Player.GetComponent<PlayerMovement>().transform.position.y ,Player.GetComponent<PlayerMovement>().transform.position.z), Quaternion.identity) as GameObject;

							obj.transform.parent = parentObj.transform;
							obj2.transform.parent = parentObj.transform;
							obj3.transform.parent = parentObj.transform;
							parentObj.transform.parent = shotHierarchy.transform;

							Vector3 holder = obj.transform.parent.position;
							Vector3 holder1 = obj2.transform.parent.position;
							Vector3 holder2 = obj2.transform.parent.position;
							
							obj.transform.position = holder - new Vector3(0.75f,0,0.75f);
							obj2.transform.position = holder1 + new Vector3(0.75f,0,0.75f);
							obj3.transform.position = holder2;
							
							obj.AddComponent<Shots>();
							obj2.AddComponent<Shots>();
							obj3.AddComponent<Shots>();
							
							timer = 0;
						}
					}
					
				}
				
			}
			if(shotType == 2){
				if(doubleWavyMachineGun == 0 && spiralShotPurchased)
				{
					if(Input.GetMouseButtonDown(0))
					{
						gameObject.GetComponent<AudioSource>().Play();
						//Create empty Object for bullet to follow
						parentObj = new GameObject();
						parentObj.AddComponent<Shots>();
						parentObj.GetComponent<Shots>().sine = sineShot;
						parentObj.GetComponent<Shots>().PassPositions(hit.point, Player.GetComponent<PlayerMovement>().transform.position);
						parentObj.transform.position = Player.transform.position;
						
						//Create the Bullet, set the parent to the Empty Object and offset the position to allow for Wavy-like movement
						obj = Instantiate(prefab,new Vector3(Player.GetComponent<PlayerMovement>().transform.position.x, Player.GetComponent<PlayerMovement>().transform.position.y ,Player.GetComponent<PlayerMovement>().transform.position.z), Quaternion.identity) as GameObject;
						obj2 = Instantiate(prefab,new Vector3(Player.GetComponent<PlayerMovement>().transform.position.x, Player.GetComponent<PlayerMovement>().transform.position.y ,Player.GetComponent<PlayerMovement>().transform.position.z), Quaternion.identity) as GameObject;

						obj.transform.parent = parentObj.transform;
						obj2.transform.parent = parentObj.transform;
						parentObj.transform.parent = shotHierarchy.transform;

						Vector3 holder = obj.transform.parent.position;
						Vector3 holder1 = obj2.transform.parent.position;
						
						obj.transform.position = holder - new Vector3(0.75f,0,0.75f);
						obj2.transform.position = holder1 + new Vector3(0.75f,0,0.75f);
						obj.AddComponent<WavyMovement>();
						obj.GetComponent<WavyMovement>().isSingle = false;
						obj2.AddComponent<WavyMovement>();
						obj2.GetComponent<WavyMovement>().isSingle = false;
					}
				}
				else if(doubleWavyMachineGun == 1 && spiralShotPurchased)
				{
					if(Input.GetKey(KeyCode.Mouse0))
					{
						if (timer >= timeInterval){
							gameObject.GetComponent<AudioSource>().Play();
							//Create empty Object for bullet to follow
							parentObj = new GameObject();
							parentObj.AddComponent<Shots>();
							parentObj.GetComponent<Shots>().sine = sineShot;
							parentObj.GetComponent<Shots>().PassPositions(hit.point, Player.GetComponent<PlayerMovement>().transform.position);
							parentObj.transform.position = Player.transform.position;
							
							//Create the Bullet, set the parent to the Empty Object and offset the position to allow for Wavy-like movement
							obj = Instantiate(prefab,new Vector3(Player.GetComponent<PlayerMovement>().transform.position.x, Player.GetComponent<PlayerMovement>().transform.position.y ,Player.GetComponent<PlayerMovement>().transform.position.z), Quaternion.identity) as GameObject;
							obj2 = Instantiate(prefab,new Vector3(Player.GetComponent<PlayerMovement>().transform.position.x, Player.GetComponent<PlayerMovement>().transform.position.y ,Player.GetComponent<PlayerMovement>().transform.position.z), Quaternion.identity) as GameObject;

							obj.transform.parent = parentObj.transform;
							obj2.transform.parent = parentObj.transform;
							parentObj.transform.parent = shotHierarchy.transform;

							Vector3 holder = obj.transform.parent.position;
							Vector3 holder1 = obj2.transform.parent.position;
							
							obj.transform.position = holder - new Vector3(0.75f,0,0.75f);
							obj2.transform.position = holder1 + new Vector3(0.75f,0,0.75f);
							obj.AddComponent<WavyMovement>();
							obj2.AddComponent<WavyMovement>();
							obj.GetComponent<WavyMovement>().isSingle = false;
							obj2.GetComponent<WavyMovement>().isSingle = false;
							timer = 0;
						}
					}
					
				}
				
			}
			
			//			if(shotType == 3)
			//			{
			//				if(homingGun == 0){
			//					if(Input.GetMouseButtonDown(0)== true)
			//					{
			//						gameObject.GetComponent<AudioSource>().Play();
			//						obj = Instantiate(prefab,new Vector3(Player.GetComponent<PlayerMovement>().transform.position.x, Player.GetComponent<PlayerMovement>().transform.position.y ,Player.GetComponent<PlayerMovement>().transform.position.z), Quaternion.identity) as GameObject;
			//						obj.AddComponent<HomingShot>();
			//						obj.GetComponent<HomingShot>().PassPositions(hit.point, Player.GetComponent<PlayerMovement>().transform.position);
			//						
			//						
			//					}
			//				}
			//				else if(homingGun == 1)
			//				{
			//					if(Input.GetKey(KeyCode.Mouse0))
			//					{
			//						if (timer >= timeInterval){
			//							gameObject.GetComponent<AudioSource>().Play();
			//							obj = Instantiate(prefab,new Vector3(Player.GetComponent<PlayerMovement>().transform.position.x, Player.GetComponent<PlayerMovement>().transform.position.y ,Player.GetComponent<PlayerMovement>().transform.position.z), Quaternion.identity) as GameObject;
			//							obj.AddComponent<HomingShot>();
			//							obj.GetComponent<HomingShot>().PassPositions(hit.point, Player.GetComponent<PlayerMovement>().transform.position);
			//							
			//							timer = 0;
			//						}
			//						
			//					}
			//				}
			//			}
			
			
			
		}
		
		
	}
	
	
	void Bomb()
	{
		foreach (Collider col in colliders)
		{
			
			if (col.collider.tag == "Enemy")
			{

				int enemyType = col.collider.gameObject.GetComponent<Behaviour>().behaviourInt;

				switch(enemyType)
				{
					case 0: 
						ScoreController.Instance.addScore(1000);
						break;
					case 1:
						ScoreController.Instance.addScore(2000);
						break;
					case 2:
						ScoreController.Instance.addScore(3000);
						break;

				}

				Destroy(col.collider.gameObject);
				
			}
			if(col.collider.gameObject.layer == 10)
			{
				Destroy(col.collider.gameObject);
				
			}
			if(col.collider.gameObject.layer == 8)
			{
				Destroy(col.collider.gameObject);
			}
			
		}
		
	}

	void createShield()
	{

		shield = Instantiate(shieldPrefab,new Vector3(Player.GetComponent<PlayerMovement>().transform.position.x, Player.GetComponent<PlayerMovement>().transform.position.y ,Player.GetComponent<PlayerMovement>().transform.position.z), Quaternion.identity) as GameObject;
		shield.transform.parent = Player.GetComponent<PlayerMovement>().transform;

		shieldSet = true;
	}
	public void createAlly()
	{
		if (hasAlly == false)
		{
			if(allySet != true)
			{
				ally1 = Instantiate(allyPrefab,new Vector3(Player.GetComponent<PlayerMovement>().transform.position.x, Player.GetComponent<PlayerMovement>().transform.position.y ,Player.GetComponent<PlayerMovement>().transform.position.z), Quaternion.identity) as GameObject;
				ally1.transform.parent = Player.GetComponent<PlayerMovement>().transform;
				ally2 = Instantiate(allyPrefab,new Vector3(Player.GetComponent<PlayerMovement>().transform.position.x, Player.GetComponent<PlayerMovement>().transform.position.y ,Player.GetComponent<PlayerMovement>().transform.position.z), Quaternion.identity) as GameObject;
				ally2.transform.parent = Player.GetComponent<PlayerMovement>().transform;
				ally3 = Instantiate(allyPrefab,new Vector3(Player.GetComponent<PlayerMovement>().transform.position.x, Player.GetComponent<PlayerMovement>().transform.position.y ,Player.GetComponent<PlayerMovement>().transform.position.z), Quaternion.identity) as GameObject;
				ally3.transform.parent = Player.GetComponent<PlayerMovement>().transform;
				
				Vector3 holder = ally1.transform.parent.position;
				Vector3 holder1 = ally2.transform.parent.position;
				Vector3 holder2 = ally3.transform.parent.position;
				
				ally1.transform.position = holder - new Vector3(3.5f,0,3.5f);
				ally2.transform.position = holder1 + new Vector3(3.5f,0,-3.5f);
				ally3.transform.position = holder2 + new Vector3(0,0,3.5f);
				allySet = true;
			}
			hasAlly = true;
		}
		
		nbAlly = 3;
		
	}

	public void setShield()
	{
		if (shieldSet != true)
			createShield();
	
		shieldSet = true;
	}
	
	
	//	void OnTriggerEnter (Collider other) {
	//
	//						
	//			
	//		}
	
}
