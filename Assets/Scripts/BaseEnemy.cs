using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
	private float BaseMovementSpeed = -5;
	private const float endPosition = -11;

	private GameManager gm;
	// Start is called before the first frame update
	void Start()
    {
        gm = GameManager.instance;
    }

    // Update is called once per frame
    public virtual void Update()
    {
	    transform.position += new Vector3(BaseMovementSpeed * Time.deltaTime, 0, 0);

	    if (transform.position.x < endPosition)
		{
			SetObjectInactive();
		}
	}
	
	public virtual void setMovementSpeed(float p_value)
	{
		BaseMovementSpeed = p_value;
	}

	public virtual void setPosition(Vector2 p_position)
	{
		transform.position = p_position;
	}

	public virtual GameManager GetGameManager()
	{
		return gm;
	}

	public virtual void SetObjectInactive()
	{

	}
}
