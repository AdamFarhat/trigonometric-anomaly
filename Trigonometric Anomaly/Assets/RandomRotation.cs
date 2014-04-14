using UnityEngine;
using System.Collections;

public class RandomRotation : MonoBehaviour {
	[SerializeField] private Vector3 initial_rotation = Vector3.zero;
	[SerializeField] private Quaternion rotation = Quaternion.identity;
	[SerializeField] private float maxAngularVelocity = 10f;

	// Use this for initialization
	void Start () {
		maxAngularVelocity = (Random.value * 100f) % 5f;
		initial_rotation = Random.insideUnitSphere * maxAngularVelocity;
		rotation = Quaternion.Euler(initial_rotation);
		this.transform.rotation = Quaternion.Slerp (this.transform.rotation, Quaternion.LookRotation (initial_rotation), Time.deltaTime);
	}
	
	// Update is called once per frame
	void Update () 
	{
		this.transform.rotation = this.transform.rotation * rotation;
	}
}
