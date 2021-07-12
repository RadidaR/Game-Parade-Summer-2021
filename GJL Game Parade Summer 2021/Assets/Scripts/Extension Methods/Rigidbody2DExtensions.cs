using UnityEngine;

public static class Rigidbody2DExtensions
{
	public static Vector2 SetVelocity(this Rigidbody2D rigidBody, float? x = null, float? y = null)
	{
		return rigidBody.velocity.SetValues(x ?? rigidBody.velocity.x, y ?? rigidBody.velocity.y);
	}
}
