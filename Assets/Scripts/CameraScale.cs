using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScale : MonoBehaviour {

	private readonly float targetaspect = 16.0f / 9.0f;

	// Use this for initialization
	void Start()
	{
		Camera.main.aspect = targetaspect;
	}
}
