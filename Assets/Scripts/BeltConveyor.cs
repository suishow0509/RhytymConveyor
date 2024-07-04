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
    [Header("�x���g�R���x�A�̃X�s�[�h(/s)")]
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
        // �x���g�R���x�A�̒������}���n�b�^�������ŋ��߂�
        m_conveyorLength = Mathf.Abs(m_conveyorBegin.x - m_conveyorEnd.x) + Mathf.Abs(m_conveyorBegin.y - m_conveyorEnd.y);

        // �Ƃ肠��������_�͎n�_�ƏI�_�̒��_�ɂ��Ă���
        m_controlPoint = Vector2.Lerp(m_conveyorBegin, m_conveyorEnd, 0.5f);

        // �`��̐ݒ�
        if (m_lineRenderer)
        {
            // �������̐ݒ�
            m_lineRenderer.positionCount = m_beltDivision;
            // 

            for (int i = 0; i < m_beltDivision; i++)
            {
                // ����
                float t = (float)i / m_beltDivision;
                // �n�_��
                Vector2 beginLerp = Vector2.Lerp(m_conveyorBegin, m_controlPoint, t);
                // �I�_��
                Vector2 endLerp = Vector2.Lerp(m_controlPoint, m_conveyorEnd, t);
                // ���W�̌v�Z
                Vector2 pos = Vector2.Lerp(beginLerp, endLerp, t);
                // �`����W�ݒ�
                m_lineRenderer.SetPosition(i, pos);
            }

		}
	}

    // Update is called once per frame
    void Update()
    {

    }


    // �n�_�̎擾
    public Vector2 ConveyorBegin
    {
        get { return m_conveyorBegin; }
    }
    // �I�_�̎擾
    public Vector2 ConveyorEnd
    {
        get { return m_conveyorEnd; }
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
    }
    // ��̃x���g�R���x�A
    public BeltConveyor ToConveyor
    {
        get { return m_toConveyor; }
    }

}
