using UnityEngine;
[CreateAssetMenu(fileName = "Current Data", menuName = "Game/Current")]
public class CurrentData : ScriptableObject
{
    [Header("Level Specific")]
    public int currentAttempt;
    public bool trampolineAvailable;
    public bool rollAvailable;
    public bool swingAvailable;
    public int abilitiesUsed;

    [Header("Player Movement")]
    public bool movingRight;
    public int direction;
    public bool swingInReach;
    public Vector2 hookPosition;
    //public bool isGrounded;
    //public bool isBouncing;
    //public bool isAirborne;
    //public bool isSwinging;
    //public bool isDashing;

    [Header("Player State")]
    public States state;
    public enum States { Grounded, Bouncing, Airborne, Rolling, Swinging, Dead, UsingTrampoline};

    [Header("Input")]
    [Range(0, 1)] public float swingInput;
}
