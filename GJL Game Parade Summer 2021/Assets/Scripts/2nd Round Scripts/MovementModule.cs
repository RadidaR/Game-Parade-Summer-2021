using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Toiper
{
    namespace Player
    {
        public class MovementModule : MonoBehaviour
        {
            InputActions playerInput;

            [Header("Data")]
            [SerializeField] NewGameData data;
            [SerializeField] NewCurrentData current;

            [Header("Events")]
            [SerializeField] GameEvent eJumped;


            Rigidbody2D rigidBody;

            bool canMove => current.state == NewCurrentData.States.Idle || current.state == NewCurrentData.States.Running || current.state == NewCurrentData.States.Jumping || current.state == NewCurrentData.States.Airborne || current.state == NewCurrentData.States.Propelled;
            bool canJump => current.state == NewCurrentData.States.Idle || current.state == NewCurrentData.States.Running;

            float currentSpeed => rigidBody.velocity.x;

            private void Awake()
            {
                playerInput = new InputActions();

                playerInput.Gameplay.Move.performed += ctx => current.moveInput = playerInput.Gameplay.Move.ReadValue<float>();
                playerInput.Gameplay.Move.canceled += ctx => current.moveInput = playerInput.Gameplay.Move.ReadValue<float>();

                playerInput.Gameplay.Jump.performed += ctx => Jump();

                rigidBody = GetComponent<Rigidbody2D>();
                current.originalGravity = rigidBody.gravityScale;
            }

            private void FixedUpdate()
            {
                if (current.moveInput != 0)
                    if (canMove)
                        Move();                
            }

            void Move()
            {
                current.direction = Mathf.RoundToInt(current.moveInput);
                transform.localScale = transform.localScale.SetValues(x: current.direction);
                if (current.state == NewCurrentData.States.Idle || current.state == NewCurrentData.States.Running || current.state == NewCurrentData.States.Jumping || current.state == NewCurrentData.States.Airborne)
                {
                    rigidBody.AddForce(new Vector2(data.moveSpeed * current.moveInput, 0));
                    if (Mathf.Abs(currentSpeed) >= data.maxSpeed)
                        rigidBody.velocity = rigidBody.SetVelocity(x: data.maxSpeed * current.moveInput);
                }
                else if (current.state == NewCurrentData.States.Propelled)
                {
                    rigidBody.AddForce(new Vector2(data.propelledSpeed * current.moveInput, 0));
                    if (Mathf.Abs(currentSpeed) >= data.maxSpeed)
                        rigidBody.velocity = rigidBody.SetVelocity(x: data.maxPropelledSpeed * current.moveInput);
                }
            }

            void Jump()
            {
                if (canJump)
                {
                    rigidBody.AddForce(new Vector2(0, data.jumpForce), ForceMode2D.Impulse);
                    eJumped.Raise();
                }
            }

            public void Freeze()
            {
                rigidBody.velocity = Vector2.zero;
                rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
            }

            public void Unfreeze() => rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;


            private void OnEnable()
            {
                playerInput.Enable();
            }

            private void OnDisable()
            {
                playerInput.Disable();
            }
        }
    }
}
