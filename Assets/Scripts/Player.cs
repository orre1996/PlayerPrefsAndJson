using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;


public class Player : MonoBehaviour {

	private const int jumpForce = 500;
	private const int flyForce = 500;

	private const float movementSpeed = 5.0f;

	
	private readonly Vector3 birdStartPosition = new Vector3(-7.5f, 2f);
	private readonly Vector3 dragonStartPosition = new Vector3(-4f, -3.2f);

	private bool isDragon = true;
	private bool canJump = true;
	private bool hasGameStarted = false;
	private bool isGameOver = false;
	private bool hasShield = false;

	[FormerlySerializedAs("JumpDust")] [SerializeField]
	private GameObject jumpDust;


	private GameManager gm;

	private Rigidbody2D rb;
	private Animator animator;
	private BoxCollider2D boxCollider;

	private Color dragonColor = new Color(1, 1, 1);
	

	[SerializeField]
	private RuntimeAnimatorController bird;
	[SerializeField]
	private RuntimeAnimatorController dragon;

	[FormerlySerializedAs("Shield")] [SerializeField]
	private GameObject shield;

	void Start () {

		animator = GetComponent<Animator>();
		rb = GetComponent<Rigidbody2D>();
		gm = GameManager.instance;
		boxCollider = GetComponent<BoxCollider2D>();
	}
	

	void Update () {
		if (hasGameStarted && !isGameOver)
		{
			PhoneTiltMovement();
		}
		
		if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) && hasGameStarted && !isGameOver )
		
		{
			Jump();
			
		}
	}

	void Jump()
	{
		if(isDragon && canJump)
		{
			animator.SetInteger("DragonStates", 1);
			jumpDust.SetActive(true);
			rb.AddForce(Vector2.up * jumpForce);

		}
		else if (!isDragon)
		{
			rb.velocity = Vector3.zero;
			rb.AddForce(Vector2.up * flyForce);
		}
	}

	private void PhoneTiltMovement()
	{
		if((Input.acceleration.x < -0.1f || Input.GetKey(KeyCode.LeftArrow)) && transform.position.x > -7.5f)
		{
			transform.position -= new Vector3(movementSpeed * Time.deltaTime,0);
		}

		if ((Input.acceleration.x >= 0.1f || Input.GetKey(KeyCode.RightArrow)) && transform.position.x < 7.5f)
		{
			transform.position += new Vector3(movementSpeed * Time.deltaTime, 0);
		}
	}

	public void SetStartGame()
	{
		hasGameStarted = true;
	}

	IEnumerator ChangeDimension()
	{

		gm.ChangeDimension();
		if (isDragon)
		{
			rb.simulated = false;
			isDragon = false;
			animator.runtimeAnimatorController = bird;
			transform.position = birdStartPosition;
			transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
			GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
			shield.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
			yield return new WaitForSeconds(2);
			rb.simulated = true;
		}

		else
		{
			isDragon = true;
			animator.runtimeAnimatorController = dragon;
			transform.position = dragonStartPosition;
			transform.localScale = Vector3.one;
			shield.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
		}

	}
	
	public void SetCanJump(bool p_canJump)
	{
		canJump = p_canJump;
	}

	

	

	private void OnTriggerEnter2D(Collider2D other)
	{

		if(other.tag == "Ground")
		{
			animator.SetInteger("DragonStates", 0);
		}

		if (other.tag == "Portal")
		{
			StartCoroutine(ChangeDimension());
		}

		if(other.tag == "Enemy")
		{

			if (!hasShield)
			{
				
				
				boxCollider.enabled = false;
				rb.simulated = false;

				animator.SetInteger("DragonStates", 2);
				isGameOver = true;
				StartCoroutine(KillPlayer());

			}

			if(hasShield)
			{
				hasShield = false;
				shield.SetActive(false);
			}
		}
	}

	private IEnumerator KillPlayer()
	{
		yield return new WaitForSeconds(1);
		gm.GameOver();
		Destroy(this.gameObject);
	}

	public void SetDragonColor(Color p_color)
	{
		dragonColor = p_color;
		GetComponent<SpriteRenderer>().color = dragonColor;


	}

	public void SetBirdSkin(RuntimeAnimatorController p_AnimatorController)
	{
		bird = p_AnimatorController;
	}

	public bool GetIsShielded()
	{
		return hasShield;
	}

	public void ActivateShield()
	{
		shield.SetActive(true);
		hasShield = true;
	}
	
}
