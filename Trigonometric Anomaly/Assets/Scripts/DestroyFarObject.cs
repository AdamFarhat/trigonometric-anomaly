using UnityEngine;
using System.Collections;

public class DestroyFarObject : MonoBehaviour {
	[SerializeField] private float destroy_distance = 150f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Vector3.Distance(this.transform.position, PlayerMovement.Instance.position) >= destroy_distance)
		{
			Destroy(this.gameObject);
		}
	}
}
