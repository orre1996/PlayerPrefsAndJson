using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeEnemy : BaseEnemy
{
	private float movementSpeed = -5;
	
	// Use this for initialization
	void Start()
	{ 
		base.setMovementSpeed(movementSpeed);
	}

	public override void SetObjectInactive()
	{
		GameManager.instance.InactivateObject(this);
	}
}
