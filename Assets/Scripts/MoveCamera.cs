using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour {

	private float endZoom = 5.0f;
	private float startZoom = 2.5f;
	private float moveSpeed = 1.2f;
	private float ZoomSpeed = 0.85f;

	private readonly Vector3 startPosition = new Vector3(0, -2.1f, -10);
	private readonly Vector3 endPosition = new Vector3(0, 0, -10);

	private bool HasGameStarted = false;
	// Use this for initialization
	void Start () {
		Camera.main.orthographicSize = startZoom;
		transform.position = startPosition;
	}
	
	// Update is called once per frame
	void Update () {
		
		if(HasGameStarted)
		{
			transform.position = Vector3.MoveTowards(transform.position, endPosition, moveSpeed * Time.deltaTime);
			Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, endZoom, ZoomSpeed * Time.deltaTime);
		}
	}

	public void SetGameStarted()
	{
		HasGameStarted = true;
	}
}
