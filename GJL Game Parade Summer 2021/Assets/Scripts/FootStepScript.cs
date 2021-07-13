using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStepScript : MonoBehaviour
{
    [SerializeField] ParticleSystem particles;
    [SerializeField] int groundLayer;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == groundLayer)
            particles.Play();
    }
}
