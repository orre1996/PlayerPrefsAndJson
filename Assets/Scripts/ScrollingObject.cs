using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingObject : MonoBehaviour
{
	[SerializeField]
	private float MovementSpeed = -5;

	void Update()
    {
		transform.position += new Vector3(MovementSpeed * Time.deltaTime, 0, 0);
	}
}
