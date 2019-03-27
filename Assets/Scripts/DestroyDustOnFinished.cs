using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyDustOnFinished : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void SetActive()
	{
		this.gameObject.SetActive(false);
	}

	private void OnEnable()
	{
		Invoke("SetActive", 0.5f);
	}
}
