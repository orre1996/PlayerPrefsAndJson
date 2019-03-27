using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skinz : MonoBehaviour
{
	public static Skinz Instance;

	[SerializeField] private List<DragonSkins.DragonSkinSetting> dragonSkins;
	//[SerializeField] private List<BirdSkinSetting> birdSkins;

	public DragonSkins dragonSkin;
	//public BirdSkin birdSkin;

	private Player player;
	private ScoreManager scoreManager; //use full descriptive names to make code easier to read

	// Use this for initialization
	void Start()
	{
		if (Instance != null)
		{
			Destroy(gameObject);
			return;
		}

		Instance = this;

		player = FindObjectOfType<Player>();
		scoreManager = ScoreManager.instance;
		
		dragonSkin = new DragonSkins(dragonSkins, player, scoreManager);
		

		//birdSkin = new BirdSkin(birdSkins, player, scoreManager);
	}

}
