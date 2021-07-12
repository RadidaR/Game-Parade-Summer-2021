using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] CurrentData current;
    [SerializeField] Transform wallCheck;
    [SerializeField] Transform groundCheck;

    [SerializeField] GameEvent eWallAhead;
    [SerializeField] GameEvent eSteppedOnTrampoline;

    [SerializeField] LayerMask groundLayer;
    [SerializeField] int trampolineLayer;

    private void Update()
    {
        if (Physics2D.OverlapCircle(wallCheck.position, 0.1f, groundLayer))
            eWallAhead.Raise();


        current.isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
        current.isAirborne = !Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);

        if (current.isGrounded)
            current.isBouncing = false;
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.layer == trampolineLayer)
    //    {
    //        eSteppedOnTrampoline.Raise();
    //    }
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == trampolineLayer)
        {
            eSteppedOnTrampoline.Raise();
        }
    }
}
