using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepParticles : MonoBehaviour
{
    [SerializeField] NewGameData data;
    ParticleSystem particles;
    int groundLayer => data.groundLayerInt;

    private void Awake()
    {
        particles = GetComponentInChildren<ParticleSystem>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == groundLayer)
            particles.Play();
    }
}
