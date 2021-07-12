using UnityEngine;
[CreateAssetMenu(fileName = "Current Data", menuName = "Game/Current")]
public class CurrentData : ScriptableObject
{
    [Header("Level Specific")]
    public int currentAttempt;
    public bool trampolineAvailable;
    public bool dashAvailable;
    public bool swingAvailable;

    [Header("Player Movement")]
    public bool movingRight;
    public int direction;
    //public bool isGrounded;
    //public bool isBouncing;
    //public bool isAirborne;
    //public bool isSwinging;
    //public bool isDashing;

    public States state;
    public enum States { Grounded, Bouncing, Airborne, Rolling, Swinging, Dead, UsingTrampoline};
}
