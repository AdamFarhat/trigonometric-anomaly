using UnityEngine;
using System.Collections;

public class WavyMovement : MonoBehaviour {
	
	public float rotSpeed = 100f;
	public float WaveSpeed = 600f;
	public float minDistance;
	public Transform target;
	
	void Start () {
		target = transform.parent;
		minDistance = Vector3.Distance(target.position , transform.position);
	}
	
	void Update () {
		transform.Rotate(Vector3.up, rotSpeed * Time.deltaTime);
		transform.RotateAround(target.position, Vector3.up, WaveSpeed * Time.deltaTime);
		
		//fix possible changes in distance
		float currentDistance = Vector3.Distance(target.position, transform.position);
		Vector3 towardsTarget = transform.position - target.position;
		transform.position += (minDistance - currentDistance) * towardsTarget.normalized;



	}
}
