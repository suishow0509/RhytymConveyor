using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeltConveyor : MonoBehaviour
{
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
    [Header("�[�̃x�N�g��")]
    [SerializeField] private Vector2 m_conveyorVectorEnd = Vector2.down;
    // �x���g�R���x�A�̒���
    private float m_conveyorLength = 1.0f;
    //// �x�W�F�Ȑ��̐���_
    //private Vector2 m_controlPoint = Vector2.zero;

	[Header("�����Ă��錳�̃x���g�R���x�A")]
	[SerializeField] private BeltConveyor m_fromConveyor = null;
	[Header("�o�Ă�����̃x���g�R���x�A")]
	[SerializeField] private BeltConveyor m_toConveyor = null;

    [Header("����_(�m�F�p)")]
    [SerializeField] private GameObject m_controlObject = null;

    [Header("----- �m�[�c -----")]
    [Header("���ݍڂ��Ă���m�[�c")]
    [SerializeField] private List<RhythmMaterial> m_materials = new();


	// Start is called before the first frame update
	void Start()
    {
        // �O�Ƀx���g�R���x�A������ꍇ�͎n�_��ݒ肷��
		if (FromConveyor)
        {
            // �n�_�̐ݒ�
            ConveyorBeginPoint = FromConveyor.ConveyorEnd.point;
		}

        // �\���m�F
        if (m_controlObject)
        {
            Instantiate(m_controlObject, m_conveyorBegin.point + m_conveyorBegin.control, Quaternion.identity);
            Instantiate(m_controlObject, m_conveyorEnd.point + m_conveyorEnd.control, Quaternion.identity);
        }

        // �x���g�R���x�A����
        BakeBeltConveyor();

	}

    // Update is called once per frame
    void Update()
    {
        MoveMaterials();
	}

    // ****************************** �x���g�R���x�A�̏��� ****************************** //
	// �x���g�R���x�A�̒��_����
	public void BakeBeltConveyor()
	{
		// �x���g�R���x�A�̒������}���n�b�^�������ŋ��߂�
		m_conveyorLength = Mathf.Abs(m_conveyorBegin.point.x - m_conveyorEnd.point.x) + Mathf.Abs(m_conveyorBegin.point.y - m_conveyorEnd.point.y);

		// �`��̐ݒ�
		if (m_lineRenderer)
		{
			// 1�O�̍��W
			Vector2 prePosition = m_conveyorBegin.point;
			// �g�[�^���̕�����
			int division = m_beltDivision * (int)m_conveyorLength;
			// ���_���̐ݒ�
			m_lineRenderer.positionCount = division + 1;
			for (int i = 0; i <= division; i++)
			{
				// ����
				float t = (float)i / division;
				// ���W�̌v�Z
				Vector2 pos = MyFunction.GetPointOnBezierCurve(m_conveyorBegin, m_conveyorEnd, t);

				// �`����W�ݒ�
				m_lineRenderer.SetPosition(i, pos);

				// ���������߂�
				m_conveyorLength += Vector2.Distance(prePosition, pos);
				// �O�̍��W��ݒ肷��
				prePosition = pos;
			}
		}

	}

	// �x���g�R���x�A�𗬂�鎞��
	public float GetBeltPassTime()
    {
        // �X�s�[�h�� 0 �ȏ�
        if (m_conveyorSpeed > 0.0f)
        {
			return m_conveyorLength / m_conveyorSpeed;
		}
		return 0.0f;
    }

    // ���ݒn�܂ł̎���
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
		return (material.Timer - GetCurrentLocationTime()) / GetBeltPassTime();
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
    // �I�_���̃x�N�g��
    public Vector2 ConveyorVectorEnd
    {
        get { return m_conveyorVectorEnd; }
        set { m_conveyorVectorEnd = value;}
    }

    //// ����_
    //public Vector2 ControlPoint
    //{
    //    get { return m_controlPoint; }
    //}

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



    // �m�[�c�̈ړ�
    private void MoveMaterial(RhythmMaterial material)
    {
		// ����
		float t = GetMaterialTime(material);

		// ���Ԃ� 1 �ȏ�
		if (t >= 1.0f)
		{
			NextConveyor(material);
		}

		// ���W
		Vector2 pos = MyFunction.GetPointOnBezierCurve(m_conveyorBegin, m_conveyorEnd, t);
		material.transform.position = pos;

	}

    // �}�e���A�������̃x���g�R���x�A�Ɉڂ�
    private void NextConveyor(RhythmMaterial material)
    {
        // ���̃x���g�R���x�A���Ȃ�
        if (ToConveyor == null)
            return;

        // �}�e���A���̐ݒ�
        ToConveyor.AddMaterial(material);
        // ���݂̃x���g�R���x�A����폜
        RemoveMaterial(material);

    }

}
