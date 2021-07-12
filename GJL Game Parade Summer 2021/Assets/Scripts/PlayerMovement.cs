using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	[SerializeField] GameData data;
	[SerializeField] CurrentData current;

	[SerializeField] Rigidbody2D rigidBody;

	[SerializeField] GameEvent eBounced;

	protected void Awake()
	{
		rigidBody = GetComponent<Rigidbody2D>();
	}
	
	protected void FixedUpdate()
    {
		if (current.movingRight)
			current.direction = 1;
		else
			current.direction = -1;

		if (current.state != CurrentData.States.UsingTrampoline)
			Move();
		else
			rigidBody.velocity = rigidBody.SetVelocity(x: 0);
    }

    private void Move()
    {
		if (current.state != CurrentData.States.Rolling)
			rigidBody.velocity = rigidBody.SetVelocity(x: data.moveSpeed * current.direction);
		else
			rigidBody.velocity = rigidBody.SetVelocity(x: data.rollSpeed * current.direction, y: 0);

		transform.localScale = transform.SetScale(x: current.direction);
    }

	//called when there's wall ahead
	public void TurnAround()
    {
		if (current.movingRight)
			current.movingRight = false;
		else
			current.movingRight = true;
    }

	//called when player steps on trampoline
	public void Bounce()
    {
		rigidBody.AddForce(new Vector2(0, data.bounceForce), ForceMode2D.Impulse);
		eBounced.Raise();
    }
}
