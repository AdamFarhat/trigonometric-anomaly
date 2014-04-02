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
			Vector3 pos = new Vector3(Random.Range(box.bounds.min.x, box.bounds.max.x), 
			                          0,
			                          Random.Range(box.bounds.min.z, box.bounds.max.z));
			GameObject enemy = Instantiate (prefab, pos, Quaternion.identity) as GameObject;




			enemy.GetComponent<Behaviour>().enemyType = EnumScript.EnemyType.A;
		}
	}
}
