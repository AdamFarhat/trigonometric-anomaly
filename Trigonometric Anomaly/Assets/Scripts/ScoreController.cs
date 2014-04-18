using UnityEngine;
using System.Collections;

public class ScoreController : MonoBehaviour {
	[SerializeField] public int score = 0;
	[SerializeField] public GUIText score_label = null;

	public int EnemyKillCount{get;set;}
	public int EnemyWaveLength{get;set;}

	private static ScoreController _instance = null;
	public static ScoreController Instance
	{
		get { return _instance; }
	}

	public ScoreController getInstance()
	{
		return _instance;
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
	void Start () {
				
	
		}
	// Update is called once per frame
	void Update () 
	{
	}

	void OnGUI() 
	{
		GUI.TextArea(new Rect(10, 10, 150, 100), EnemyKillCount.ToString()+ "/" + EnemyWaveLength.ToString());	
	}

	public void addScore(int points)
	{
		this.score += points;
		if (score_label != null)
		{
		score_label.text = "Score " + score;
		}
	}

	public void decrementScore(int points)
	{
		this.score -= points;
		if (score_label != null)
		{
			score_label.text = "Score " + score.ToString();
		}

	}

}
