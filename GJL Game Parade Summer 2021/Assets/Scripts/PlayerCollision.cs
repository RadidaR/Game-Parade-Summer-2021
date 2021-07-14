using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] CurrentData current;
    [SerializeField] GameData data;
    [SerializeField] Transform wallCheck;
    //[SerializeField] Transform groundCheck;

    [SerializeField] GameEvent eWallAhead;
    [SerializeField] GameEvent eSteppedOnTrampoline;

    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerMask swingLayer;
    [SerializeField] int trampolineLayer;
    [SerializeField] float terrainCheckRadius;

    private void FixedUpdate()
    {
        //checks for a wall ahead
        if (Physics2D.OverlapCircle(wallCheck.position, terrainCheckRadius, groundLayer))
            eWallAhead.Raise();

        current.swingInReach = Physics2D.OverlapCircle(transform.position, data.swingReach, swingLayer);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == trampolineLayer)
            eSteppedOnTrampoline.Raise();
    }

    private void OnDrawGizmosSelected()
    {
        if (terrainCheckRadius > 0)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(wallCheck.position, terrainCheckRadius);
        }

        if (data.swingReach > 0)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, data.swingReach);
        }
    }
}
