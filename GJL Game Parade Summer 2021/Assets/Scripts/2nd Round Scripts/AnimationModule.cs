using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Toiper
{
    namespace Player
    {
        public class AnimationModule : MonoBehaviour
        {
            [SerializeField] NewCurrentData current;
            [SerializeField] NewGameData data;

            [SerializeField] NewCurrentData.States currentState;
            [SerializeField] string animationState;

            [SerializeField] AnimationClip jumpAnim;
            [SerializeField] AnimationClip propelledAnim;
            [SerializeField] AnimationClip rollingAnim;
            [SerializeField] AnimationClip twistingAnim;

            Animator animator;


            private void Awake()
            {
                animator = GetComponentInChildren<Animator>();

                data.jumpDuration = jumpAnim.length - 0.01f;
                data.bounceDuration = propelledAnim.length - 0.01f;
                data.propelledDuration = propelledAnim.length - 0.01f;
                data.rollDuration = rollingAnim.length - 0.01f;
                data.twistDuration = twistingAnim.length - 0.01f;

                //Debug.Log($"{jumpAnim.length}");
                //Debug.Log($"{jumpAnim.apparentSpeed}");


                //foreach (AnimationClip clip in animator.runtimeAnimatorController.animationClips)
                //{
                //    if (clip.name == "Jump_Anim")
                //    {
                //        Debug.Log($"Average Speed: {clip.averageSpeed}");
                //        Debug.Log($"Apparent Speed: {clip.apparentSpeed}");
                //        animator.la
                //    }
                //}
            }

            private void Update()
            {
                if (currentState == current.state)
                    return;

                currentState = current.state;
                //ChangeAnimationState();
                SetAnimationState();
                animator.Play(animationState);
            }

            void SetAnimationState()
            {
                if (currentState == NewCurrentData.States.Idle)
                    animationState = "Idle_Anim";
                else if (currentState == NewCurrentData.States.Running)
                    animationState = "Running_Anim";
                else if (currentState == NewCurrentData.States.Jumping)
                    animationState = "Jump_Anim";
                else if (currentState == NewCurrentData.States.Bouncing || currentState == NewCurrentData.States.Propelled)
                    animationState = "Propelled_Anim";
                else if (currentState == NewCurrentData.States.Airborne)
                    animationState = "Airborne_Anim";
                else if (currentState == NewCurrentData.States.Swinging)
                    animationState = "Swinging_Anim";
                else if (currentState == NewCurrentData.States.Twisting)
                    animationState = "Twisting_Anim";
                else if (currentState == NewCurrentData.States.Rolling)
                    animationState = "Rolling_Anim";
            }

            public void ChangeAnimationState()
            {
                

                animator.Play(animationState);  
            }
        }
    }
}
