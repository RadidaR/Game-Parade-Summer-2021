using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrampolineAdjustment : MonoBehaviour
{
    [SerializeField] LayerMask groundLayer;

    private void Update()
    {
        while (Physics2D.OverlapCircle(transform.position, 0.5f, groundLayer))
        {
            transform.position = transform.position.SetValues(y: transform.position.y + 0.1f);
            if (!Physics2D.OverlapCircle(transform.position, 1f, groundLayer))
                return;
        }

        while (!Physics2D.OverlapCircle(transform.position, 1f, groundLayer))
        {
            transform.position = transform.position.SetValues(y: transform.position.y - 0.1f);
            if (Physics2D.OverlapCircle(transform.position, 1f, groundLayer))
                return;
        }

    }
}
