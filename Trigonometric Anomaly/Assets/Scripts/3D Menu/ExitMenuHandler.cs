using UnityEngine;
using System.Collections;

public class ExitMenuHandler : MonoBehaviour 
{

	[SerializeField] string SceneName;
	
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		//renderer.material.color = Color.white;
		
	}
	
	void OnMouseEnter() 
	{
		renderer.material.color = Color.yellow;
		gameObject.GetComponent<AudioSource>().Play();
	}
	
	void OnMouseExit()
	{
		renderer.material.color = Color.white;
	}
	
	void OnMouseDown()
	{
		OpenScene();
	}
	
	public void OpenScene()
	{
		Time.timeScale = 1f;
		if(SceneName!=null)
			Application.LoadLevel(SceneName);
	}
}
