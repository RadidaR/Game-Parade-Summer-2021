using UnityEngine;

public static class QuaternionExtensions
{
	//Set Values
	public static Quaternion SetEulerValues(this Quaternion quaternion, float? x = null, float? y = null, float? z = null)
	{
		return Quaternion.Euler(x ?? quaternion.x, y ?? quaternion.y, z ?? quaternion.z);
	}
	
	//Lerp Values
	public static Quaternion LerpEulerValues(this Quaternion quaternion, Quaternion goalValues, float percent)
	{
		return Quaternion.Euler(Mathf.Lerp(quaternion.eulerAngles.x, goalValues.eulerAngles.x, percent), Mathf.Lerp(quaternion.eulerAngles.y, goalValues.eulerAngles.y, percent), Mathf.Lerp(quaternion.eulerAngles.z, goalValues.eulerAngles.z, percent));
	}
	
	//Lerp X
	public static Quaternion LerpEulerX(this Quaternion quaternion, float xGoal, float percent)
	{
		return Quaternion.Euler(Mathf.Lerp(quaternion.eulerAngles.x, xGoal, percent), quaternion.eulerAngles.y, quaternion.eulerAngles.z);
	}
	
	//Lerp Y
	public static Quaternion LerpEulerY(this Quaternion quaternion, float yGoal, float percent)
	{
		return Quaternion.Euler(quaternion.eulerAngles.x, Mathf.Lerp(quaternion.eulerAngles.y, yGoal, percent), quaternion.eulerAngles.z);
	}
	
	//Lerp Z
	public static Quaternion LerpEulerZ(this Quaternion quaternion, float zGoal, float percent)
	{
		return Quaternion.Euler(quaternion.eulerAngles.x, quaternion.eulerAngles.y, Mathf.Lerp(quaternion.eulerAngles.z, zGoal, percent));
	}
	
	//Drop to Vector3
	public static Vector3 DropToV3(this Quaternion quaternion)
	{
		return new Vector3(quaternion.x, quaternion.y, quaternion.z);
	}
}
