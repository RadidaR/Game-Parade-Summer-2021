using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	[SerializeField] GameData data;
	[SerializeField] CurrentData current;

	[SerializeField] Rigidbody2D rigidBody;
	//[SerializeField] bool movingRight = true;
	//[SerializeField] float moveSpeed;
	
	
	// Awake is called when the script instance is being loaded.
	protected void Awake()
	{
		rigidBody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called every frame, if the MonoBehaviour is enabled.
	protected void FixedUpdate()
    {
		if (current.movingRight)
			current.direction = 1;
		else
			current.direction = -1;

        Move();
    }

    private void Move()
    {
		rigidBody.velocity = rigidBody.SetVelocity(x: data.moveSpeed * current.direction);
		transform.localScale = transform.SetScale(x: current.direction);
    }

	public void TurnAround()
    {
		if (current.movingRight)
			current.movingRight = false;
		else
			current.movingRight = true;
    }

	public void Bounce()
    {
		current.isBouncing = true;
		rigidBody.AddForce(new Vector2(0, data.bounceForce), ForceMode2D.Impulse);
    }
}
