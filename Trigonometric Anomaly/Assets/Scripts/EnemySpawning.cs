using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawning : MonoBehaviour {
	
	public GameObject prefab;
	public GameObject prefab_child;
	GameObject enemySpawner;
	GameObject blues;
	GameObject greens;
	GameObject reds;
	GameObject yellows;
	private GameObject enemiesHierarchy;
	int numberOfEnemies = 10;	//10
	BoxCollider box;
	BoxCollider activeBox;
	const float EDGE_CONSTRAINT = 0.5f;
	const float EDGE_SPAWN_CONSTRAINT = 1.5f;
	
	float elapsedTime = 0.0f;
	float maxTime = 10.0f;	//5.0f

	
	
	// Use this for initialization
	void Start () 
	{
		ScoreController.Instance.EnemyWaveLength = numberOfEnemies;
		box = GameObject.Find ("BoundingBox").GetComponent<BoxCollider>();
		activeBox = GameObject.Find("ActiveBox").GetComponent<BoxCollider>();
		//correctBoxScale();
		if(GameObject.Find("Enemy Spawner") == null){
			enemySpawner = new GameObject("Enemy Spawer");

			blues = new GameObject("Blues");
			blues.transform.parent = enemySpawner.transform;

			greens = new GameObject("Greens");
			greens.transform.parent = enemySpawner.transform;

			reds = new GameObject("Reds");
			reds.transform.parent = enemySpawner.transform;
			yellows = new GameObject("Yellows");
			yellows.transform.parent = enemySpawner.transform;
			enemySpawner.transform.parent = GameObject.Find("_Spawners").transform;
		}
		spawnEnemies ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		elapsedTime += Time.deltaTime;
		if(elapsedTime > maxTime){
			numberOfEnemies = (int)(numberOfEnemies * 1.1);
			ScoreController.Instance.EnemyWaveLength = numberOfEnemies;
			spawnEnemies();
			destroyFarEnemies();
			elapsedTime = 0.0f;
		}


//		destroyFarEnemies();
//		if(ScoreController.Instance.EnemyKillCount >= ScoreController.Instance.EnemyWaveLength)
//		{//new wave
//			numberOfEnemies = (int)(numberOfEnemies * 1.2);
//			ScoreController.Instance.EnemyWaveLength = numberOfEnemies;
//			//destroyFarEnemies();
//			spawnEnemies();
//		}
		
	}
	
	void destroyFarEnemies(){
		GameObject [] enemies = GameObject.FindGameObjectsWithTag("Enemy");
		foreach (GameObject e in enemies){
			if(!e.collider.bounds.Intersects(activeBox.bounds))
			{
				Destroy(e);

//			//ADAM:
//				int enemyLoc = Random.Range(0,4);
//				Vector3 pos = getPosition(enemyLoc);
//				e.transform.position = pos;
			}
		}
		
	}
	
	void correctBoxScale(){
		float ratio = (float)Screen.width / (float)Screen.height;	
		if(ratio > 1.7f){
			box.transform.localScale = new Vector3(box.transform.localScale.x * 1.6f, 
			                                       box.transform.localScale.y, 
			                                       box.transform.localScale.z * 0.9f);
		}
		
	}
	
	void spawnEnemies(){
		for (int i = 0; i < numberOfEnemies; ++i) {
			int enemyLoc = Random.Range(0,4);
			Vector3 pos = getPosition(enemyLoc);			
			GameObject enemy = Instantiate (prefab, pos, Quaternion.identity) as GameObject;
			
			//Random number between 0 and number of enemy types - 1 (to account for NONE)
			int enemyType = Random.Range (0, System.Enum.GetNames(typeof(EnumScript.EnemyType)).Length - 1);		
			switch(enemyType){
			case 0:		//BLUE
				enemy.transform.parent = blues.transform;
				break;
			case 1:		//GREEN
				enemy.transform.parent = greens.transform;					
				break;
			case 2:		//RED
				enemy.transform.parent = reds.transform;	
				break;
			case 3:		//YELLOW
				//create line of enemies
				foreach(GameObject child in createEnemyLine(enemy)){
					child.transform.parent = enemy.transform;
					child.AddComponent<Behaviour>();
					
				}
				enemy.transform.parent = yellows.transform;
				break;
			}
			enemy.GetComponent<Behaviour>().behaviourInt = enemyType;
		}
	}
	
	GameObject[] createEnemyLine(GameObject head){
		int lineLength = 16;
		float distanceBetween = 2.0f;
		GameObject[] enemyLine = new GameObject[lineLength];
		float angle = 0;
		for(int i = 0; i < lineLength; ++i){
			Vector3 pos_child;
			float x = 0, y = 0, z = 0;
			if(i == 0){			
				angle = 180 * Mathf.Deg2Rad;
				x = Mathf.Round (head.transform.position.x + (distanceBetween * Mathf.Sin (angle)));
				y = head.transform.position.y;
				z = Mathf.Round (head.transform.position.z + (distanceBetween * Mathf.Cos(angle)));
			}else{
				float angleBounds = 0.3f;
				angle = Random.Range (angle  - angleBounds, angle + angleBounds);
				x = Mathf.Round (enemyLine[i-1].transform.position.x + (distanceBetween * Mathf.Sin (angle)));
				y = enemyLine[i-1].transform.position.y;
				z = Mathf.Round (enemyLine[i-1].transform.position.z + (distanceBetween * Mathf.Cos(angle)));
			}
			
			pos_child = new Vector3(x,y,z);
			enemyLine[i] = Instantiate (prefab_child, pos_child, Quaternion.identity) as GameObject;
			enemyLine[i].transform.LookAt(head.transform);
			if(i == 0 || i == 1){
				enemyLine[i].renderer.enabled = false;
				enemyLine[i].GetComponent<BoxCollider>().enabled = false;
			}
		}
		return enemyLine;
	}

	Vector3 getPosition(int enemyLoc){
		Vector3 pos = new Vector3();	//()
		switch(enemyLoc){
		case 0:		//north
			pos = new Vector3(Random.Range(box.bounds.min.x + EDGE_CONSTRAINT, box.bounds.max.x - EDGE_CONSTRAINT), 
			                  0,
			                  Random.Range(box.bounds.max.z - EDGE_SPAWN_CONSTRAINT, box.bounds.max.z - EDGE_CONSTRAINT));
			break;
		case 1:		//west
			pos = new Vector3(Random.Range(box.bounds.min.x + EDGE_CONSTRAINT, box.bounds.min.x + EDGE_SPAWN_CONSTRAINT),
			                  0,
			                  Random.Range(box.bounds.min.z + EDGE_CONSTRAINT, box.bounds.max.z - EDGE_CONSTRAINT));
			break;
		case 2:		//east
			pos = new Vector3(Random.Range(box.bounds.max.x - EDGE_CONSTRAINT, box.bounds.max.x - EDGE_SPAWN_CONSTRAINT),
			                  0,
			                  Random.Range(box.bounds.min.z + EDGE_CONSTRAINT, box.bounds.max.z - EDGE_CONSTRAINT));
			break;
		case 3:		//south
			pos = new Vector3(Random.Range(box.bounds.min.x + EDGE_CONSTRAINT, box.bounds.max.x - EDGE_CONSTRAINT), 
			                  0,
			                  Random.Range(box.bounds.min.z + EDGE_CONSTRAINT, box.bounds.min.z + EDGE_SPAWN_CONSTRAINT));
			break;
		}
		return pos;
	}
}
