using UnityEngine;
using System.Collections;

public class ShopWindow : MonoBehaviour {
	[SerializeField] public Rect windowSize = new Rect(0, 0, 500, 250);
	[SerializeField] public Texture2D btn_1 = null;
	
	private static ShopWindow _instance = null;
	public static ShopWindow Instance
	{
		get { return _instance; }
	}
	
	int price;
	public string messageText;
	GameObject camera;
	public Texture2D shieldTexture;
	public Texture2D allyTexture;
	public Texture2D bombTexture;
	
	
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
		camera = GameObject.FindGameObjectWithTag("MainCamera");
		messageText = "";
		price = 0;
		this.enabled = false;
		windowSize.x = Screen.width / 2 - windowSize.width / 2;
		windowSize.y = Screen.height / 2 - windowSize.height / 2;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnGUI () 
	{
		GUIStyle title = new GUIStyle(GUI.skin.GetStyle("Window"));
		title.fontSize = 15;
		title.fontStyle = FontStyle.Bold;
		
		GUI.Window(1, windowSize, createWindow, "Shop", title);
	}
	
	void createWindow(int id)
	{
		GUIStyle centeredStyle = new GUIStyle(GUI.skin.GetStyle("Label"));
		GUIStyle subtitle = new GUIStyle(GUI.skin.GetStyle("Label"));
		
		centeredStyle.alignment = TextAnchor.MiddleCenter;
		subtitle.fontStyle = FontStyle.Bold;
		subtitle.fontSize = 10;
		
		GUILayout.BeginVertical();
		GUILayout.Label("You can buy upgrades here at the expense of your score.");
		GUILayout.Space(5);
		GUILayout.Label("Offensive", subtitle);
		
		GUILayout.BeginHorizontal();
		
		GUILayout.BeginVertical();
		if (GUILayout.Button(bombTexture))
		{
			price = 5000;
			if((ScoreController.Instance.score - price) >= 0)
			{
				messageText = "Purchased!";
				camera.GetComponent<Shooting>().numberBombs += 1;
				ScoreController.Instance.decrementScore(price);
				
			}
			else
			{
				messageText = "Insufficient points";
				//print("Not enough");
			}
			
		}
		
		GUILayout.Label("5000", centeredStyle);
		GUILayout.EndVertical();
		
		GUILayout.BeginVertical();
		if (GUILayout.Button("Triple Shot"))
		{
			price = 50000;
			if (camera.GetComponent<Shooting>().tripleShotPurchased != true)
			{
				if ((ScoreController.Instance.score - price) >= 0)
				{
					messageText = "Purchased!";
					camera.GetComponent<Shooting>().tripleShotPurchased = true;
					ScoreController.Instance.decrementScore(price);
				} else
				{
					messageText = "Insufficient points";
					//print("Not enough");
				}
			}
			else{
				messageText = "You already purchase this item!";
			}
		}
		GUILayout.Label("50000", centeredStyle);
		GUILayout.EndVertical();
		GUILayout.BeginVertical();
		if (GUILayout.Button("Spiral Shot"))
		{
			price = 75000;
			if (camera.GetComponent<Shooting>().spiralShotPurchased != true)
			{
				if ((ScoreController.Instance.score - price) >= 0)
				{
					messageText = "Purchased!";
					camera.GetComponent<Shooting>().spiralShotPurchased = true;
					ScoreController.Instance.decrementScore(price);
				} 
				else{
					messageText = "Insufficient points";
					//print("Not enough");
				}
			}
			else{
				messageText = "You already purchase this item!";
			}
		}
		GUILayout.Label("75000", centeredStyle);
		GUILayout.EndVertical();
		GUILayout.EndHorizontal();
		GUILayout.Space(5);
		GUILayout.Label("Defensive", subtitle);
		
		GUILayout.BeginHorizontal();
		GUILayout.BeginVertical();

		if (GUILayout.Button(shieldTexture))
				{
			price = 0;
			if(camera.GetComponent<Shooting>().hasShield != true)
			{
				if ((ScoreController.Instance.score - price) >= 0)
				{
					messageText = "Purchased!";
					camera.GetComponent<Shooting>().setShield();
					ScoreController.Instance.decrementScore(price);
				} 
				else{
					messageText = "Insufficient points";
				}
			}
			else{
				messageText = "You already purchase this item!";
			}

				}
		GUILayout.Label("10000", centeredStyle);
		GUILayout.EndVertical();
		GUILayout.BeginVertical();
		if (GUILayout.Button(allyTexture))
		{
			price = 0;
			if (camera.GetComponent<Shooting>().hasAlly != true)
			{
				if ((ScoreController.Instance.score - price) >= 0)
				{
					messageText = "Purchased!";
					camera.GetComponent<Shooting>().createAlly();
					ScoreController.Instance.decrementScore(price);
				} 
				else{
					messageText = "Insufficient points";
				}
			}
			else{
				messageText = "You already purchase this item!";
			}
			
		}
		GUILayout.Label("40000", centeredStyle);
		GUILayout.EndVertical();
		GUILayout.Button("Button");
		GUILayout.EndHorizontal();
		GUILayout.EndVertical();
		
		//GUI.Button (new Rect (10,200,100,20), new GUIContent ("Click me", btn_1, "This is the tooltip"));
		/*if (GUI.Button (new Rect (10,150,150,100), "I am a button")) {
			print ("You clicked the button!");
		}*/
		
		GUI.Label(new Rect(windowSize.width / 2 - 240, windowSize.height - 60, 480, 30), messageText, centeredStyle);
		GUI.Label(new Rect(windowSize.width / 2 - 240, windowSize.height - 30, 480, 30), "Close shop by pressing <ENTER>", centeredStyle);
		GUI.DragWindow();
	}
}
