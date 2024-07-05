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

    [Header("���ݏ���Ă���x���g�R���x�A")]
    [SerializeField] private BeltConveyor m_currentBeltConveyor = null;

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
    // �^�C�}�[
    public float Timer
    {
        get { return m_timer; } 
    }



}
