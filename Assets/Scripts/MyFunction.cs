using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyFunction
{
	// �x�W�F�Ȑ���̓_���擾
	static public Vector3 GetPointOnBezierCurve(Vector3 beginPoint, Vector3 endPoint, Vector3 controlPoint, float t)
	{
		// �n�_��
		Vector3 beginLerp = Vector3.Lerp(beginPoint, controlPoint, t);
		// �I�_��
		Vector3 endLerp = Vector3.Lerp(controlPoint, endPoint, t);
		// ���_
		Vector3 bezierPoint = Vector3.Lerp(beginLerp, endLerp, t);

		return bezierPoint;
	}

	//// Ray�̌����n�_
	//static public Vector2 IntersectionRay(Ray2D ray1, Ray2D ray2)
	//{
	//	// 
	//}

}
