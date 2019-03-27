using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeDimensionFlash : MonoBehaviour {

	SpriteRenderer sr;
	// Use this for initialization
	void Start () {
		sr = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
	
		if(sr.color.a >= 0)
		{
			sr.color -= new Color(0, 0, 0, 0.7f * Time.deltaTime);
		}
	}

	public void MaxAlpha()
	{
		sr.color = new Color(1, 1, 1, 1);
	}
}
