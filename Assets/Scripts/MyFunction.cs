using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyFunction
{
	// ベジェ曲線上の点を取得
	static public Vector3 GetPointOnBezierCurve(Vector3 beginPoint, Vector3 endPoint, Vector3 controlPoint, float t)
	{
		// 始点側
		Vector3 beginLerp = Vector3.Lerp(beginPoint, controlPoint, t);
		// 終点側
		Vector3 endLerp = Vector3.Lerp(controlPoint, endPoint, t);
		// 中点
		Vector3 bezierPoint = Vector3.Lerp(beginLerp, endLerp, t);

		return bezierPoint;
	}

	//// Rayの交差地点
	//static public Vector2 IntersectionRay(Ray2D ray1, Ray2D ray2)
	//{
	//	// 
	//}

}
