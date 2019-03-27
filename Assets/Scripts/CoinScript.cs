using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : BaseEnemy {
	
	private float movementSpeed = -5;
	
	private ScoreManager sm;

	void Start()
	{
		sm = ScoreManager.instance;
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
			sm.IncreaseCoins();
			GameManager.instance.InactivateObject(this);
		}
	}
}
