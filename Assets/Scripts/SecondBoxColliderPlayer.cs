using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondBoxColliderPlayer : MonoBehaviour {

	private Player player;

	void Start () {
		player = FindObjectOfType<Player>();
	}
	

	void Update () {
		
	}
	private void OnTriggerStay2D(Collider2D other)
	{
		if (other.tag == "Ground")
		{
			player.SetCanJump(true);
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == "Ground")
		{
			player.SetCanJump(false);
		}
	}
}
