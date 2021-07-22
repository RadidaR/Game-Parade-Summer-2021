using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepParticles : MonoBehaviour
{
    [SerializeField] NewGameData data;
    ParticleSystem particles;
    int groundLayer => data.groundLayerInt;
    AudioManager audioManager;

    private void Awake()
    {
        particles = GetComponentInChildren<ParticleSystem>();
        audioManager = FindObjectOfType<AudioManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == groundLayer)
        {
            audioManager.PlaySound("SFX_Step");
            particles.Play();
        }
    }
}
