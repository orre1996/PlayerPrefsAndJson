using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;


public class GameManager : MonoBehaviour {
	

	[SerializeField]
	private GameObject spikeEnemy;
	[SerializeField]
	private GameObject birdEnemy;
	[SerializeField]
	private GameObject dragonEnemy;
	[SerializeField]
	private GameObject mountains;
	[SerializeField]
	private GameObject stars;
	[SerializeField]
	private GameObject portal;
	[SerializeField]
	private GameObject gameOverScreen;
	[SerializeField]
	private GameObject coin;
	[SerializeField]
	private GameObject bomb;
	[SerializeField]
	private GameObject shield;

	private readonly List<GameObject> obstacles = new List<GameObject>();
	private List<SpikeEnemy> spikeEnemies = new List<SpikeEnemy>();
	private List<BirdEnemy> birdEnemies = new List<BirdEnemy>();
	private List<DragonEnemy> dragonEnemies = new List<DragonEnemy>();
	private List<CoinScript> coins = new List<CoinScript>();
	private List<BombScript> bombs = new List<BombScript>();
	private List<BubbleScript> bubbles = new List<BubbleScript>();
	private List<GameObject> portals = new List<GameObject>();

	private readonly Vector3 enemySpikeStartPosition = new Vector3(11.5f, -4, 0);
	private readonly Vector3 enemyBirdStartPosition = new Vector3(11.5f, -3.2f, 0);
	private readonly Vector3 portalStartPosition = new Vector3(11.5f, 0, 0);
	private Vector3 powerupStartPosition = new Vector3(11.5f, 0, 0);
	private Vector3 enemyDragonStartPosition;

	public static GameManager instance;
	private Player player;
	private MoveCamera camera;
	private ChangeDimensionFlash flash;
	private ScrollingBackground[] backgrounds;
	private SaveLoadStats stats;
	
	private const float enemySpawnDelay = 0.2f;
	private const float spikeSpawnTimer = 3.0f;
	private const float minDragonStartYPosition = -3.2f;
	private const float maxDragonStartYPosition = 6.7f;
	private const float powerupStartXPosition = 11.5f;
	private const float minCoinStartYPosition = -4.0f;
	private const float maxCoinStartYPosition = -0.6f;
	private const float spaceMinCoinStartYPosition = -4.33f;
	private const float spaceMaxCoinStartYPosition = 4.3f;
	private const float dragonStartXPosition = 11f;
	private const float minPortalSpawnTimer = 15;
	private const float maxPortalSpawnTimer = 20;
	private const float minPowerupSpawnTimer = 10;
	private const float maxPowerupSpawnTimer = 15;
	private const float dragonSpawnTimer = 0.9f;
	private float score = 0;
	
	private const int minBirdSpawnTimer = 10;
	private const int maxBirdSpawnTimer = 15;
	private const int spikeEnemySpawnTimer = 5;
	
	private bool hasGameStarted = false;
	private bool isInSpace = false;
	private bool isGameOver = false;

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
		player = FindObjectOfType<Player>();
		camera = FindObjectOfType<MoveCamera>();
		backgrounds = FindObjectsOfType<ScrollingBackground>();
		flash = FindObjectOfType<ChangeDimensionFlash>();
		stats = FindObjectOfType<SaveLoadStats>();

		GameObject newPortal = Instantiate<GameObject>(portal, portalStartPosition, Quaternion.identity);
		portals.Add(newPortal);
		newPortal.SetActive(false);

	}
	

	void Update () {
		
		if(hasGameStarted && !isGameOver)
		{
			score += Time.deltaTime;
		}
		
	}

	public void StartGame()
	{

		StartCoroutine(SpawnSpikeEnemy());
		StartCoroutine(SpawnBirdEnemy());
		StartCoroutine(SpawnPortal());
		Invoke("SpawnPowerup", Random.Range(minPowerupSpawnTimer, maxPowerupSpawnTimer));
		hasGameStarted = true;
		player.SetStartGame();
		camera.SetGameStarted();
	}


	public void ChangeDimension()
	{
		CancelInvoke();
		flash.MaxAlpha();
		Invoke("SpawnPowerup", Random.Range(minPowerupSpawnTimer, maxPowerupSpawnTimer));

		BaseEnemy[] ObjectsInScene = GameObject.FindObjectsOfType<BaseEnemy>();
		foreach (BaseEnemy go in ObjectsInScene)
		{
			go.gameObject.SetActive(false);
		}

		foreach (ScrollingBackground background in backgrounds)
		{
			background.ChanceDimension();
		}
		
		portals[0].SetActive(false);
		
		if (isInSpace)
		{
			mountains.SetActive(true);
			stars.SetActive(false);
			isInSpace = false;
			StartCoroutine(SpawnSpikeEnemy());
			StartCoroutine(SpawnBirdEnemy());
		}
		else if (!isInSpace)
		{
			mountains.SetActive(false);
			stars.SetActive(true);
			StartCoroutine(SpawnDragonEnemy());
			isInSpace = true;
		}
	}
	
	IEnumerator SpawnSpikeEnemy()
	{
		yield return new WaitForSeconds(spikeSpawnTimer);
		if (!isInSpace)
		{

			int numberOfEnemies = Random.Range(1, 4);

			for (int i = 0; i < numberOfEnemies; i++)
			{
				if (!spikeEnemies.Any())
				{

					GameObject newEnemy =
						Instantiate<GameObject>(spikeEnemy, enemySpikeStartPosition, Quaternion.identity);
					obstacles.Add(newEnemy);
					yield return new WaitForSeconds(enemySpawnDelay);
				}
				else
				{
					spikeEnemies[0].gameObject.SetActive(true);
					spikeEnemies[0].setPosition(enemySpikeStartPosition);
					spikeEnemies.RemoveAt(0);
				}
			}
		}

		StartCoroutine(SpawnSpikeEnemy());
	}

	private IEnumerator SpawnBirdEnemy()
	{
		yield return  new WaitForSeconds(Random.Range(minBirdSpawnTimer, maxBirdSpawnTimer));

		if (!isInSpace)
		{
			if (!birdEnemies.Any())
			{
				GameObject newEnemy = Instantiate<GameObject>(birdEnemy, enemyBirdStartPosition, Quaternion.identity);
				obstacles.Add(newEnemy);
			}
			else
			{
				birdEnemies[0].gameObject.SetActive(true);
				birdEnemies[0].setPosition(enemyBirdStartPosition);
				birdEnemies.RemoveAt(0);
			}

		}

		StartCoroutine(SpawnBirdEnemy());
	}

	private IEnumerator SpawnDragonEnemy()
	{
		yield return new WaitForSeconds(dragonSpawnTimer);

		if (isInSpace)
		{

			if (!dragonEnemies.Any())
			{
				enemyDragonStartPosition = new Vector3(dragonStartXPosition,
					Random.Range(minDragonStartYPosition, maxDragonStartYPosition), 0);
				GameObject newEnemy =
					Instantiate<GameObject>(dragonEnemy, enemyDragonStartPosition, Quaternion.identity);
				obstacles.Add(newEnemy);
			}
			else
			{
				dragonEnemies[0].gameObject.SetActive(true);
				dragonEnemies[0].setPosition(enemyBirdStartPosition);
				dragonEnemies.RemoveAt(0);
			}
		}

		StartCoroutine(SpawnDragonEnemy());
	}

	private IEnumerator SpawnPowerup()
	{
		yield return new WaitForSeconds(Random.Range(minPowerupSpawnTimer, maxPowerupSpawnTimer));

		if(!isInSpace)
		{
			powerupStartPosition = new Vector3(powerupStartXPosition, Random.Range(minCoinStartYPosition, maxCoinStartYPosition), 0);
		}
		else if (isInSpace)
		{
			powerupStartPosition = new Vector3(powerupStartXPosition, Random.Range(spaceMinCoinStartYPosition, spaceMaxCoinStartYPosition), 0);
		}

		int Powerup = Random.Range(0, 3);

		switch(Powerup)
		{
			case 0:

				if (!coins.Any())
				{
					Instantiate<GameObject>(coin, powerupStartPosition, Quaternion.identity);
				}
				else
				{
					coins[0].gameObject.SetActive(true);
					coins[0].setPosition(powerupStartPosition);
					coins.RemoveAt(0);
				}
				StartCoroutine(SpawnPowerup());
				
				break;

			case 1:

				if (!bombs.Any())
				{
					Instantiate<GameObject>(bomb, powerupStartPosition, Quaternion.identity);
				}
				else
				{
					bombs[0].gameObject.SetActive(true);
					bombs[0].setPosition(powerupStartPosition);
					bombs.RemoveAt(0);
				}
				StartCoroutine(SpawnPowerup());

				break;

			case 2:

				if (!player.GetIsShielded())
				{
					if (!bubbles.Any())
					{
						Instantiate<GameObject>(shield, powerupStartPosition, Quaternion.identity);
					}
					else
					{
						bubbles[0].gameObject.SetActive(true);
						bubbles[0].setPosition(powerupStartPosition);
						bubbles.RemoveAt(0);
					}
					StartCoroutine(SpawnPowerup());

				}
				else
				{
					StartCoroutine(SpawnPowerup());
				}
				break;
		}
	}

	private IEnumerator SpawnPortal()
	{
		yield return new WaitForSeconds(Random.Range(minPortalSpawnTimer, maxPortalSpawnTimer));

		portals[0].SetActive(true);
		portals[0].transform.position = portalStartPosition;

		StartCoroutine(SpawnPortal());
	}

	public float GetScore()
	{
		return score;
	}

	public bool GetGameStarted()
	{
		return hasGameStarted;
	}

	public void DestroyAllEnemies()
	{
		BaseEnemy[] ObjectsInScene = GameObject.FindObjectsOfType<BaseEnemy>();
		foreach (BaseEnemy go in ObjectsInScene)
		{
			go.gameObject.SetActive(false);
		}
	}
	
	public void GameOver()
	{
		stats.Save();
		gameOverScreen.SetActive(true);
		isGameOver = true;
	}

	public bool GetGameOver()
	{
		return isGameOver;
	}

	public void InactivateObject(SpikeEnemy p_object)
	{
		spikeEnemies.Add(p_object);
		p_object.gameObject.SetActive(false);
	}

	public void InactivateObject(BirdEnemy p_object)
	{
		birdEnemies.Add(p_object);
		p_object.gameObject.SetActive(false);
	}

	public void InactivateObject(DragonEnemy p_object)
	{
		dragonEnemies.Add(p_object);
		p_object.gameObject.SetActive(false);
	}

	public void InactivateObject(BombScript p_object)
	{
		bombs.Add(p_object);
		p_object.gameObject.SetActive(false);
	}

	public void InactivateObject(BubbleScript p_object)
	{
		bubbles.Add(p_object);
		p_object.gameObject.SetActive(false);
	}
	public void InactivateObject(CoinScript p_object)
	{
		coins.Add(p_object);
		p_object.gameObject.SetActive(false);
	}
	
}
