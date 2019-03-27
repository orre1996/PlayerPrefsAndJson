using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeScreen : MonoBehaviour {

	private bool FadeImage = false;
	

	void Update()
	{

		if (FadeImage == true)
		{
			if (GetComponent<Image>().color.a <= 1)
			{
				GetComponent<Image>().color += new Color(0, 0, 0, 0.6f * Time.deltaTime);

			}
			if (GetComponent<Image>().color.a >= 1)
			{
				Invoke("RestartGame", 0.5f);
			}
		}
		else if (FadeImage == false)
		{
			if (GetComponent<Image>().color.a >= 0.01f)
			{
				GetComponent<Image>().color -= new Color(0, 0, 0, 0.4f * Time.deltaTime);
			}
		}
	}


	void RestartGame()
	{
		Application.LoadLevel(Application.loadedLevel);
	}

	public void SetFade()
	{
		FadeImage = true;
	}
}
