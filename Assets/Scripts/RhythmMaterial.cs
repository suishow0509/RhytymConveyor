using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmMaterial : MonoBehaviour
{
    // ���
    [System.Serializable]
    public enum MaterialState
    {
        RAW,               // �����H
        SLIDE,              // ��������
        PRESSED,            // ���H�ς�
        CRITICAL_PRESSED,   // ����
    }

    [Header("�m�[�c�̏��")]
    [SerializeField] private MaterialState m_materialState = MaterialState.RAW;

    // �m�[�c��������̎���
    private float m_timer = 0.0f;

    Vector3 oldPos = Vector3.zero;


	private void Update()
	{
        // �^�C�}�[���Z
        m_timer += Time.deltaTime;

        //Debug.Log(Vector3.Distance(transform.position, oldPos));
        oldPos = transform.position;
	}


    // ��Ԃ̐ݒ�
    public void SetState(MaterialState state)
    {
        // ���ǂ����
        if (state > m_materialState)
        {
            m_materialState = state;
        }
    }


    // �m�[�c�̏��
    public MaterialState State
    {
        get { return m_materialState; }
    }

    // �^�C�}�[
    public float Timer
    {
        get { return m_timer; } 
        set { m_timer = value; }
    }



}
