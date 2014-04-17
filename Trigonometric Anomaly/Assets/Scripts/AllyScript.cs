using UnityEngine;
using System.Collections;

public class AllyScript : MonoBehaviour {
	
	
	public GameObject prefab;
	public Transform target;
	public float rotSpeed = 100f;
	public float WaveSpeed = 400f;
	public float minDistance;
	GameObject gameobject;
	int ally = 1;
	
	// Use this for initialization
	void Start () {
		gameobject = GameObject.FindGameObjectWithTag("MainCamera");
		target = transform.parent;
		minDistance = Vector3.Distance(target.position , transform.position);
		
	}
	
	// Update is called once per frame
	void Update () {
		if ( Time.timeScale <= 0 ) return;
		//transform.Rotate(Vector3.up, rotSpeed * Time.deltaTime);
		transform.RotateAround(target.position, Vector3.up, WaveSpeed * Time.deltaTime);
		
		//fix possible changes in distance
		float currentDistance = Vector3.Distance(target.position, transform.position);
		Vector3 towardsTarget = transform.position - target.position;
		transform.rotation = Quaternion.LookRotation(towardsTarget);
	}
	
	void OnTriggerEnter(Collider collision)
	{
		if (collision.gameObject.tag == "Enemy") {
			
			collision.gameObject.GetComponent<Behaviour>().lowEnemyHealth -= 3f;
			
			//Destroy(transform.parent.gameObject);
			Destroy(gameObject);
			
			gameobject.GetComponent<Shooting>().nbAlly -= 1;
			
			
		}
		
		
	}
}
