using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressedMachine : BeltConveyor
{
    // ����͈�
    [System.Serializable]
    public struct JudgementRange
    {
        public float range;
        public RhythmMaterial.MaterialState result;
    }

    [Header("---------- �v���X�@ ----------")]

    [Header("�ݒ�L�[")]
    [SerializeField] private KeyCode m_keyCode = KeyCode.Space;

    [Header("����������̎��ԋ���(�b)")]
    [SerializeField] private float m_timeDistance = 1.0f;

    [Header("�v���X�̃N�[���^�C��")]
    [SerializeField] private float m_pressCoolTime = 0.0f; 
    private float m_timer = 0.0f;

    [Header("����͈�")]
    [SerializeField] private List<JudgementRange> m_range = new();



	private void Awake()
	{
        // ����͈͂̃\�[�g(range �̏���)
        m_range.Sort((lhs, rhs) => lhs.range.CompareTo(rhs.range));

	}

    void Update()
    {
        // �m�[�c�̈ړ�
        MoveMaterials();

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
            Press();
        }
        
    }

    // �v���X
    public void Press()
    {
        // �N�[���^�C�����͏������Ȃ�
        if (m_timer > 0.0f)
        {
            return;
        }

        // ���݂̃��[���ɏ���Ă���m�[�c�擾
        List<RhythmMaterial> materials = RhythmMaterials;

        // �m�[�c���m�F
        foreach (RhythmMaterial material in materials)
        {
            // �ړ������擾
            float t = GetMaterialTime(material);
            // ���S�Ƃ̍�
            float dif = Mathf.Abs(t - 0.5f);

            // ����̃��[�v
            foreach (JudgementRange judge in m_range)
            {
                // ����͈͓�
                if (dif <= judge.range)
                {
                    // ����̐ݒ�
                    material.SetState(judge.result);
                    Debug.Log(judge.result);
                    break;
                }
            }
        }

    }
}
