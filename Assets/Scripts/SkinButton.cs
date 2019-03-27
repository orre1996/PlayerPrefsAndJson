using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinButton : MonoBehaviour {
	

	public void UnlockSkin()
	{
		GetComponent<Image>().color = new Color(1, 1, 1);
		gameObject.transform.GetChild(0).gameObject.SetActive(false);
		gameObject.transform.GetChild(1).gameObject.SetActive(false);
		gameObject.transform.GetChild(2).gameObject.SetActive(true);
	}
}
