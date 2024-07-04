using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressedMachine : MonoBehaviour
{
    [Header("�ݒ�L�[")]
    [SerializeField] private KeyCode m_keyCode = KeyCode.Space;

    [Header("����������̎��ԋ���(�b)")]
    [SerializeField] private float m_timeDistance = 1.0f;

    [Header("�v���X�̃N�[���^�C��")]
    [SerializeField] private float m_pressCoolTime = 0.0f; 
    private float m_timer = 0.0f;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // �N�[���^�C����
        if (m_timer > 0.0f)
        {
            // ���Ԍo��
            m_timer -= Time.deltaTime;
            return;
        }

        // �v���X
        if (Input.GetKeyDown(m_keyCode))
        {

        }
        
    }
}
