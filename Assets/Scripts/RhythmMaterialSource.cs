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

	[Header("----- �}�e���A������ -----")]

    [Header("�m�[�c")]
    [SerializeField] private RhythmMaterial m_rhythmMaterial = null;
    private int m_notes = 0;

    [Header("�V�[���}�l�[�W��")]
    [SerializeField] private RhythmSceneManager m_sceneManager = null;

	private void Awake()
	{
        // �x���g�R���x�A�̎n�_�ݒ�
		ConveyorBeginPoint = transform.position;
        // �x���g�R���x�A�̏I�_�ݒ�
		ConveyorEndPoint = transform.position + (Vector3.down * (transform.localScale.x / 2.0f));
		// ���̃x���g�R���x�A������
		if (ToConveyor)
		{
			// ���̃x���g�R���x�A�̎n�_�����݂̃x���g�R���x�A�̏I�_�ɐݒ肷��
			ToConveyor.ConveyorBeginPoint = ConveyorEnd.point;
            // �X�s�[�h�����̃x���g�R���x�A�ɍ��킹��
            ConveyorSpeed = ToConveyor.ConveyorSpeed;
		}
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.P))
        {
            PopMaterial();
        }
		MoveMaterials();

	}

	// �m�[�c����
	public void PopMaterial()
    {
        // ����
        RhythmMaterial rhythm = Instantiate(m_rhythmMaterial, ConveyorBegin.point, Quaternion.identity);
        // �R���x�A�̐ݒ�
        AddMaterial(rhythm);
    }
}
