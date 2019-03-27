using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdEnemy : BaseEnemy {
	
	private const float movementSpeed = -8;

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
