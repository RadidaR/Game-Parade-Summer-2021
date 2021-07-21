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

    [Header("Bounce")]
    public float bounceForce;
    public float bounceDuration;

    [Header("Twist")]
    public float twistDuration;

    [Header("Roll")]
    public float rollSpeed;
    public float rollDuration;
    public float rollLineWidth;

    [Header("Swing")]
    public float swingReach;
    public float swingSpeed;
    [Range(0, 1)] public float swingAngle;
    public float swingLineWidth;

    [Header("Propell")]
    public float propelledSpeed;
    public float maxPropelledSpeed;
    public float propelledDuration;
    [Range(0, 1.5f)] public float propelledForce;

    [Header("Transitions")]
    public float transitionDelay;

    [Header("Layers")]
    [HideInInspector] public LayerMask groundLayer;
    [HideInInspector] public int groundLayerInt;
    [HideInInspector] public LayerMask trampolineLayer;
    [HideInInspector] public int trampolineLayerInt;
    [HideInInspector] public LayerMask swingLayer;
    [HideInInspector] public int swingLayerInt;
    [HideInInspector] public LayerMask exitLayer;
    [HideInInspector] public int exitLayerInt;
}
