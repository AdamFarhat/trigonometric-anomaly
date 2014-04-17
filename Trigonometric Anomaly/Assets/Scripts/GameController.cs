using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	public enum GameState
	{
		PLAYING,
		MENU,
		SHOPPING,
		GAME_OVER
	}

	[SerializeField] public GUIText wave_label = null;
	[SerializeField] public GUIText timer_label = null;
	[SerializeField] public float next_wave_timer = 0f;
	[SerializeField] public float current_wave_length = 5f;
	[SerializeField] public int current_wave = 1;
	[SerializeField] public int NumberOfEnemies;
	[SerializeField] public GameState state =  GameState.PLAYING;

	[SerializeField] public bool Bombs = false;
	[SerializeField] public bool TripleShot = false;
	[SerializeField] public bool SpiralShot = false;
	[SerializeField] public bool Shield = false;
	[SerializeField] public bool NPCShield = false;

	int roundWaveCount;
	public int enemyWaveCount;

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
		//next_wave_timer = current_wave_length;

		//EnemySpawning es = GameObject.FindGameObjectWithTag("EnemySpawner").GetComponent<EnemySpawning>();
		roundWaveCount = NumberOfEnemies;

		GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");

		if(Bombs)
		{
			camera.GetComponent<Shooting>().numberBombs += 10;
		}
		if(TripleShot )
		{
			camera.GetComponent<Shooting>().tripleShotPurchased = true;
		}
	   	if(SpiralShot)
		{
			camera.GetComponent<Shooting>().spiralShotPurchased = true;
		}
		if(Shield)
		{
			camera.GetComponent<Shooting>().setShield();
		}
		if(NPCShield )
		{
			camera.GetComponent<Shooting>().tripleShotPurchased = true;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{


		//Pause/unpause
		if (Input.GetKeyDown(KeyCode.Return) && state == GameState.PLAYING)
		{
			togglePauseState();
		} 
		else if (Input.GetKeyDown(KeyCode.Return) && state == GameState.SHOPPING)
		{
			ShopWindow.Instance.enabled = false;
			ShopWindow.Instance.CloseShop();
			unpause();
			ShopWindow.Instance.messageText = "";
			state = GameState.PLAYING;

		}

		//next_wave_timer -= Time.deltaTime;

		//Reset timer
		//New wave spawning, pause game and open shop window
		if (enemyWaveCount <= 0)
		{
			//next_wave_timer = current_wave_length;
			float incr = roundWaveCount * 1.2f;
			roundWaveCount = (int)incr;
			enemyWaveCount = roundWaveCount;

			//current_wave++;


			//Popup shop
			Time.timeScale = 0f;
			state = GameState.SHOPPING;
			ShopWindow.Instance.enabled = true;
		}

		wave_label.text = "EnemyCount =" + (roundWaveCount -enemyWaveCount);

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

	public void togglePauseState()
	{
		Time.timeScale = (-1f * Time.timeScale + 1);
	}

	public void pause()
	{
		Time.timeScale = 0f;
	}

	public void unpause()
	{
		Time.timeScale = 1f;
	}
}
