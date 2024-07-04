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

    [Header("����^�C�~���O(ms)")]
    [SerializeField] private float m_timing;

    [Header("���ݏ���Ă���x���g�R���x�A")]
    [SerializeField] private BeltConveyor m_currentBeltConveyor = null;

    [Header("�m�[�c�̏��")]
    [SerializeField] private MaterialState m_materialState = MaterialState.PASS;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // �x���g�R���x�A�̎n�_
        Vector2 conveyorBegin = m_currentBeltConveyor.ConveyorBegin;
        // �x���g�R���x�A�̏I�_
        Vector2 conveyorEnd = m_currentBeltConveyor.ConveyorEnd;
        // �x���g�R���x�A�̐���_
        Vector2 conveyorControl = m_currentBeltConveyor.ControlPoint;



    }


    // ���݂̃x���g�R���x�A
    public BeltConveyor CurrentBeltConveyor
    {
        set { m_currentBeltConveyor = value; }
    }

}
