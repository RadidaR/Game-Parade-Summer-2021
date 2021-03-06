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
    void FixedUpdate()
    {
        if (current.state == CurrentData.States.Done)
            return;

        if (current.state == CurrentData.States.Grounded || current.state == CurrentData.States.Airborne)
        {
            if (Physics2D.OverlapCircle(groundCheck.position, terrainCheckRadius, groundLayer))
                current.state = CurrentData.States.Grounded;
            else if (!Physics2D.OverlapCircle(groundCheck.position, terrainCheckRadius, groundLayer))
                current.state = CurrentData.States.Airborne;
        }
    }

    public void Bounced()
    {
        if (current.state == CurrentData.States.Done)
            return;

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

        if (current.state != CurrentData.States.Swinging)
            current.state = CurrentData.States.Airborne;
    }

    public void Done()
    {
        Timing.KillCoroutines();
        current.state = CurrentData.States.Done;
    }



    private void OnDrawGizmosSelected()
    {
        if (terrainCheckRadius <= 0)
            return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheck.position, terrainCheckRadius);
    }
}
