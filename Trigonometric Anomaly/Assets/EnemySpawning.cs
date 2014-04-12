using UnityEngine;
using System.Collections;

public class EnemySpawning : MonoBehaviour {

	public GameObject prefab;
	int numberOfEnemies = 10;
	BoxCollider box;

	// Use this for initialization
	void Start () {
		box = GameObject.Find ("BoundingBox").GetComponent<BoxCollider>();
		spawnEnemies ();
	}
	
	// Update is called once per frame
	void Update () {

	}

	void spawnEnemies(){
		for (int i = 0; i < numberOfEnemies; ++i) {
			Vector3 pos = new Vector3(Random.Range(box.bounds.min.x + 0.5f, box.bounds.max.x - 0.5f), 
			                          0,
			                          Random.Range(box.bounds.min.z + 0.5f, box.bounds.max.z - 0.5f));
			GameObject enemy = Instantiate (prefab, pos, Quaternion.identity) as GameObject;

			//Random number between 0 and number of enemy types - 1 (to account for NONE)
			int enemyType = Random.Range (0, System.Enum.GetNames(typeof(EnumScript.EnemyType)).Length - 1);
			enemy.GetComponent<Behaviour>().behaviourInt = enemyType;
		}
	}
}
