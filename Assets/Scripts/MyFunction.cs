using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyFunction
{
	// �x���g�R���x�A�̒[
	[System.Serializable]
	public struct BezierEdge
	{
		public Vector2 point;
		public Vector2 control;
	}


	// �x�W�F�Ȑ���̓_���擾
	static public Vector2 GetPointOnBezierCurve(BezierEdge beginPoint, BezierEdge endPoint, float t)
	{
		// �꒼����ɂ���ꍇ�͒��_��Ԃ��悤�ɏ���������
		if (IsStraightLine(beginPoint, endPoint))
		{
			return Vector2.Lerp(beginPoint.point, endPoint.point, t);
		}

		// ����_ 1
		Vector2 control1 = beginPoint.point + beginPoint.control;
		// ����_ 2
		Vector2 control2 = endPoint.point + endPoint.control;

		// �n�_���琧��_ 1 �ւ̃��[�v
		Vector2 lerp1 = Vector2.Lerp(beginPoint.point, control1, t);
		// ����_ 1 ���琧��_ 2 �ւ̃��[�v
		Vector2 lerp2 = Vector2.Lerp(control1, control2, t);
		// ����_ 2 ����I�_�ւ̃��[�v
		Vector2 lerp3 = Vector2.Lerp(control2, endPoint.point, t);

		// ���[�v 1 ���烉�[�v 2 �ւ̃��[�v
		Vector2 lerp4 = Vector2.Lerp(lerp1, lerp2, t);
		// ���[�v 2 ���烉�[�v 3 �ւ̃��[�v
		Vector2 lerp5 = Vector2.Lerp(lerp2, lerp3, t);

		// ���_
		Vector2 bezierPoint = Vector2.Lerp(lerp4, lerp5, t);

		return bezierPoint;
	}

	// �꒼����ɑ��݂���
	static public bool IsStraightLine(BezierEdge edge1, BezierEdge edge2)
	{
		// edge2 �� edge1�̎n�_�̍ŒZ���W
		Vector2 p1 = edge2.point + (edge2.control * Vector2.Dot(edge1.point - edge2.point, edge2.control));
		// �ŒZ���W�� edge1 �̎n�_�̋���
		float d1 = Vector2.Distance(p1, edge1.point);

		// edge1 �� edge2�̎n�_�̍ŒZ���W
		Vector2 p2 = edge1.point + (edge1.control * Vector2.Dot(edge2.point - edge1.point, edge1.control));
		// �ŒZ���W�� edge2 �̎n�_�̋���
		float d2 = Vector2.Distance(p2, edge2.point);

		// �ǂ���̋�������������Έ꒼����ɂ���Ƃ������Ƃɂł���
		if (d1 <= 0.1f && d2 <= 0.1f)
		{
			return true;
		}

		return false;
	}

	// Ray�̌����n�_
	static public Vector2 IntersectionRay(Ray2D ray1, Ray2D ray2, float minDistance = 0.1f)
	{
		// ray2 �� ray1�̎n�_�̍ŒZ���W
		Vector2 verticalPoint = ray2.origin + (ray2.direction * Vector2.Dot(ray1.origin - ray2.origin, ray2.direction));

		// ray1�̎n�_ �� �����_ �̋����� 0.1f �ȉ��̏ꍇ�͒��_��Ԃ�
		if (Vector2.Distance(ray1.origin, verticalPoint) <= minDistance)
		{
			return Vector2.Lerp(ray1.origin, ray2.origin, 0.5f);
		}

		// ray �̓���
		float dot = Vector2.Dot(ray1.direction, ray2.direction);

		return verticalPoint + (-ray2.direction * dot);
	}

}
