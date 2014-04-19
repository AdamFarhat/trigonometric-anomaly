using UnityEngine;
using System.Collections;

public class ShopMenuHandler : MonoBehaviour 
{
	
	[SerializeField] string SceneName;
	[SerializeField] EnumScript.ShopMenuItems menuItem;
	[SerializeField] GameObject UserMessage;
	TextMesh tm;
	string messageText;

	int price;
	GameObject camera;
	
	// Use this for initialization
	void Start () 
	{
		camera = GameObject.FindGameObjectWithTag("MainCamera");

	}
	
	// Update is called once per frame
	void Update () 
	{
		
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
	
	void OnMouseDown()
	{

		switch(menuItem) 
		{
			case(EnumScript.ShopMenuItems.Bomb):
			{
				price = 5000;
				if((ScoreController.Instance.score - price) >= 0)
				{
					messageText = "Purchased!";
					UserMessage.renderer.material.color = Color.green;
					camera.GetComponent<Shooting>().numberBombs += 1;
					ScoreController.Instance.decrementScore(price);
				}
				else
				{
					messageText = "Insufficient points";
					UserMessage.renderer.material.color = Color.red;
					//print("Not enough");
				}
				UserMessage.GetComponent<TextMesh>().text = messageText;

				break;
			}
			case(EnumScript.ShopMenuItems.TripleShot):
			{
				price = 50000;
				if (camera.GetComponent<Shooting>().tripleShotPurchased != true)
				{
					if ((ScoreController.Instance.score - price) >= 0)
					{
						messageText = "Purchased!";
						UserMessage.renderer.material.color = Color.green;
						camera.GetComponent<Shooting>().tripleShotPurchased = true;
						ScoreController.Instance.decrementScore(price);
					} 
					else
					{
						UserMessage.renderer.material.color = Color.red;
						messageText = "Insufficient points";
						//print("Not enough");
					}
				}
				else
				{
					UserMessage.renderer.material.color = Color.red;
					messageText = "You already purchase this item!";
				}
				UserMessage.GetComponent<TextMesh>().text = messageText;
				break;
			}
			case(EnumScript.ShopMenuItems.SpiralShot):
			{
				price = 75000;
				if (camera.GetComponent<Shooting>().spiralShotPurchased != true)
				{
					if ((ScoreController.Instance.score - price) >= 0)
					{
						messageText = "Purchased!";
						UserMessage.renderer.material.color = Color.green;
						camera.GetComponent<Shooting>().spiralShotPurchased = true;
						ScoreController.Instance.decrementScore(price);
					} 
					else
					{
						UserMessage.renderer.material.color = Color.red;
						messageText = "Insufficient points";
						//print("Not enough");
					}
				}
				else
				{
					UserMessage.renderer.material.color = Color.red;
					messageText = "You already purchase this item!";
				}
				UserMessage.GetComponent<TextMesh>().text = messageText;
				break;
			}
			case(EnumScript.ShopMenuItems.ForceField):
			{
				price = 10000;
				if(camera.GetComponent<Shooting>().hasShield != true)
				{
					if ((ScoreController.Instance.score - price) >= 0)
					{
						messageText = "Purchased!";
						UserMessage.renderer.material.color = Color.green;
						camera.GetComponent<Shooting>().setShield();
						ScoreController.Instance.decrementScore(price);
					} 
					else
					{
						UserMessage.renderer.material.color = Color.red;
						messageText = "Insufficient points";
					}
				}
				else
				{
					UserMessage.renderer.material.color = Color.red;
					messageText = "You already purchase this item!";
				}
				UserMessage.GetComponent<TextMesh>().text = messageText;
				break;
			}
			case(EnumScript.ShopMenuItems.NPCShield):
			{
				price = 20000;
				if (camera.GetComponent<Shooting>().hasAlly != true)
				{
					if ((ScoreController.Instance.score - price) >= 0)
					{
						messageText = "Purchased!";
						UserMessage.renderer.material.color = Color.green;
						camera.GetComponent<Shooting>().createAlly();
						ScoreController.Instance.decrementScore(price);
					} 
					else
					{
						UserMessage.renderer.material.color = Color.red;
						messageText = "Insufficient points";
					}
				}
				else
				{
					UserMessage.renderer.material.color = Color.red;
					messageText = "You already purchase this item!";
				}
				UserMessage.GetComponent<TextMesh>().text = messageText;
				break;
			}
			case(EnumScript.ShopMenuItems.ExtraLife):
			{

				break;
			}

			case(EnumScript.ShopMenuItems.Back):
				{
					
					ShopWindow.Instance.enabled = false;
					ShopWindow.Instance.CloseShop();
					GameController.Instance.unpause();
					ShopWindow.Instance.messageText = "";
					GameController.Instance.state = GameController.GameState.PLAYING;

				break;
				}
		}
	}
}
