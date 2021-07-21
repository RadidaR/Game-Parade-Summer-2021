using UnityEngine;
[CreateAssetMenu(fileName = "New Current Data", menuName = "Game/NewCurrent")]
public class NewCurrentData : ScriptableObject
{
    [Header("Level")]
    public int level;
    public bool trampolineAvailable;
    public bool rollAvailable;
    public bool swingAvailable;
    public bool swingInReach;
    public int abilitiesUsed;
    public bool exitReached;

    [Header("Main Menu")]
    public bool mainMenu;

    [Header("Player State")]
    public States state;
    public enum States { Neutral, Idle, Running, Jumping, Bouncing, Propelled, Airborne, Twisting, Rolling, Swinging, Done };

    [Header("Player Input")]
    [Range(-1, 1)] public float moveInput;
    [Range(0, 1)] public float swingInput;

    [Header("Player Movement")]
    [Range(-1, 1)] public int direction;
    [HideInInspector] public float originalGravity;

    [Header("Swinging")] 
    [HideInInspector] public Vector2 hookPosition;
    [HideInInspector] public Vector2 swingPosition;
    [HideInInspector] public Quaternion swingRotation;

}
