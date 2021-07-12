using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;

public class UseRoll : MonoBehaviour
{
    [SerializeField] GameData data;
    [SerializeField] CurrentData current;

    [SerializeField] CircleCollider2D bodyCollider;
    [SerializeField] Transform wallCheck;
    [SerializeField] BoxCollider2D rollCollider;

    private Vector2 originalWallCheckPosition;

    private void Awake()
    {
        originalWallCheckPosition = wallCheck.localPosition;
    }

    public void ExpendRoll()
    {
        if (!current.rollAvailable)
            return;

        Timing.RunCoroutine(_RollingDuration(), Segment.FixedUpdate);

    }

    IEnumerator<float> _RollingDuration()
    {
        current.rollAvailable = false;
        current.state = CurrentData.States.Rolling;
        SwitchColliders();

        float timer = data.rollDuration;
        while (timer > 0)
        {
            timer -= Time.fixedDeltaTime;
            yield return Timing.WaitForSeconds(Time.fixedDeltaTime);
            if (timer <= 0)
                break;
        }

        current.state = CurrentData.States.Grounded;
        SwitchColliders();

    }

    private void SwitchColliders()
    {
        if (current.state != CurrentData.States.Rolling)
        {
            bodyCollider.enabled = true;
            wallCheck.localPosition = originalWallCheckPosition;
            rollCollider.enabled = false;
        }
        else
        {
            bodyCollider.enabled = false;
            wallCheck.localPosition = wallCheck.localPosition.SetValues(y: wallCheck.localPosition.y - 0.3f);
            rollCollider.enabled = true;
        }
    }
}
