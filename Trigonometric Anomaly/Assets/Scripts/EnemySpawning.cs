using UnityEngine;
using System.Collections;

public class EnemySpawning : MonoBehaviour {

	public GameObject prefab;
	GameObject enemySpawner;
	GameObject blues;
	GameObject greens;
	GameObject reds;
	int numberOfEnemies = 10;	//10
	BoxCollider box;
	const float EDGE_CONSTRAINT = 0.5f;
	const float EDGE_SPAWN_CONSTRAINT = 1.5f;

	float elapsedTime = 0.0f;
	float maxTime = 5.0f;	//5.0f

	// Use this for initialization
	void Start () {
		box = GameObject.Find ("BoundingBox").GetComponent<BoxCollider>();
		//correctBoxScale();
		if(GameObject.Find("Enemy Spawner") == null){
			enemySpawner = new GameObject("Enemy Spawner");
			blues = new GameObject("Blues");
			blues.transform.parent = enemySpawner.transform;
			greens = new GameObject("Greens");
			greens.transform.parent = enemySpawner.transform;
			reds = new GameObject("Reds");
			reds.transform.parent = enemySpawner.transform;
		}
		spawnEnemies ();
	}
	
	// Update is called once per frame
	void Update () {
		elapsedTime += Time.deltaTime;
		if(elapsedTime > maxTime){
			spawnEnemies();
			elapsedTime = 0.0f;
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
			}
			enemy.GetComponent<Behaviour>().behaviourInt = enemyType;
		}
	}

	Vector3 getPosition(int enemyLoc){
		Vector3 pos = new Vector3();
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
