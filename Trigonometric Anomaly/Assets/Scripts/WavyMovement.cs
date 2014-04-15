using UnityEngine;
using System.Collections;

public class WavyMovement : MonoBehaviour {
	
	public float rotSpeed = 100f;
	public float WaveSpeed = 600f;
	public float minDistance;
	public Transform target;
	public bool isSingle;
	
	void Start () {
		target = transform.parent;
		minDistance = Vector3.Distance(target.position , transform.position);
	}
	
	void Update () {
		//transform.Rotate(Vector3.up, rotSpeed * Time.deltaTime);
		transform.RotateAround(target.position, Vector3.up, WaveSpeed * Time.deltaTime);
		
		//fix possible changes in distance
		float currentDistance = Vector3.Distance(target.position, transform.position);
		Vector3 towardsTarget = transform.position - target.position;
		transform.position += (minDistance - currentDistance) * towardsTarget.normalized;
		transform.rotation = Quaternion.LookRotation(towardsTarget);
		
		
	}
	
	void OnTriggerEnter(Collider collision)
	{
		if (collision.gameObject.tag == "Enemy") {
			
			if(isSingle == true)
				collision.gameObject.GetComponent<Behaviour>().lowEnemyHealth -= 1f;
			else if(isSingle == false)
				collision.gameObject.GetComponent<Behaviour>().lowEnemyHealth -= 0.5f;
			
			//Destroy(transform.parent.gameObject);
			Destroy(gameObject);
			
			
		}
		
		
	}
	
	
}
