using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;

namespace Toiper
{
    namespace Player
    {
        public class AbilitiesModule : MonoBehaviour
        {
            InputActions playerInput;

            [Header("Data")]
            [SerializeField] NewGameData data;
            [SerializeField] NewCurrentData current;

            [Header("Events")]
            [SerializeField] GameEvent eTrampolineUsed;
            [SerializeField] GameEvent eRolling;
            [SerializeField] GameEvent eSwinging;
            [SerializeField] GameEvent eTwisting;


            Rigidbody2D rigidBody;
            LineRenderer lineRenderer;
            [HideInInspector] [SerializeField] Transform shootPoint;
            [HideInInspector] [SerializeField] GameObject rollPaper;
            [HideInInspector] [SerializeField] Transform trampolineSpawnPoint;
            [HideInInspector] [SerializeField] GameObject trampoline;
            [HideInInspector] [SerializeField] CapsuleCollider2D bodyCollider;
            [HideInInspector] [SerializeField] CircleCollider2D rollingCollider;

            bool canUseTrampoline => current.trampolineAvailable && (current.state == NewCurrentData.States.Idle || current.state == NewCurrentData.States.Running) && current.state != NewCurrentData.States.Done;
            bool canRoll => current.rollAvailable && current.state != NewCurrentData.States.Twisting && current.state != NewCurrentData.States.Swinging && current.state != NewCurrentData.States.Done;
            bool canSwing => current.swingAvailable && current.swingInReach && (current.state == NewCurrentData.States.Airborne || current.state == NewCurrentData.States.Jumping || current.state == NewCurrentData.States.Propelled) && current.state != NewCurrentData.States.Done;

            private void Awake()
            {
                playerInput = new InputActions();

                playerInput.Gameplay.UseTrampoline.performed += ctx => UseTrampoline();
                playerInput.Gameplay.Roll.performed += ctx => UseRoll();
                playerInput.Gameplay.Swing.performed += ctx => UseSwing();
                playerInput.Gameplay.Swing.performed += ctx => current.swingInput = playerInput.Gameplay.Swing.ReadValue<float>();
                playerInput.Gameplay.Swing.canceled += ctx => current.swingInput = playerInput.Gameplay.Swing.ReadValue<float>();

                rigidBody = GetComponent<Rigidbody2D>();
                lineRenderer = GetComponentInChildren<LineRenderer>();
            }

            void UseTrampoline()
            {
                if (canUseTrampoline)
                    Timing.RunCoroutine(_Trampoline(), Segment.FixedUpdate);
            }

            IEnumerator<float> _Trampoline()
            {
                eTrampolineUsed.Raise();
                current.trampolineAvailable = false;
                current.abilitiesUsed++;
                yield return Timing.WaitForOneFrame;
                bool wallAhead = Physics2D.OverlapCircle(trampolineSpawnPoint.position, 2.25f, data.groundLayer);
                
                if (wallAhead)
                {
                    transform.localScale = transform.localScale.SetValues(x: transform.localScale.x * -1);
                }

                trampoline.transform.position = trampolineSpawnPoint.position;
                trampoline.transform.SetParent(null);
                trampoline.SetActive(true);
            }

            void UseRoll()
            {
                if (canRoll)
                    Timing.RunCoroutine(_Roll(), Segment.FixedUpdate);
            }

            IEnumerator<float> _Roll()
            {
                eRolling.Raise();
                current.rollAvailable = false;
                current.abilitiesUsed++;
                bodyCollider.enabled = false;
                rollingCollider.enabled = true;
                Vector2 startPoint = transform.position.DropToV2();
                yield return Timing.WaitForOneFrame;
                rigidBody.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
                rigidBody.gravityScale = 0;
                lineRenderer.startWidth = data.rollLineWidth;
                lineRenderer.endWidth = data.rollLineWidth * 0.75f;
                lineRenderer.enabled = true;
                while (current.state == NewCurrentData.States.Rolling)
                {
                    lineRenderer.SetPosition(0, shootPoint.position);
                    lineRenderer.SetPosition(1, startPoint);
                    rigidBody.velocity = rigidBody.SetVelocity(x: data.rollSpeed * current.direction, y: 0);
                    yield return Timing.WaitForSeconds(Time.fixedDeltaTime);
                    if (current.state != NewCurrentData.States.Rolling)
                        break;
                }
                rollPaper.SetActive(true);
                rollPaper.transform.SetParent(null);
                lineRenderer.enabled = false;
                rigidBody.gravityScale = current.originalGravity;
                bodyCollider.enabled = true;
                rollingCollider.enabled = false;
                rigidBody.constraints = RigidbodyConstraints2D.None;
                rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
            }

            void UseSwing()
            {
                if (canSwing)
                    Timing.RunCoroutine(_Swing(), Segment.FixedUpdate);
            }

            IEnumerator<float> _Swing()
            {
                eTwisting.Raise();
                current.swingAvailable = false;
                current.abilitiesUsed++;

                GameObject hook = Physics2D.OverlapCircle(transform.position, data.swingReach + 7.5f, data.swingLayer).gameObject;

                current.hookPosition = hook.transform.position;

                Vector2 position2D = transform.position.DropToV2();
                Vector2 target = hook.transform.position.DropToV2();

                Vector2 direction = target.GetDirection(position2D);
                Vector2 reverseDirection = position2D.GetDirection(target);

                float angle = Mathf.Atan2(reverseDirection.y, reverseDirection.x) * Mathf.Rad2Deg - 90;

                Vector2 startPos = target + (direction * data.swingReach);

                Rigidbody2D hookRb = hook.GetComponentInChildren<Rigidbody2D>();
                hookRb.rotation = angle;

                yield return Timing.WaitForOneFrame;
                yield return Timing.WaitForOneFrame;

                Transform swingTransform = hook.transform;

                foreach (Transform child in hook.transform)
                {
                    foreach (Transform subChild in child)
                    {
                        if (subChild.gameObject.tag == "SwingSpot")
                        {
                            swingTransform = subChild;
                        }
                    }
                }

                current.swingPosition = swingTransform.position;
                current.swingRotation = swingTransform.rotation;

                yield return Timing.WaitUntilDone(_LerpToPosition(startPos), Segment.FixedUpdate);

                eSwinging.Raise();

                Pendulum swing = swingTransform.GetComponentInParent<Pendulum>();
                swing.enabled = true;
                rigidBody.gravityScale = 0;

                while (current.state == NewCurrentData.States.Swinging)
                {
                    transform.localScale = transform.SetScale(x: 1);
                    transform.SetPositionAndRotation(swingTransform.position, swingTransform.rotation);

                    lineRenderer.SetPosition(0, shootPoint.position);
                    lineRenderer.SetPosition(1, current.hookPosition);

                    if (swing.movingClockwise)
                    {
                        yield return Timing.WaitForOneFrame;
                        current.direction = 1;
                    }
                    else
                    {
                        yield return Timing.WaitForOneFrame;
                        current.direction = -1;
                    }

                    if (current.swingInput == 0 || current.state != NewCurrentData.States.Swinging)
                        break;
                }
                //rigidBody.gravityScale = current.originalGravity;

                if ((swing.movingClockwise && hookRb.rotation > 0) || (!swing.movingClockwise && hookRb.rotation < 0))
                    rigidBody.AddForce(new Vector2(0, Mathf.Abs(hookRb.rotation) * data.propelledForce), ForceMode2D.Impulse);

                lineRenderer.enabled = false;
                hookRb.constraints = RigidbodyConstraints2D.FreezeAll;

                foreach (Transform child in hook.transform)
                {
                    foreach (Transform subChild in child)
                    {
                        if (subChild.gameObject.tag == "Paper")
                        {
                            subChild.gameObject.SetActive(true);
                        }
                    }
                }

                swing.enabled = false;
            }

            IEnumerator<float> _LerpToPosition(Vector2 newPosition)
            {
                eTwisting.Raise();
                float timer = data.twistDuration;
                lineRenderer.startWidth = data.swingLineWidth;
                lineRenderer.endWidth = data.swingLineWidth / 2;
                lineRenderer.enabled = true;
                while (current.state == NewCurrentData.States.Twisting)
                {                    
                    yield return Timing.WaitForSeconds(Time.fixedDeltaTime);
                    timer -= Time.fixedDeltaTime;
                    float t = 1 - (timer / data.twistDuration);
                    transform.position = transform.LerpPosition(current.swingPosition, t);
                    transform.rotation = transform.LerpEulerRotation(current.swingRotation, t);

                    //Shoot rope
                    lineRenderer.SetPosition(0, shootPoint.position);
                    lineRenderer.SetPosition(1, shootPoint.LerpPosition(current.hookPosition, t * 1.5f));


                    if (timer <= 0 /*|| current.state != NewCurrentData.States.Twisting*/)
                        break;
                }
            }

            private void OnEnable()
            {
                playerInput.Enable();
            }

            private void OnDisable()
            {
                playerInput.Disable();
            }

            private void OnDrawGizmos()
            {
                if (Physics2D.OverlapCircle(transform.position, data.swingReach + 7.5f, data.swingLayer))
                { 
                    Vector2 position2D = transform.position.DropToV2();
                    Vector2 target = Physics2D.OverlapCircle(transform.position, data.swingReach + 7.5f, data.swingLayer).transform.position.DropToV2();

                    Vector2 direction = target.GetDirection(position2D);

                    //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

                    Vector2 startPos = target + (direction * data.swingReach);

                    Gizmos.DrawWireSphere(startPos, 10f);
                }

                if (trampolineSpawnPoint != null)
                {
                    Gizmos.color = Color.yellow;
                    Gizmos.DrawWireSphere(trampolineSpawnPoint.position, 2.25f);
                }
            }
        }
    }        
}
