using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmMaterial : MonoBehaviour
{
    // ���
    [System.Serializable]
    public enum MaterialState
    {
        PASS,               // �����H
        SLIDE,              // ��������
        PRESSED,            // ���H�ς�
        CRITICAL_PRESSED,   // ����
    }

    [Header("�V�[���}�l�[�W��")]
    [SerializeField] private RhythmSceneManager m_sceneManager = null;

    [Header("����^�C�~���O(ms)")]
    [SerializeField] private float m_timing;

    [Header("���ݏ���Ă���x���g�R���x�A")]
    [SerializeField] private BeltConveyor m_currentBeltConveyor = null;

    [Header("�m�[�c�̏��")]
    [SerializeField] private MaterialState m_materialState = MaterialState.PASS;

    // �m�[�c��������̎���
    private float m_timer = 0.0f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // �^�C�}�[���Z
        m_timer += Time.deltaTime;

        // �x���g�R���x�A�̎n�_
        Vector2 conveyorBegin = m_currentBeltConveyor.ConveyorBegin;
        // �x���g�R���x�A�̏I�_
        Vector2 conveyorEnd = m_currentBeltConveyor.ConveyorEnd;
        // �x���g�R���x�A�̐���_
        Vector2 conveyorControl = m_currentBeltConveyor.ControlPoint;
        // ����
        float t = (m_timer - m_currentBeltConveyor.GetCurrentLocationTime()) / m_currentBeltConveyor.GetBeltPassTime();

        // ���Ԃ� 1 �ȏ�
        if (t >= 1.0f)
        {
            NextConveyor();
        }

        // ���W
        Vector2 pos = MyFunction.GetPointOnBezierCurve(conveyorBegin, conveyorEnd, conveyorControl, t);
        transform.position = pos;

    }



    // �V�[���}�l�[�W��
    public RhythmSceneManager SceneManager
    {
        set { m_sceneManager = value; }
    }
    // ���݂̃x���g�R���x�A
    public BeltConveyor CurrentBeltConveyor
    {
        set { m_currentBeltConveyor = value; }
    }



    private void NextConveyor()
    {
        // ���̃x���g�R���x�A���Ȃ�
        if (m_currentBeltConveyor.ToConveyor == null)
            return;

        // ���̃x���g�R���x�A��ݒ肷��
        m_currentBeltConveyor = m_currentBeltConveyor.ToConveyor;

    }

}
