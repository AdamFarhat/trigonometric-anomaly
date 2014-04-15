using UnityEngine;
using System.Collections;

public class StartScreen : MonoBehaviour {
	
	static bool play = false;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	
	void OnGUI()
	{
		
		if (!play){ 
			
			if (GUI.Button(new Rect (Screen.width/2.6f,Screen.height/1.4f, Screen.width/4,Screen.height/10), "Play"))
			{ 
				play = true; Application.LoadLevel(1); 
			} 
			
			if (GUI.Button(new Rect (Screen.width/2.6f,Screen.height/1.2f, Screen.width/4,Screen.height/10), "Quit")){ 
				Application.Quit(); 
			} 
		}
		
		
	}
}