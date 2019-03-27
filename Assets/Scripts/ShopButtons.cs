using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopButtons : MonoBehaviour
{
	[SerializeField]private DragonSkins dragonSkins;

	[SerializeField] private Sprite buttonImage;

	[SerializeField] private Color buttonColor;

	private int dragonSkin = 0;

	[SerializeField] private int buttonIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
		if (Skinz.Instance.dragonSkin.getCurrentSkin(buttonIndex).isUnlocked)
		{
			GetComponent<Image>().color = Color.white;
			for (int i = 0; i < transform.childCount; i++)
			{
				if (transform.GetChild(i).gameObject.activeSelf)
				{
					transform.GetChild(i).gameObject.SetActive(false);
				}
				else
				{
					transform.GetChild(i).gameObject.SetActive(true);
				}
			}
		}
	}
	
	public void DefaultDragonSkin()
	{
		ChangeSkin(0);

	}

	public void FirstDragonSkin()
	{
		ChangeSkin(1);
	}

	public void SecondDragonSkin()
	{
		ChangeSkin(2);
	}


	public void ChangeSkin(int p_value)
	{
		if (Skinz.Instance.dragonSkin.SetCurrent(p_value) && !Skinz.Instance.dragonSkin.getCurrentSkin(p_value).isUnlocked)
		{
			GetComponent<Image>().color = Color.white;
			for (int i = 0; i < transform.childCount; i++)
			{
				if (transform.GetChild(i).gameObject.activeSelf)
				{
					transform.GetChild(i).gameObject.SetActive(false);
				}
				else
				{
					transform.GetChild(i).gameObject.SetActive(true);
				}
			}
		}
	}

	

	public void DefaultBirdSkin()
	{

	}

	public void FirstBirdSkin()
	{

	}

	public void SecondBirdSkin()
	{

	}


	
}
