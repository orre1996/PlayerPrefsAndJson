using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

	private Text scoreText;
	[SerializeField]
	private Text coinsText;
	[SerializeField]
	private Text scoreGameOverUI;
	[SerializeField]
	private Text highScoreGameOverUI;

	public static ScoreManager instance;
	private GameManager gm;
	private SaveLoadStats stats;
	

	private int score = 0;
	private int highscore = 0;
	private int coins = 99;

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}

		else if (instance != this)
		{
			Destroy(gameObject);
		}
	}

	void Start () {

		gm = GameManager.instance;
		stats = FindObjectOfType<SaveLoadStats>();
		stats.Load();
	}
	
	IEnumerator CalculateScore()
	{
		while (true)
		{
			yield return new WaitForSeconds(1);

			if (!gm.GetGameOver())
			{
				score = Mathf.RoundToInt(gm.GetScore());
				scoreText.text = score.ToString();

				if (score > highscore)
				{
					highscore = score;
				}

				if (scoreGameOverUI != null)
				{
					scoreGameOverUI.text = score.ToString();
				}

				if (highScoreGameOverUI != null)
				{
					highScoreGameOverUI.text = highscore.ToString();
				}
			}
		}
	}

	public int GetHighScore()
	{
		return highscore;
	}

	public void SetHighScore(int p_HighScore)
	{
		highscore = p_HighScore;
	}

	public void SetCoins(int p_Coins)
	{
		coins = p_Coins;
		coinsText.text = coins.ToString();
		
	}

	public void IncreaseCoins()
	{
		coins++;
		coinsText.text = coins.ToString();
	}

	public int GetCoins()
	{
		return coins;
	}

	private void OnEnable()
	{
		gm = GameManager.instance;
		scoreText = GetComponent<Text>();
		StartCoroutine(CalculateScore());
	}
}
