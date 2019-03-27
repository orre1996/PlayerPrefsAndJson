using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveLoadStats : MonoBehaviour {

	ScoreManager scoreManager;
	Skinz skins;
	private string json;
	private int HighScore;

	void Awake () {
		scoreManager = ScoreManager.instance;
		skins = FindObjectOfType<Skinz>();
		
	}

	private void Start()
	{
		scoreManager = ScoreManager.instance;
		scoreManager.SetHighScore(PlayerPrefs.GetInt("HighScore"));
		scoreManager.SetCoins(PlayerPrefs.GetInt("Coins"));
		Load();
	}

	public void Save()
	{
		scoreManager = FindObjectOfType<ScoreManager>();
		PlayerPrefs.SetInt("HighScore", scoreManager.GetHighScore());
		PlayerPrefs.SetInt("Coins", scoreManager.GetCoins());

		//PlayerPrefs.SetInt("CurrentDragonColor", skins.GetDragonSkin());
		//PlayerPrefs.SetInt("CurrentBirdSkin", skins.GetBirdSkin());
		//PlayerPrefs.SetInt("FirstDragonSkinUnlocked", skins.GetFirstDragonSkinUnlocked() ? 1 : 0);
		//PlayerPrefs.SetInt("SecondDragonSkinUnlocked", skins.GetSecondDragonSkinUnlocked() ? 1 : 0);
		//PlayerPrefs.SetInt("FirstBirdSkinUnlocked", skins.GetFirstBirdskinUnlocked() ? 1 : 0);
		//PlayerPrefs.SetInt("SecondBirdSkinUnlocked", skins.GetSecondBirdskinUnlocked() ? 1 : 0);

		string json = JsonUtility.ToJson(skins);
		PlayerPrefs.SetString("Skins", json);
	//	File.WriteAllText(Application.dataPath + "/SaveFile.json", json);
		
		
	}

	public void Load()
	{
		scoreManager = ScoreManager.instance;
		scoreManager.SetHighScore(PlayerPrefs.GetInt("HighScore"));
		scoreManager.SetCoins(PlayerPrefs.GetInt("Coins"));


		string load = PlayerPrefs.GetString("Skins");
		
		List<bool> hej = JsonUtility.FromJson<List<bool>>(load);




		//skins.SetFirstBirdSkinUnlocked(loadedSkins.GetFirstDragonSkinUnlocked());
		//skins.Tralala(loadedSkins);


		//skins.SetDragonSkin(PlayerPrefs.GetInt("CurrentDragonColor"));
		//skins.SetBirdSkin(PlayerPrefs.GetInt("CurrentBirdSkin"));
		//skins.SetFirstDragonSkinUnlocked(PlayerPrefs.GetInt("FirstDragonSkinUnlocked") != 0);
		//skins.SetSecondDragonSkinUnlocked(PlayerPrefs.GetInt("SecondDragonSkinUnlocked") != 0);
		//skins.SetFirstBirdSkinUnlocked(PlayerPrefs.GetInt("FirstBirdSkinUnlocked") != 0);
		//skins.SetSecondBirdSkinUnlocked(PlayerPrefs.GetInt("SecondBirdSkinUnlocked") != 0);
	}
}
