using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour {
	
	private float movementSpeed = -5;

	private Vector3 spaceScale = new Vector3(0.493f, 0.3f, 0);
	private Vector3 desertScale = new Vector3(1.1f, 1.1f, 0);

	private SpriteRenderer spriteRenderer;

	[SerializeField]
	private Sprite desert;

	[SerializeField]
	private Sprite space;
	

	void Start () {
		spriteRenderer = GetComponent<SpriteRenderer>();
	}
	

	void Update () {

		transform.position += new Vector3(movementSpeed * Time.deltaTime, 0, 0);
		
	}

	public void ChanceDimension()
	{
		if(spriteRenderer.sprite == desert)
		{
			transform.localScale = spaceScale;
			spriteRenderer.sprite = space;
		}
		else
		{
			transform.localScale = desertScale;
			spriteRenderer.sprite = desert;
		}
	}


}
