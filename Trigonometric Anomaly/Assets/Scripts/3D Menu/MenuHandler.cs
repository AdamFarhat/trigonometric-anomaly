using UnityEngine;
using System.Collections;

public class MenuHandler : MonoBehaviour 
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

	
	public void OpenScene()
	{
		if(SceneName!=null)
			Application.LoadLevel(SceneName);
	}

	public void ToggleColor()
	{

	}
}
