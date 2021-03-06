﻿using UnityEngine;
using System.Collections;

public class AsteroidDebris : MonoBehaviour {
	[SerializeField] public int life = 10;
	[SerializeField] private int min_debris = 2;
	[SerializeField] private int max_debris = 5;
	[SerializeField] private int debris_to_spawn = 0;
	[SerializeField] private GameObject prefab = null;
	[SerializeField] private GameObject explosion = null;
	[SerializeField] private float immune_timer = 0.2f;
	// Use this for initialization
	void Start () {
		if ((max_debris - min_debris) > 0)
		{
				debris_to_spawn = ((int)(Random.value * 100)) % (max_debris - min_debris + 1) + min_debris;
		} else
		{
			debris_to_spawn = min_debris;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (immune_timer > 0f)
			immune_timer -= Time.deltaTime;
	}

	void OnCollisionEnter(Collision collision) {
		//A small immunity for creating the debris so they can collide between themselves and split up randomly with unity's collision resolution
		if (immune_timer < 0f)
		{
			life--;	
			if(life <= 0)
			{
				//Spawn other asteroids if the prefab is set
				if (prefab != null)
				{
						for (int count = 0; count < debris_to_spawn; count++)
						{
								GameObject debris = (GameObject)AsteroidSpawner.Instantiate(prefab, this.transform.position, Quaternion.identity);
								debris.transform.parent = AsteroidSpawner.Instance.transform;
								for (int child_index = 0; child_index < debris.transform.childCount; child_index++)
								{
										Transform child = debris.transform.GetChild(child_index);

										child.gameObject.AddComponent<CapSpeed>();
								}
						}
				}

				//Spawn explosion if there is one
				if(explosion != null)
				{
					//gameObject.GetComponent<AudioSource>().Play(); TODO: Play sound but Unity freezes, I think it's because the object is destroyed before end of play
					Instantiate(explosion, this.transform.position, Quaternion.identity);
				}
				
				Destroy(this.gameObject);
			}
		}
	}	
}
