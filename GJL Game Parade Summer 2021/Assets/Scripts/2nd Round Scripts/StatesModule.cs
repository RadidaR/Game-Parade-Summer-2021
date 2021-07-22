using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;

namespace Toiper
{
    namespace Player
    {
        public class StatesModule : MonoBehaviour
        {
            [Header("Data")]
            [SerializeField] NewGameData data;
            [SerializeField] NewCurrentData current;

            [Header("Ground Check")]
            [HideInInspector] [SerializeField] Transform groundCheck;
            [SerializeField] float groundCheckRadius;

            [Header("Swing Check")]
            [HideInInspector] [SerializeField] Transform swingCheck;

            [Header("Events")]
            [SerializeField] GameEvent eBounced;

            bool groundBelow => Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, data.groundLayer);
            bool isInBaseState => current.state == NewCurrentData.States.Neutral || current.state == NewCurrentData.States.Idle || current.state == NewCurrentData.States.Running || current.state == NewCurrentData.States.Airborne;
            bool swingInReach => Physics2D.OverlapCircle(swingCheck.position, data.swingReach, data.swingLayer);

            Rigidbody2D rigidBody;

            private void Awake()
            {
                //Debug.Log($"{data.groundLayerInt}");
                rigidBody = GetComponent<Rigidbody2D>();
            }

            private void FixedUpdate()
            {
                if (isInBaseState)
                    AssignBaseState();

                current.swingInReach = swingInReach;

                if (current.exitReached)
                    Timing.KillCoroutines();
            }

            void AssignBaseState()
            {
                if (groundBelow)
                    if (Mathf.Abs(rigidBody.velocity.x) > 1f)
                        current.state = NewCurrentData.States.Running;
                    else
                        current.state = NewCurrentData.States.Idle;
                else
                    current.state = NewCurrentData.States.Airborne;
            }

            public void Jump() => Timing.RunCoroutine(_Jump(), Segment.FixedUpdate);

            IEnumerator<float> _Jump()
            {
                if (current.state != NewCurrentData.States.Jumping)
                {
                    current.state = NewCurrentData.States.Jumping;
                    float timer = data.jumpDuration;
                    while (timer > 0)
                    {
                        timer -= Time.fixedDeltaTime;
                        yield return Timing.WaitForSeconds(Time.fixedDeltaTime);
                        if (timer <= 0)
                            break;
                    }

                    if (current.state == NewCurrentData.States.Jumping)
                        current.state = NewCurrentData.States.Neutral;
                }
            }

            public void Bounce() => Timing.RunCoroutine(_Bounce(), Segment.Update);

            IEnumerator<float> _Bounce()
            {
                //Debug.Log("Bounce");
                if (current.state != NewCurrentData.States.Bouncing)
                {
                    eBounced.Raise();
                    current.state = NewCurrentData.States.Bouncing;
                    float timer = data.bounceDuration;
                    while (timer > 0)
                    {
                        timer -= Time.deltaTime;
                        yield return Timing.WaitForSeconds(Time.deltaTime);
                        if (timer <= 0)
                            break;
                    }

                    if (current.state == NewCurrentData.States.Bouncing)
                        current.state = NewCurrentData.States.Neutral;
                }
            }

            public void Twist() => Timing.RunCoroutine(_Twist(), Segment.Update);

            IEnumerator<float> _Twist()
            {
                if (current.state != NewCurrentData.States.Twisting)
                {

                    current.state = NewCurrentData.States.Twisting;
                    rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                    float timer = data.twistDuration;
                    while (timer > 0)
                    {
                        timer -= Time.deltaTime;
                        yield return Timing.WaitForSeconds(Time.deltaTime);
                        if (timer <= 0)
                            break;
                    }
                    rigidBody.constraints = RigidbodyConstraints2D.None;
                    rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;

                    if (current.state == NewCurrentData.States.Twisting)
                        current.state = NewCurrentData.States.Neutral;
                }
            }

            public void Roll() => Timing.RunCoroutine(_Roll(), Segment.Update);

            IEnumerator<float> _Roll()
            {
                if (current.state != NewCurrentData.States.Rolling)
                {
                    current.state = NewCurrentData.States.Rolling;
                    float timer = data.rollDuration;
                    while (timer > 0)
                    {
                        timer -= Time.deltaTime;
                        yield return Timing.WaitForSeconds(Time.deltaTime);
                        if (timer <= 0)
                            break;
                    }

                    if (current.state == NewCurrentData.States.Rolling)
                        current.state = NewCurrentData.States.Neutral;
                }
            }

            public void Swing() => Timing.RunCoroutine(_Swing(), Segment.Update);

            IEnumerator<float> _Swing()
            {
                if (current.state != NewCurrentData.States.Swinging)
                {
                    current.state = NewCurrentData.States.Swinging;

                    //float timer = data.rollDuration;
                    while (current.swingInput == 1)
                    {
                        yield return Timing.WaitForSeconds(Time.deltaTime);
                        if (current.swingInput == 0)
                            break;
                    }

                    if (current.state == NewCurrentData.States.Swinging)
                        Timing.RunCoroutine(_Propelled(), Segment.Update);
                }
            }

            IEnumerator<float> _Propelled()
            {
                if (current.state != NewCurrentData.States.Propelled)
                {
                    rigidBody.AddForce(new Vector2(data.propelledSpeed * current.direction, 0), ForceMode2D.Impulse);
                    transform.rotation = Quaternion.identity;
                    transform.localScale = transform.localScale.SetValues(x: current.direction);
                    rigidBody.gravityScale = current.originalGravity / 3;
                    current.state = NewCurrentData.States.Propelled;
                    float timer = data.propelledDuration;
                    while (timer > 0)
                    {
                        timer -= Time.deltaTime;
                        yield return Timing.WaitForSeconds(Time.deltaTime);
                        if (timer <= 0)
                            break;
                    }

                    rigidBody.gravityScale = current.originalGravity;

                    if (current.state == NewCurrentData.States.Propelled)
                        current.state = NewCurrentData.States.Neutral;
                }
            }

            public void Done() => current.state = NewCurrentData.States.Done;

            private void OnDrawGizmosSelected()
            {
                if (groundCheckRadius <= 0 && groundCheck != null)
                    return;

                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);

                if (data.swingReach <= 0 && swingCheck != null)
                    return;

                Gizmos.color = Color.yellow;
                Gizmos.DrawWireSphere(swingCheck.position, data.swingReach);
            }
        }
    }
}
