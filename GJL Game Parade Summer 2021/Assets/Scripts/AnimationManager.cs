using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    [SerializeField] GameData data;
    [SerializeField] CurrentData current;

    [SerializeField] Animator animator;
    //[SerializeField] AnimationClip[] animations;

    [SerializeField] CurrentData.States animationState;
    [SerializeField] bool moving;
    [SerializeField] bool jumping;
    [SerializeField] float timer;
    [SerializeField] float jumpTimer;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {

        if (animationState == CurrentData.States.Grounded)
        {
            animator.SetBool("Moving", moving);

            if (current.moveInput == 0)
            {
                moving = false;
            }
            else
                moving = true;
        }

        if (timer > 0)
            timer -= Time.deltaTime;

            SwitchAnimationState();
    }

    void SwitchAnimationState()
    {
        animationState = current.state;
        if (animationState == CurrentData.States.Grounded)
            return;
        else if (animationState == CurrentData.States.Airborne)
        {
            if (timer > 0)
                return;
            animator.Play("Airborne_Anim");
        }
        else if (animationState == CurrentData.States.Bouncing || animationState == CurrentData.States.Propelled)
            animator.Play("Propelled_Anim");
        else if (animationState == CurrentData.States.Swinging)
            animator.Play("Swinging_Anim");

    }

    public void PlayAnimation(string animation)
    {
        animator.Play(animation);
    }

    public void Jumped()
    {
        timer = jumpTimer;
    }


}
