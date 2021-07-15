using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	[SerializeField] GameData data;
	[SerializeField] CurrentData current;

	[SerializeField] Rigidbody2D rigidBody;

	[SerializeField] GameEvent eBounced;

	float originalGravity;
	float originalMass;

	protected void Awake()
	{
		rigidBody = GetComponent<Rigidbody2D>();

		originalGravity = rigidBody.gravityScale;
		originalMass = rigidBody.mass;
	}

	protected void FixedUpdate()
	{
		if (current.movingRight)
			current.direction = 1;
		else
			current.direction = -1;

		if (current.state != CurrentData.States.UsingTrampoline)
			Move();
		else if (current.state == CurrentData.States.UsingTrampoline)
			rigidBody.velocity = rigidBody.SetVelocity(x: 0);

		if (current.state == CurrentData.States.Grounded || current.state == CurrentData.States.Airborne)
		{
			transform.rotation = Quaternion.identity;
			rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
			rigidBody.gravityScale = originalGravity;
			rigidBody.mass = originalMass;
		}
	}

    private void Move()
    {
		//if (current.state == CurrentData.States.Swinging)
		//	return;

		if (current.state == CurrentData.States.Grounded || current.state == CurrentData.States.Airborne || current.state == CurrentData.States.Bouncing)
			rigidBody.velocity = rigidBody.SetVelocity(x: data.moveSpeed * current.direction);
		else if (current.state == CurrentData.States.Rolling)
			rigidBody.velocity = rigidBody.SetVelocity(x: data.rollSpeed * current.direction, y: 0);
		else if (current.state == CurrentData.States.Swinging)
		{
			transform.localScale = transform.SetScale(x: 1);
			rigidBody.gravityScale = 0f;
            rigidBody.constraints = RigidbodyConstraints2D.None;
            //rigidBody.gravityScale = 1.75f;

            //         int swingDirection = current.direction;

            //         if (transform.position.x < current.hookPosition.x)
            //             swingDirection = 1;
            //         else if (transform.position.x > current.hookPosition.x)
            //             swingDirection = -1;

            //if (swingDirection == 1)
            //         {
            //	if (rigidBody.velocity.x > 0 && rigidBody.velocity.y < 0)
            //		current.movingRight = true;
            //         }
            //else if (swingDirection == -1)
            //         {
            //	if (rigidBody.velocity.x < 0 && rigidBody.velocity.y < 0)
            //		current.movingRight = false;
            //         }

            //current.direction = swingDirection;
        }


        //else if (current.state == CurrentData.States.Swinging)
        //      {
        //	Debug.Log("here");
        //	rigidBody.constraints = RigidbodyConstraints2D.None;
        //	rigidBody.mass = 0.3f;
        //	rigidBody.gravityScale = 1f;
        //	int swingDirection = current.direction;


        //	if (transform.position.x < current.hookPosition.x)
        //		swingDirection = 1;
        //	else if (transform.position.x > current.hookPosition.x)
        //		swingDirection = -1;

        //	if (Mathf.Abs(transform.position.x - current.hookPosition.x) > data.swingReach / 4)
        //          {
        //		if (transform.position.y > current.hookPosition.y)
        //			rigidBody.AddForce(new Vector2(0, data.swingForceY * Time.fixedDeltaTime));
        //		else
        //			rigidBody.AddForce(new Vector2(data.swingForceX * swingDirection * Time.fixedDeltaTime, 0));
        //          }

        //	if (swingDirection == -1)
        //	{
        //		if (rigidBody.velocity.x < -data.swingVelocityLimit)
        //			current.movingRight = false;
        //	}
        //	else
        //	{ 
        //		if (rigidBody.velocity.x > data.swingVelocityLimit)
        //			current.movingRight = true;
        //          }
        //      }

        if (current.state != CurrentData.States.Swinging)
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
		if (current.state != CurrentData.States.Bouncing)
		{
			rigidBody.AddForce(new Vector2(0, data.bounceForce), ForceMode2D.Impulse);
			eBounced.Raise();
		}
    }

	public void Freeze()
    {
		rigidBody.velocity = rigidBody.SetVelocity(y: 0);
    }
}
