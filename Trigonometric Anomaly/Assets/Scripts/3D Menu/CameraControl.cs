using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour 
{
	
	RaycastHit hit;
	bool leftClickFlag = true;
	
//	void OnGUI()
//	{	
//		GUI.color=Color.black;
//		Rect rect = new Rect(50,400,Screen.width,Screen.height);
//		
//		GUI.Label(rect,text);
//	}
	
	void Start()
	{

	}
	
	void Update () 
	{
		/***Left Click****/
		if (Input.GetKey(KeyCode.Mouse0) && leftClickFlag)
			leftClickFlag = false;

//		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//
//		if (Physics.Raycast(ray, out hit, 100))
//		{
//			if (hit.transform.tag == "MenuText" )
//			{
////				TextMesh tm= hit.transform.gameObject.GetComponent("TextMesh") as TextMesh;
////				tm.color = Color.yellow;
//				MenuHandler mh = hit.transform.gameObject.GetComponent("MenuHandler") as MenuHandler;
//	
//			}
//		}
//	
		if (!Input.GetKey(KeyCode.Mouse0) && !leftClickFlag)
		{
			leftClickFlag = true;
			Ray ray2 = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray2, out hit, 100))
			{
				if (hit.transform.tag == "MenuText" )
				{
					MenuHandler mh = hit.transform.gameObject.GetComponent("MenuHandler") as MenuHandler;
					mh.OpenScene();
				}
			}
		}
	}
}
