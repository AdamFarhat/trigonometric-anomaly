using UnityEngine;
using System.Collections;

public class AsteroidSpawner : MonoBehaviour {
	[SerializeField] public Vector3 player_position = Vector3.zero;

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
		DontDestroyOnLoad(this.gameObject);
	}

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		player_position = PlayerMovement.Instance.position;


	}
}
