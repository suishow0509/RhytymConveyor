using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmMaterialSource : BeltConveyor
{
    [System.Serializable]
    public struct MaterialTiming
    {
        public float time;      // �m�[�c�̃^�C�~���O
        public float step;      // �m�[�c�̉���
    }

    [Header("�����Ԋu")]
    [SerializeField] private MaterialTiming[] m_timings = null;

    [Header("�m�[�c")]
    [SerializeField] private RhythmMaterial m_rhythmMaterial = null;
    private int m_notes = 0;

    [Header("�V�[���}�l�[�W��")]
    [SerializeField] private RhythmSceneManager m_sceneManager = null;

	private void Awake()
	{
		ConveyorBegin = transform.position;
		ConveyorEnd = transform.position;
		// ���̃x���g�R���x�A������
		if (ToConveyor)
		{
			// ���̃x���g�R���x�A�̎n�_��ݒ肷��
			ConveyorEnd = ToConveyor.ConveyorBegin;
		}

	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.P))
        {
            PopMaterial();
        }

    }

    // �m�[�c����
    public void PopMaterial()
    {
        // ����
        RhythmMaterial rhythm = Instantiate(m_rhythmMaterial, ToConveyor.transform.position, Quaternion.identity);
        // �V�[���}�l�[�W���̐ݒ�
        rhythm.SceneManager = m_sceneManager;
        // �R���x�A�̐ݒ�
        rhythm.CurrentBeltConveyor = this;
    }
}
