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
}
