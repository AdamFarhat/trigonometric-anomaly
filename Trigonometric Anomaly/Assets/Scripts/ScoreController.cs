using UnityEngine;
using System.Collections;

public class ScoreController : MonoBehaviour {
	[SerializeField] public int score = 0;
	[SerializeField] public GUIText score_label = null;

	private static ScoreController _instance = null;
	public static ScoreController Instance
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
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{

	}

	public void addScore(int score)
	{
		this.score += score;
		if (score_label != null)
		{
			score_label.text = "Score " + score.ToString();
		}
	}
}
