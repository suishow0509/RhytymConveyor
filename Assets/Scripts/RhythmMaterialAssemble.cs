using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmMaterialAssemble : BeltConveyor
{
	[Header("----- �}�e���A�����W -----")]

	[Header("�X�R�A")]
	[SerializeField] private int m_score = 0;


	private void Awake()
	{
		// �x���g�R���x�A�̎n�_�ݒ�
		ConveyorBegin = transform.position;
		if (FromConveyor)
		{
			// �O�̃x���g�R���x�A�ɃX�s�[�h�����킹��
			ConveyorSpeed = FromConveyor.ConveyorSpeed;

			// �ʒu����
			ConveyorBegin = FromConveyor.ConveyorEnd;
			transform.position = ConveyorBegin + (FromConveyor.ConveyorVectorEnd.normalized * 0.5f);
		}
		// �x���g�R���x�A�̏I�_�ݒ�
		ConveyorEnd = transform.position;

	}

	// Update is called once per frame
	void Update()
    {
		// �m�[�c�����݂���
		if (RhythmMaterials.Count > 0)
		{
			CollectMaterial();
		}

		// �m�[�c�̈ړ�
		MoveMaterials();
    }



	// �m�[�c���W
	private void CollectMaterial()
	{
		for (int i = RhythmMaterials.Count - 1; i >= 0; i--)
		{
			// ���[���̖����Ƀm�[�c������
			if (GetMaterialTime(RhythmMaterials[i]) >= 1.0f)
			{
				// �m�[�c�̃X�R�A���Z
				m_score = RhythmMaterials[i].GetScore();
				// �m�[�c�̔j��
				DestroyMaterial(RhythmMaterials[i]);
			}
		}
	}

}
