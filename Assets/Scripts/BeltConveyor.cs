using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeltConveyor : MonoBehaviour
{
    [Header("----- �`�� -----")]
    [Header("���C�������_���[")]
    [SerializeField] private LineRenderer m_lineRenderer = null;

    [Header("�`��̕�����")]
    [SerializeField] private int m_beltDivision = 5;


    [Header("----- �x���g�R���x�A -----")]
    [Header("�x���g�R���x�A�̃X�s�[�h(/s)"), Min(0.0f)]
    [SerializeField] private float m_conveyorSpeed = 1.0f;

    [Header("�x���g�R���x�A�̎n�_�ƏI�_")]
    [SerializeField] private Vector2 m_conveyorBegin = Vector2.zero;
    [SerializeField] private Vector2 m_conveyorEnd = Vector2.zero;
    // �x���g�R���x�A�̒���
    private float m_conveyorLength = 1.0f;
    // �x�W�F�Ȑ��̐���_
    private Vector2 m_controlPoint = Vector2.zero;

	[Header("�����Ă��錳�̃x���g�R���x�A")]
	[SerializeField] private BeltConveyor m_fromConveyor = null;
	[Header("�o�Ă�����̃x���g�R���x�A")]
	[SerializeField] private BeltConveyor m_toConveyor = null;


	// Start is called before the first frame update
	void Start()
    {
        // ����_�̌v�Z(�����Ɛ����̍ŒZ���W)
        // �Ƃ肠��������_�͎n�_�ƏI�_�̒��_�ɂ��Ă���
        m_controlPoint = Vector2.Lerp(m_conveyorBegin, m_conveyorEnd, 0.5f);

        // �x���g�R���x�A�̒���������
        m_conveyorLength = 0.0f;

		// �`��̐ݒ�
		if (m_lineRenderer)
		{
            // 1�O�̍��W
            Vector2 prePosition = m_conveyorBegin;
			// ���_���̐ݒ�
			m_lineRenderer.positionCount = m_beltDivision + 1;
			for (int i = 0; i <= m_beltDivision; i++)
			{
				// ����
				float t = (float)i / m_beltDivision;
				// ���W�̌v�Z
				Vector2 pos = MyFunction.GetPointOnBezierCurve(m_conveyorBegin, m_conveyorEnd, m_controlPoint, t);

				// �`����W�ݒ�
				m_lineRenderer.SetPosition(i, pos);

                // ���������߂�
                m_conveyorLength += Vector2.Distance(prePosition, pos);
                // �O�̍��W��ݒ肷��
                prePosition = pos;
			}
		}
        // �x���g�R���x�A�̒���
        else
        {
			// �x���g�R���x�A�̒������}���n�b�^�������ŋ��߂�
			m_conveyorLength = Mathf.Abs(m_conveyorBegin.x - m_conveyorEnd.x) + Mathf.Abs(m_conveyorBegin.y - m_conveyorEnd.y);
		}
	}

    // Update is called once per frame
    void Update()
    {

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
        if (m_fromConveyor)
        {
            return m_fromConveyor.GetCurrentLocationTime() + m_fromConveyor.GetBeltPassTime();
        }
        else
        {
            return 0.0f;
        }
    }


    // �n�_
    public Vector2 ConveyorBegin
    {
        get { return m_conveyorBegin; }
        set { m_conveyorBegin = value; }
    }
    // �I�_
    public Vector2 ConveyorEnd
    {
        get { return m_conveyorEnd; }
        set { m_conveyorEnd = value; }
    }
    // ����_
    public Vector2 ControlPoint
    {
        get { return m_controlPoint; }
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

}
