using UnityEngine;
[CreateAssetMenu(fileName = "Game Data", menuName = "Game/Data")]
public class GameData : ScriptableObject
{
    public float moveSpeed;
    public float bounceForce;

    public float useTrampolineDuration;
    public float bounceDuration;

    public float rollSpeed;
    public float rollDuration;

    public float useSwingDuration;
    public float swingReach;
    public float ropeLength;
    public float swingForceX;
    public float swingForceY;
    public float swingVelocityLimit;

    public float swingSpeed;
    public float swingAngle;
}
