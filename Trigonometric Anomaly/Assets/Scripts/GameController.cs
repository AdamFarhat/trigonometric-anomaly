using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	[SerializeField] public GUIText wave_label = null;
	[SerializeField] public GUIText timer_label = null;
	[SerializeField] public float next_wave_timer = 0f;
	[SerializeField] public float current_wave_length = 120f;
	[SerializeField] public int current_wave = 1;

	private static GameController _instance = null;
	public static GameController Instance
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
		next_wave_timer = current_wave_length;
	}
	
	// Update is called once per frame
	void Update () 
	{
		next_wave_timer -= Time.deltaTime;

		//Reset timer
		//New wave spawning, pause game and open shop window
		if (next_wave_timer <= 0f)
		{
			next_wave_timer = current_wave_length;
			current_wave++;
			wave_label.text = "Wave " + current_wave;
		}

		//Display timer
		if(timer_label != null)
		{
			timer_label.text = "Next wave in " + convertTimeToText(next_wave_timer);
		}
	}

	/// <summary>
	/// Converts a float representing seconds into the following
	/// string format MM:ss
	/// </summary>
	/// <returns>The time to text.</returns>
	/// <param name="time">Time.</param>
	string convertTimeToText(float time)
	{
		string minutes = ((int)(time / 60f)).ToString();
		string seconds = ((int)(time % 60f)).ToString();

		return minutes.PadLeft(2, '0') + ":" + seconds.PadLeft(2, '0');
	}
}
