using UnityEngine;

public static class Vector3Extensions
{
	//Set Values
	public static Vector3 SetValues(this Vector3 vector, float? x = null, float? y = null, float? z = null)
	{
		return new Vector3(x ?? vector.x, y ?? vector.y, z ?? vector.z);
	}
	
	//Lerp Values
	public static Vector3 LerpValues(this Vector3 vector, Vector3 goalValues, float percent)
	{
		return new Vector3(Mathf.Lerp(vector.x, goalValues.x, percent), Mathf.Lerp(vector.y, goalValues.y, percent), Mathf.Lerp(vector.z, goalValues.z, percent));
	}
	
	//Lerp X
	public static Vector3 LerpX(this Vector3 vector, float xGoal, float percent)
	{
		return new Vector3(Mathf.Lerp(vector.x, xGoal, percent), vector.y, vector.z);
	}
	
	//Lerp Y
	public static Vector3 LerpY(this Vector3 vector, float yGoal, float percent)
	{
		return new Vector3(vector.x, Mathf.Lerp(vector.y, yGoal, percent), vector.z);
	}
	
	//Lerp Z
	public static Vector3 LerpZ(this Vector3 vector, float zGoal, float percent)
	{
		return new Vector3(vector.x, vector.y, Mathf.Lerp(vector.z, zGoal, percent));
	}
	
	//Get Direction To
	public static Vector3 GetDirection(this Vector3 from, Vector3 to)
	{
		return Vector3.Normalize(to - from);
	}
	
	//Drop to Vector2
	public static Vector2 DropToV2 (this Vector3 vector)
	{
		return new Vector2 (vector.x, vector.y);
	}
}
