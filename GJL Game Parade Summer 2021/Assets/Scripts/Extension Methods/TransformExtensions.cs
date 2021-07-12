using UnityEngine;

public static class TransformExtensions 
{
	//Set Position
	public static Vector3 SetPosition(this Transform transform, float? x = null, float? y = null, float? z = null)
	{
		return transform.position.SetValues(x ?? transform.position.x, y ?? transform.position.y, z ?? transform.position.z);
	}
	
	//Set Rotation
	public static Quaternion SetRotation(this Transform transform, float? x = null, float? y = null, float? z = null, float? w = null)
	{
		return new Quaternion(x ?? transform.rotation.x, y ?? transform.rotation.y, z ?? transform.rotation.z, w ?? transform.rotation.w);
	}
	
	//Set 2D Rotation
	public static Quaternion Set2DRotation(this Transform transform, float rotation)
	{
		return transform.rotation.SetEulerValues(z: rotation);
	}  
	
	//Set Scale
	public static Vector3 SetScale(this Transform transform, float? x = null, float? y = null, float? z = null, float? all = null)
	{
		if (all == null)
		{
			return transform.localScale.SetValues(x ?? transform.localScale.x, y ?? transform.localScale.y, z ?? transform.localScale.z);
		}
		else
		{
			return transform.localScale.SetValues(x: all, y: all, z: all);
		}
	}
	
	//Get Direction To
	public static Vector3 GetDirection(this Transform from, Transform to)
	{
		return from.position.GetDirection(to.position);
	}
	
	//Get 2D Direction
	public static Vector2 Get2DDirection(this Transform from, Transform to)
	{
		Vector2 from2D = from.position.DropToV2();
		Vector2 to2D = to.position.DropToV2();
		return from2D.GetDirection(to2D);
	}
	
	//Lerp Position
	public static Vector3 LerpPosition(this Transform transform, Vector3 goalPosition, float percent)
	{
		return transform.position.LerpValues(goalPosition, percent);
	}
	
	//Lerp X Position
	public static Vector3 LerpPositionX(this Transform transform, float xGoal, float percent)
	{
		return transform.position.LerpX(xGoal, percent);
	}
	
	//Lerp Y Position
	public static Vector3 LerpPositionY(this Transform transform, float yGoal, float percent)
	{
		return transform.position.LerpY(yGoal, percent);
	}
	
	//Lerp Z Position
	public static Vector3 LerpPositionZ(this Transform transform, float zGoal, float percent)
	{
		return transform.position.LerpZ(zGoal, percent);
	}
	
	//Lerp Rotation
	public static Quaternion LerpEulerRotation(this Transform transform, Quaternion goalRotation, float percent)
	{
		return transform.rotation.LerpEulerValues(goalRotation, percent);
	}
	
	//Lerp X Rotation
	public static Quaternion LerpEulerRotationX(this Transform transform, float xGoal, float percent)
	{
		return transform.rotation.LerpEulerX(xGoal, percent);
	}
	
	//Lerp Y Rotation
	public static Quaternion LerpEulerRotationY(this Transform transform, float yGoal, float percent)
	{
		return transform.rotation.LerpEulerY(yGoal, percent);
	}
	
	//Lerp Z Rotation
	public static Quaternion LerpEulerRotationZ(this Transform transform, float zGoal, float percent)
	{
		return transform.rotation.LerpEulerZ(zGoal, percent);
	}
	
	//Lerp Scale To Vector
	public static Vector3 LerpScaleToVector(this Transform transform, Vector3 goalScale, float percent)
	{
		return transform.localScale.LerpValues(goalScale, percent);
	}
	
	//Lerp Scale All
	public static Vector3 LerpScaleAll(this Transform transform, float all, float percent)
	{
		return transform.localScale.LerpValues(new Vector3(all, all, all), percent);
	}
	
	//Lerp X Scale
	public static Vector3 LerpScaleX(this Transform transform, float xGoal, float percent)
	{
		return transform.localScale.LerpX(xGoal, percent);
	}
	
	//Lerp Y Scale
	public static Vector3 LerpScaleY(this Transform transform, float yGoal, float percent)
	{
		return transform.localScale.LerpY(yGoal, percent);
	}
	
	//Lerp Z Scale
	public static Vector3 LerpScaleZ(this Transform transform, float zGoal, float percent)
	{
		return transform.localScale.LerpZ(zGoal, percent);
	}
}
