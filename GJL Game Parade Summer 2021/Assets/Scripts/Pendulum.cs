using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pendulum : MonoBehaviour
{
    Rigidbody2D rigidBody;
    [SerializeField] NewCurrentData current;
    [SerializeField] NewGameData data;

    public bool movingClockwise;
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    { 
        Move();

        if (Mathf.Abs(transform.rotation.z) < 0.05f)
            audioManager.PlaySound("SFX_Swing");
    }

    public void ChangeMoveDir()
    {
        if (transform.rotation.z > data.swingAngle)
            movingClockwise = true;
        if (transform.rotation.z < -data.swingAngle)
            movingClockwise = false;

    }

    public void Move()
    {
        ChangeMoveDir();
        if (!movingClockwise)
        {
            if (transform.rotation.z < data.swingAngle * 0.5f)
                rigidBody.AddTorque(data.swingSpeed);
            else
                movingClockwise = true;
        }

        if (movingClockwise)
        {
            if (transform.rotation.z > -data.swingAngle * 0.5f)
                rigidBody.AddTorque(-data.swingSpeed);
            else
                movingClockwise = false;
        }
    }
}