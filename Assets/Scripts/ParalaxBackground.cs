using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//https://www.youtube.com/watch?v=QkisHNmcK7Y

public class ParalaxBackground : MonoBehaviour {

	public float backgroundSize;
	private Transform cameraTransform;
	private Transform[] Layers;
	private float viewZone = 10;
	private int leftIndex;
	private int rightIndex;
	
	void Start () {
		cameraTransform = Camera.main.transform;
		Layers = new Transform[transform.childCount];

		for (int i = 0; i < transform.childCount; i++)
		{
			Layers[i] = transform.GetChild(i);

			leftIndex = 0;
			rightIndex = Layers.Length - 1;

		}
	}
	
	private void ScrollRight()
	{
		Layers[leftIndex].position = new Vector3(1 * Layers[rightIndex].position.x + backgroundSize, Layers[rightIndex].position.y, 0);
		rightIndex = leftIndex;
		leftIndex++;
		if (leftIndex == Layers.Length)
		{
			leftIndex = 0;
		}
	}
	
	void Update () {
		if (cameraTransform.position.x > (Layers[rightIndex].transform.position.x - viewZone))
		{
			ScrollRight();
		}
	}
}
