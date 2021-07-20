using UnityEngine;
[CreateAssetMenu(fileName = "New Game Data", menuName = "Game/NewData")]
public class NewGameData : ScriptableObject
{
    [Header("Movement")]
    public float moveSpeed;
    public float maxSpeed;

    [Header("Jump")]
    public float jumpForce;
    public float jumpDuration;

    [Header("Twist")]
    public float twistDuration;

    [Header("Roll")]
    public float rollSpeed;
    public float rollDuration;

    [Header("Swing")]
    public float swingReach;
    public float swingSpeed;
    [Range(0, 1)] public float swingAngle;

    [Header("Propell")]
    public float propelledSpeed;
    public float maxPropelledSpeed;
    public float propelledDuration;

    [Header("Layers")]
    public LayerMask groundLayer;
    public LayerMask trampolineLayer;
    public LayerMask swingLayer;
    public LayerMask exitLayer;
}
