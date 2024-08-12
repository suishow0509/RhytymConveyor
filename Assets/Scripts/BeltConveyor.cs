using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static UnityEditor.PlayerSettings;

public class BeltConveyor : MonoBehaviour
{
    [Header("----- �V�X�e�� -----")]
    [Header("�㏑��������")]
    [SerializeField] private bool m_permitOverwrite = true;

    [Header("----- �`�� -----")]
    [Header("���C�������_���[")]
    [SerializeField] private LineRenderer m_lineRenderer = null;

    [Header("�`��̕�����(�ꃁ������)")]
    [SerializeField] private int m_beltDivision = 5;


    [Header("----- �x���g�R���x�A -----")]
    [Header("�x���g�R���x�A�̃X�s�[�h(/s)"), Min(0.0f)]
    [SerializeField] private float m_conveyorSpeed = 1.0f;

    [Header("�x���g�R���x�A�̎n�_�ƏI�_")]
    [SerializeField] private MyFunction.BezierEdge m_conveyorBegin;
    [SerializeField] private MyFunction.BezierEdge m_conveyorEnd;
    [Header("�x���g�R���x�A�̒���")]
    [SerializeField] private float m_beltLength = 1.0f;
    [Header("�Ȑ����\������_")]
    [SerializeField] private Vector3[] m_points = new Vector3[0];

	[Header("�����Ă��錳�̃x���g�R���x�A")]
	[SerializeField] private BeltConveyor m_fromConveyor = null;
	[Header("�o�Ă�����̃x���g�R���x�A")]
	[SerializeField] private BeltConveyor m_toConveyor = null;

    [Header("----- �m�[�c -----")]
    [Header("���ݍڂ��Ă���m�[�c")]
    [SerializeField] private List<RhythmMaterial> m_materials = new();

    [Header("�f�o�b�O")]
    [SerializeField] private bool m_debug = true;
    [Header("�x���g�R���x�A�̒ʉߎ���")]
    [SerializeField] private float m_passTime;
    [Header("�x���g�R���x�A�ւ̓��B����")]
    [SerializeField] private float m_arriveTime;
    [Header("���ʗp�̐F")]
    [SerializeField] private Color m_colorBegin = Color.green;
    [SerializeField] private Color m_colorEnd = Color.red;



	// Start is called before the first frame update
	void Start()
    {
        // �O�Ƀx���g�R���x�A������ꍇ�͎n�_��ݒ肷��
		if (FromConveyor)
        {
            // �n�_�̐ݒ�
            ConveyorBeginPoint = FromConveyor.ConveyorEnd.point;
		}

        // �x���g�R���x�A����
        BakeBeltConveyor();

	}

    // Update is called once per frame
    void Update()
    {
        // �m�[�c�ړ�
        MoveMaterials();
	}

    // ****************************** �x���g�R���x�A�̏��� ****************************** //
    // �x���g�R���x�A�̒��_����
    [ContextMenu("BakeBeltConveyor")]
	public void BakeBeltConveyor()
	{
		// �x���g�R���x�A�̒������}���n�b�^�������ŋ��߂�
		m_beltLength = Mathf.Abs(m_conveyorBegin.point.x - m_conveyorEnd.point.x) + Mathf.Abs(m_conveyorBegin.point.y - m_conveyorEnd.point.y);

		// 1�O�̍��W
		Vector2 prePosition = m_conveyorBegin.point;
		// �g�[�^���̕�����
		int division = m_beltDivision * (int)m_beltLength;
		// �x���g�R���x�A�̒���������
		m_beltLength = 0;
        // �_�̃��X�g������
        m_points = new Vector3[division + 1];
        // ���_�ݒ�
        for (int i = 0; i <= division; i++)
        {
            // ����
            float t = (float)i / division;
            // ���W�̌v�Z
            Vector2 pos = MyFunction.GetPointOnBezierCurve(m_conveyorBegin, m_conveyorEnd, t);
            // ���W�ݒ�
            m_points[i] = pos;
            // �x���g�R���x�A�̒������Z
            m_beltLength += Vector2.Distance(prePosition, pos);
            // �O�̍��W��ݒ肷��
            prePosition = pos;
        }

		// �`��̐ݒ�
		if (m_lineRenderer)
		{
			// ���_���̐ݒ�
			m_lineRenderer.positionCount = division + 1;

			// �`����W�ݒ�
			m_lineRenderer.SetPositions(m_points);
		}

	}

	// �x���g�R���x�A�𗬂�鎞��
	public float GetBeltPassTime()
    {
        // �X�s�[�h�� 0 �ȏ�
        if (m_conveyorSpeed > 0.0f)
        {
			return m_beltLength / m_conveyorSpeed;
		}
		return 0.0f;
    }

    // ���݂̃x���g�R���x�A�ɂ��ǂ蒅���܂ł̎���
    public float GetCurrentLocationTime()
    {
        // �ЂƂO�Ƀx���g�R���x�A������
        if (m_fromConveyor)
        {
            // �ЂƂO�̃x���g�R���x�A�̎��Ԃ����Z���Ă���
            return m_fromConveyor.GetCurrentLocationTime() + m_fromConveyor.GetBeltPassTime();
        }
        else
        {
            // �x���g�R���x�A�̐�[������A�����ɂ��ǂ蒅���܂ł̎��Ԃ� 0 �ł���
            return 0.0f;
        }
    }

	// ****************************** �m�[�c�̏��� ****************************** //
	// �m�[�c�̒ǉ�
	public void AddMaterial(RhythmMaterial material)
    {
        m_materials.Add(material);
    }
    // �m�[�c�폜
    public void RemoveMaterial(RhythmMaterial material)
    {
        m_materials.Remove(material);
    }
    // �m�[�c�̔j��
    public void DestroyMaterial(RhythmMaterial material)
    {
        m_materials.Remove(material);
		Destroy(material.gameObject);
	}
	// �m�[�c�̈ړ�
	public void MoveMaterials()
	{
		if (m_debug)
		{
			// �\���m�F
			Debug.DrawRay(m_conveyorBegin.point, m_conveyorBegin.control, m_colorBegin);
			Debug.DrawRay(m_conveyorEnd.point, m_conveyorEnd.control, m_colorEnd);
            // ����
			m_passTime = GetBeltPassTime();
			m_arriveTime = GetCurrentLocationTime();
		}

		// �֐����ŗv�f���폜����\�������邽�ߖ������珈��
		for (int i = m_materials.Count - 1; i >= 0; i--)
		{
			MoveMaterial(m_materials[i]);
		}
	}
    // �m�[�c�̈ړ�����
    public float GetMaterialTime(RhythmMaterial material)
    {
        // �ʉߎ���
        float passTime = GetBeltPassTime();
        // �ʉߎ��Ԃ� 0 �̏ꍇ�̓[�����Z�ɂȂ邽�ߏ������Ȃ�
        if (passTime <= 0.0f)
        {
            return 0.0f;
        }
		// ����
		return (material.Timer - GetCurrentLocationTime()) / passTime;
	}
	// �m�[�c�̈ړ�
	public virtual void MoveMaterial(RhythmMaterial material)
	{
		// ����
		float t = GetMaterialTime(material);

		// ���Ԃ� 1 �ȏ�
		if (t >= 1.0f)
		{
			NextConveyor(material);
            return;
		}

		// ���W
		Vector2 pos = GetMaterialPosition(t)/*MyFunction.GetPointOnBezierCurve(m_conveyorBegin, m_conveyorEnd, t)*/;
		material.transform.position = pos;

	}
    // �m�[�c�̈ʒu�擾
    public Vector3 GetMaterialPosition(float time)
    {
        // ���݂̎��Ԃɉ������x���g�R���x�A�̒����v�Z
        float length = m_beltLength * time;
        // �v�Z�p�̒���
        float l = 0.0f;
		// 1�O�̍��W
		Vector2 prePosition = m_conveyorBegin.point;
        // �g�p���钸�_�̓Y�������擾
        int index = 0;
        // ���_�Ԃ̋���
        float distance = 0.0f;
		for (int i = 0; i < m_points.Length; i++)
        {
			// ���W�ݒ�
			Vector3 pos = m_points[i];
            // ���_�Ԃ̋���
            distance = Vector2.Distance(prePosition, pos);
			// ���_[i]�܂ł̒��������ݒn�̒����ȏ�
			if (l + distance >= length)
            {
                // �ЂƂO���n�_�Ƃ��Ďg�p�������Y����
                index = i - 1;
                break;
            }
			// �x���g�R���x�A�̒������Z
			l += distance;
			// �O�̍��W��ݒ肷��
			prePosition = pos;
		}
		// �n�_
		Vector3 begin = m_points[index];
        // �I�_
        Vector3 end = m_points[index + 1];
        // �����v�Z
        float t = (length - l) / distance;

        // �ʒu�v�Z
        return Vector3.Lerp(begin, end, t);

    }
	// �}�e���A�������̃x���g�R���x�A�Ɉڂ�
	public virtual void NextConveyor(RhythmMaterial material)
	{
		// ���̃x���g�R���x�A���Ȃ�
		if (ToConveyor == null)
			return;

		// �}�e���A���̐ݒ�
		ToConveyor.AddMaterial(material);
		// ���݂̃x���g�R���x�A����폜
		RemoveMaterial(material);

    }
    // �q�����Ă���x���g�R���x�A�̃m�[�c���擾����
    public List<RhythmMaterial> GetMaterialsConnectingBelt()
    {
        return GetPreviousMaterials().Concat(GetBackwardMaterials()).Distinct().ToList();
    }
    // �O���̃x���g�R���x�A�̃m�[�c�擾
    public List<RhythmMaterial> GetPreviousMaterials()
    {
        // �O���Ƀx���g�R���x�A������
        if (FromConveyor)
        {
            return FromConveyor.GetPreviousMaterials().Concat(RhythmMaterials).ToList();
        }
        // ��Ԓ[
        else
        {
            return RhythmMaterials;
        }
    }
    // ����̃x���g�R���x�A�̃m�[�c�擾
    public List<RhythmMaterial> GetBackwardMaterials()
    {
        // ����Ƀx���g�R���x�A������
        if (ToConveyor)
        {
            return ToConveyor.GetBackwardMaterials().Concat(RhythmMaterials).ToList();
        }
        else
        {
            return RhythmMaterials;
        }
    }


	// ****************************** �v���p�e�B ****************************** //
	// �㏑��������
	public bool PermitOverwrite
    {
        get { return m_permitOverwrite; }
    }

    // �A�����x
    public float ConveyorSpeed
	{
		get { return m_conveyorSpeed; }
		set { m_conveyorSpeed = value; }
	}

	// �n�_
	public MyFunction.BezierEdge ConveyorBegin
    {
        get { return m_conveyorBegin; }
        set { m_conveyorBegin = value; }
    }
    public Vector2 ConveyorBeginPoint
    {
        set { m_conveyorBegin.point = value; }
    }
    // �I�_
    public MyFunction.BezierEdge ConveyorEnd
    {
        get { return m_conveyorEnd; }
        set { m_conveyorEnd = value; }
    }
    public Vector2 ConveyorEndPoint
    {
        set { m_conveyorEnd.point = value; }
    }

    // ���̃x���g�R���x�A
    public BeltConveyor FromConveyor
    {
        get { return m_fromConveyor; }
        set { m_fromConveyor = value; }
    }
    // ��̃x���g�R���x�A
    public BeltConveyor ToConveyor
    {
        get { return m_toConveyor; }
        set { m_toConveyor = value; }
    }

    // �m�[�c
    public List<RhythmMaterial> RhythmMaterials
    {
        get { return m_materials; }
    }



}
