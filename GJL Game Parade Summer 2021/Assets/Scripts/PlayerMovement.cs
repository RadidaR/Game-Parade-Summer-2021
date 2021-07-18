using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	[SerializeField] GameData data;
	[SerializeField] CurrentData current;

	[SerializeField] Rigidbody2D rigidBody;

	[SerializeField] GameEvent eBounced;
    [SerializeField] GameEvent eJumped;


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
        if (current.state == CurrentData.States.Done)
            return;

        if (current.moveInput != 0)
        {
            if (current.state != CurrentData.States.Rolling && current.state != CurrentData.States.Swinging && current.state != CurrentData.States.Propelled)
            {
                rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
                rigidBody.gravityScale = originalGravity;
                rigidBody.mass = originalMass;
                current.direction = current.moveInput;
                Move();
            }
        }
        else
        {
            if (current.state == CurrentData.States.Grounded || current.state == CurrentData.States.Airborne || current.state == CurrentData.States.Bouncing || current.state == CurrentData.States.UsingTrampoline)
                rigidBody.velocity = rigidBody.SetVelocity(x: 0);
        }

        if (current.state == CurrentData.States.Grounded || current.state == CurrentData.States.Airborne)
        {
            transform.rotation = Quaternion.identity;
            rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
            rigidBody.gravityScale = originalGravity;
            rigidBody.mass = originalMass;
        }
        else if (current.state == CurrentData.States.Rolling)
        {
            Roll();
        }
        else if (current.state == CurrentData.States.Swinging)
        {
            if (rigidBody.gravityScale == 0)
                return;

            //transform.localScale = transform.SetScale(x: 1);
            rigidBody.gravityScale = 0f;
            rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;

        }
        else if (current.state == CurrentData.States.Propelled)
        {
            if (rigidBody.gravityScale == originalGravity / 2)
                return;

            rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
            rigidBody.gravityScale = originalGravity / 2;
            rigidBody.mass = originalMass;
            rigidBody.AddForce(new Vector2(data.propellingForce * current.direction, current.swingAngle * current.direction * 100), ForceMode2D.Impulse);
            transform.rotation = Quaternion.identity;

        }
    }

    private void Move()
    {
        if (current.state == CurrentData.States.Done)
            return;

        if (current.state == CurrentData.States.Swinging)
            return;

        if (current.direction != 0)
            transform.localScale = transform.SetScale(x: current.direction);

        if (current.state == CurrentData.States.Grounded || current.state == CurrentData.States.Airborne || current.state == CurrentData.States.Bouncing)
            rigidBody.velocity = rigidBody.SetVelocity(x: data.moveSpeed * current.moveInput);
        
    }

    private void Roll()
    {
        if (current.state == CurrentData.States.Done)
            return;
        //if (rigidBody.constraints != )
        rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionY;
        rigidBody.velocity = rigidBody.SetVelocity(x: data.rollSpeed * current.direction);
    }

    //called when there's wall ahead
    public void TurnAround()
    {
		//if (current.movingRight)
		//	current.movingRight = false;
		//else
		//	current.movingRight = true;
    }

    public void Jump()
    {
        if (current.state != CurrentData.States.Grounded)
            return;

        eJumped.Raise();
        rigidBody.AddForce(new Vector2(0, data.jumpForce), ForceMode2D.Impulse);
    }

	//called when player steps on trampoline
	public void Bounce()
    {
        if (current.state == CurrentData.States.Done)
            return;

		if (current.state != CurrentData.States.Bouncing && current.state != CurrentData.States.Swinging)
		{
			rigidBody.AddForce(new Vector2(0, data.bounceForce), ForceMode2D.Impulse);
			eBounced.Raise();
		}
    }

	public void Freeze()
    {
		rigidBody.velocity = rigidBody.SetVelocity(x: 0, y: 0);
    }

    public void StopMotion()
    {
        rigidBody.velocity = Vector2.zero;
        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
        rigidBody.isKinematic = true;
    }




}
