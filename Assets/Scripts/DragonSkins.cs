using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DragonSkins
{

	private List<DragonSkinSetting> skins;
	private DragonSkinSetting currentSkin;
	
	private Player player;
	private SaveLoadStats stats;
	private ScoreManager scoreManager;
	
	public DragonSkins(List<DragonSkinSetting> dragonSkin, Player player, ScoreManager scoreManager)
	{
		this.skins = dragonSkin;
		this.player = player;
		this.scoreManager = scoreManager;

		Load();
	}

	public bool SetCurrent(int value = 0)
	{

		if (value >= skins.Count)
		{
			Debug.LogWarning("Requested value from list is greater than list count.");
			return false;
		}

		if (!skins[value].isUnlocked)
		{
			if (scoreManager.GetCoins() >= skins[value].price)
			{
				scoreManager.SetCoins(scoreManager.GetCoins() - skins[value].price);
				skins[value].isUnlocked = true;
			}
			else
			{
				return false;
			}
		}

		currentSkin = skins[value];
		player.SetDragonColor(currentSkin.dragonColor);
		Save();

		return true;
	}

	public void Load()
	{

		if (PlayerPrefs.HasKey("dragonSkins"))
		{
			string dragonSkins = PlayerPrefs.GetString("dragonSkins");
			List<bool> dragonsUnlocked = JsonUtility.FromJson<List<bool>>(dragonSkins);

			for (int i = 0; i < dragonsUnlocked.Count; i++)
			{
				if (skins.Count < i)
				{
					return;
				}

				skins[i].isUnlocked = dragonsUnlocked[i];
			}
			
			SetCurrent(PlayerPrefs.GetInt("CurrentDragonSkin"));
		}
	}

	private void Save()
	{
		List<bool> dragonsUnlocked = new List<bool>();

		for (int i = 0; i < skins.Count; i++)
		{
			dragonsUnlocked.Add(skins[i].isUnlocked);
		}
		string dragonSkins = JsonUtility.ToJson(dragonsUnlocked);  //dragonSkins is empty
		
		PlayerPrefs.SetString("dragonSkins", dragonSkins); 
		PlayerPrefs.SetInt("CurrentDragonSkin", skins.IndexOf(currentSkin));
		
	}

	public DragonSkinSetting getCurrentSkin(int p_value)
	{
		return skins[p_value];
	}

	[Serializable]
	public class DragonSkinSetting
	{
		public bool isUnlocked;
		public Color dragonColor;
		public int price = 25;
		public Button shopButton;
		public Sprite buttonSprite;
	}
}
