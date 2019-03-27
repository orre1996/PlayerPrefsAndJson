using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : BaseEnemy {
	
	private float movementSpeed = -5;

	private void Start()
	{
		base.setMovementSpeed(movementSpeed);
	}

	public override void SetObjectInactive()
	{
		GameManager.instance.InactivateObject(this);
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "Player")
		{
			GameManager.instance.DestroyAllEnemies();
			GameManager.instance.InactivateObject(this);
		}
	}
}
