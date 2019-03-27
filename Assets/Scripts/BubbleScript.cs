using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleScript : BaseEnemy
{
	private Player player;

	private float movementSpeed = -5;

	void Start()
	{
		player = FindObjectOfType<Player>();
		base.setMovementSpeed(movementSpeed);
	}

	public override void SetObjectInactive()
	{
		GameManager.instance.InactivateObject(this);
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			player.ActivateShield();
			GameManager.instance.InactivateObject(this);
		}
	}
}
