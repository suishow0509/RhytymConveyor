using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyFunction
{
	// ベルトコンベアの端
	[System.Serializable]
	public struct BezierEdge
	{
		public Vector2 point;
		public Vector2 control;
	}


	// ベジェ曲線上の点を取得
	static public Vector2 GetPointOnBezierCurve(BezierEdge beginPoint, BezierEdge endPoint, float t)
	{
		// 一直線上にある場合は中点を返すように処理を書く
		if (IsStraightLine(beginPoint, endPoint))
		{
			return Vector2.Lerp(beginPoint.point, endPoint.point, t);
		}

		// 制御点 1
		Vector2 control1 = beginPoint.point + beginPoint.control;
		// 制御点 2
		Vector2 control2 = endPoint.point + endPoint.control;

		// 始点から制御点 1 へのラープ
		Vector2 lerp1 = Vector2.Lerp(beginPoint.point, control1, t);
		// 制御点 1 から制御点 2 へのラープ
		Vector2 lerp2 = Vector2.Lerp(control1, control2, t);
		// 制御点 2 から終点へのラープ
		Vector2 lerp3 = Vector2.Lerp(control2, endPoint.point, t);

		// ラープ 1 からラープ 2 へのラープ
		Vector2 lerp4 = Vector2.Lerp(lerp1, lerp2, t);
		// ラープ 2 からラープ 3 へのラープ
		Vector2 lerp5 = Vector2.Lerp(lerp2, lerp3, t);

		// 中点
		Vector2 bezierPoint = Vector2.Lerp(lerp4, lerp5, t);

		return bezierPoint;
	}

	// 一直線上に存在する
	static public bool IsStraightLine(BezierEdge edge1, BezierEdge edge2)
	{
		// edge2 と edge1の始点の最短座標
		Vector2 p1 = edge2.point + (edge2.control * Vector2.Dot(edge1.point - edge2.point, edge2.control));
		// 最短座標と edge1 の始点の距離
		float d1 = Vector2.Distance(p1, edge1.point);

		// edge1 と edge2の始点の最短座標
		Vector2 p2 = edge1.point + (edge1.control * Vector2.Dot(edge2.point - edge1.point, edge1.control));
		// 最短座標と edge2 の始点の距離
		float d2 = Vector2.Distance(p2, edge2.point);

		// どちらの距離も小さければ一直線上にあるということにできる
		if (d1 <= 0.1f && d2 <= 0.1f)
		{
			return true;
		}

		return false;
	}

	// Rayの交差地点
	static public Vector2 IntersectionRay(Ray2D ray1, Ray2D ray2, float minDistance = 0.1f)
	{
		// ray2 と ray1の始点の最短座標
		Vector2 verticalPoint = ray2.origin + (ray2.direction * Vector2.Dot(ray1.origin - ray2.origin, ray2.direction));

		// ray1の始点 と 垂直点 の距離が 0.1f 以下の場合は中点を返す
		if (Vector2.Distance(ray1.origin, verticalPoint) <= minDistance)
		{
			return Vector2.Lerp(ray1.origin, ray2.origin, 0.5f);
		}

		// ray の内積
		float dot = Vector2.Dot(ray1.direction, ray2.direction);

		return verticalPoint + (-ray2.direction * dot);
	}

}
