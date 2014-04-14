using UnityEngine;
using System.Collections;

public class DestroyAfterTime : MonoBehaviour {

	[SerializeField] private float timer = 1f;

	// Use this for initialization
	void Start () {
		Destroy(this.gameObject, timer);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
