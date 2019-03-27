using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonEnemy : BaseEnemy
{

	private const float movementSpeed = -5;
	private const float endPosition = -11;
	
	public override void Update()
	{
		transform.position += new Vector3(movementSpeed * Time.deltaTime, -Mathf.Sin(Time.time * 2.5f) * Time.deltaTime * 4, 0);

		if (transform.position.x < endPosition)
		{
			GameManager.instance.InactivateObject(this);
		}
	}
}
