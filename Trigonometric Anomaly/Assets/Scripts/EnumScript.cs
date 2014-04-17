using UnityEngine;
using System.Collections;

public class EnumScript : MonoBehaviour 
{
	public enum EnemyType{
		BLUE_ENEMY,
		GREEN_ENEMY,
		RED_ENEMY,
		NONE
	};

	public enum ShopMenuItems
	{
		Bomb = 0,
		TripleShot,
		SpiralShot,
		ForceField,
		NPCShield,
		ExtraLife,
		Back
	};
}
