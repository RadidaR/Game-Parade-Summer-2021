using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] CurrentData current;
    [SerializeField] Transform wallCheck;
    //[SerializeField] Transform groundCheck;

    [SerializeField] GameEvent eWallAhead;
    [SerializeField] GameEvent eSteppedOnTrampoline;

    [SerializeField] LayerMask groundLayer;
    [SerializeField] int trampolineLayer;
    [SerializeField] float terrainCheckRadius;

    private void Update()
    {
        //checks for a wall ahead
        if (Physics2D.OverlapCircle(wallCheck.position, terrainCheckRadius, groundLayer))
            eWallAhead.Raise();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == trampolineLayer)
            eSteppedOnTrampoline.Raise();
    }

    private void OnDrawGizmos()
    {
        if (terrainCheckRadius <= 0)
            return;
        Gizmos.color = Color.yellow;        
        Gizmos.DrawWireSphere(wallCheck.position, terrainCheckRadius);
    }
}
