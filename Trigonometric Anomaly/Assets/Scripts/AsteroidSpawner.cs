using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AsteroidSpawner : MonoBehaviour {
	[SerializeField] private Vector3 player_position = Vector3.zero;
	[SerializeField] private float spawn_interval = 5f;
	[SerializeField] private float timer = 0f;
	[SerializeField] private int min_asteroid = 5;
	[SerializeField] private int max_asteroid = 15;
	[SerializeField] private float max_speed = 10f;
	[SerializeField] private float spawn_distance = 10f;
	[SerializeField] private float move_into_player_radius = 10f;
	[SerializeField] private List<GameObject> prefabs = new List<GameObject>();

	private static AsteroidSpawner _instance = null;
	public static AsteroidSpawner Instance
	{
		get { return _instance; }
	}
	
	void Awake()
	{
		if (_instance != null && _instance != this)
		{
			Destroy(gameObject);
			return;
		}
		else
		{
			_instance = this;
		}
		//DontDestroyOnLoad(this.gameObject);
	}

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (prefabs.Count > 0)
		{
			timer += Time.deltaTime;
			player_position = PlayerMovement.Instance.position;

			if (timer >= spawn_interval)
			{
				int spawn_count = 0;
				timer = 0f;

				//Determin the number of asteroid to spawn between the set min and max
				if ((max_asteroid - min_asteroid) > 0)
				{
					spawn_count = ((int)(Random.value * 100)) % (max_asteroid - min_asteroid + 1) + min_asteroid;
				} else
				{
					spawn_count = min_asteroid;
				}

				for (int i = 0; i < spawn_count; i++)
				{
					//Foreach asteroid we spawn in a random direction at a set distance
					Vector2 rand = Random.insideUnitCircle;
					Vector3 rand_direction = new Vector3(rand.x, 0f, rand.y);
					rand_direction.Normalize();

					Vector3 spawnPoint = player_position + rand_direction * spawn_distance;

					//Randomize type of asteroids
					GameObject prefab = prefabs[((int)(Random.value * 100)) % prefabs.Count];

					GameObject asteroid = (GameObject)AsteroidSpawner.Instantiate(prefab, spawnPoint, Quaternion.identity);

					//Set the asteroids as a child to the spawner
					asteroid.transform.parent = this.transform;

					//Set a velocity to move near the player
					rand = Random.insideUnitCircle;
					rand_direction = new Vector3(rand.x, 0f, rand.y);
					rand_direction.Normalize();
					
					Vector3 target = player_position + rand_direction * spawn_distance;

					Vector3 target_direction = target - asteroid.transform.position;
					target_direction.Normalize();

					for (int child_index = 0; child_index < asteroid.transform.childCount; child_index++)
					{
						Transform child = asteroid.transform.GetChild(child_index);
						child.gameObject.rigidbody.velocity = target_direction * (Random.value * 100f % max_speed);
					}
				}
			}
		}
	}
}
