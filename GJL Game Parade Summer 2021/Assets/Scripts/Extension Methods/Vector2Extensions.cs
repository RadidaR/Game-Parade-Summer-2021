using UnityEngine;

public static class Vector2Extensions
{
	//Set Values
	public static Vector2 SetValues(this Vector2 vector, float? x = null, float? y = null)
	{
		return new Vector2(x ?? vector.x, y ?? vector.y);
	}
	
	//Lerp Values
	public static Vector2 LerpValues(this Vector2 vector, Vector2 goalValues, float percent)
	{
		return new Vector2(Mathf.Lerp(vector.x, goalValues.x, percent), Mathf.Lerp(vector.y, goalValues.y, percent));
	}
	
	//Lerp X
	public static Vector2 LerpX(this Vector2 vector, float xGoal, float percent)
	{
		return new Vector2(Mathf.Lerp(vector.x, xGoal, percent), vector.y);
	}
	
	//Lerp Y
	public static Vector2 LerpY(this Vector2 vector, float yGoal, float percent)
	{
		return new Vector2(vector.x, Mathf.Lerp(vector.y, yGoal, percent));
	}
	
	//Get Direction
	public static Vector2 GetDirection(this Vector2 from, Vector2 to)
	{
		return new Vector2(to.x - from.x, to.y - from.y).normalized;
	}
	
	//Raise to Vector3
	public static Vector3 RaiseToV3(this Vector2 vector)
	{
		return new Vector3(vector.x, vector.y, 0f);
	}
}
