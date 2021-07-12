using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;

public class PlayerStates : MonoBehaviour
{
    [SerializeField] GameData data;
    [SerializeField] CurrentData current;

    [Header("Ground Check")]
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float terrainCheckRadius;

    // Update is called once per frame
    void Update()
    {
        if (current.state != CurrentData.States.Bouncing && current.state != CurrentData.States.UsingTrampoline && current.state != CurrentData.States.Rolling)
        {
            if (Physics2D.OverlapCircle(groundCheck.position, terrainCheckRadius, groundLayer))
                current.state = CurrentData.States.Grounded;
            else if (!Physics2D.OverlapCircle(groundCheck.position, terrainCheckRadius, groundLayer))
                current.state = CurrentData.States.Airborne;
        }
    }

    public void Bounced()
    {
        current.state = CurrentData.States.Bouncing;
        Timing.RunCoroutine(_BounceDuration(), Segment.FixedUpdate);
    }

    IEnumerator<float> _BounceDuration()
    {
        float timer = data.bounceDuration;
        while (timer > 0)
        {
            timer -= Time.fixedDeltaTime;
            yield return Timing.WaitForSeconds(Time.fixedDeltaTime);
            if (timer <= 0)
                break;
        }
        current.state = CurrentData.States.Airborne;
    }



    private void OnDrawGizmos()
    {
        if (terrainCheckRadius <= 0)
            return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheck.position, terrainCheckRadius);
    }
}
