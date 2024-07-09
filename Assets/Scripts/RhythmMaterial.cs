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

    [Header("�m�[�c�̏��")]
    [SerializeField] private MaterialState m_materialState = MaterialState.PASS;

    // �m�[�c��������̎���
    private float m_timer = 0.0f;



	private void Update()
	{
        // �^�C�}�[���Z
        m_timer += Time.deltaTime;
	}

	// �X�R�A�擾
	public int GetScore()
    {
        return 1;
        return (int)m_materialState;
    }



    // �^�C�}�[
    public float Timer
    {
        get { return m_timer; } 
    }



}
