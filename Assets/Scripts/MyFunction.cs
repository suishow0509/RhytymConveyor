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

	// Rayの交差地点
	static public Vector2 IntersectionRay(Ray2D ray1, Ray2D ray2)
	{
		// ray2 と ray1の始点の最短座標
		Vector2 verticalPoint = ray2.origin + (ray2.direction * Vector2.Dot(ray1.origin - ray2.origin, ray2.direction));

		// ray の内積
		float dot = Vector2.Dot(ray1.direction, ray2.direction);

		return verticalPoint + (-ray2.direction * dot);
	}

}
