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
		ConveyorBeginPoint = transform.position + (Vector3.up * (transform.localScale.x / 2.0f));
		// �x���g�R���x�A�̏I�_�ݒ�
		ConveyorEndPoint = transform.position;
		if (FromConveyor)
		{
			// �O�̃x���g�R���x�A�ɃX�s�[�h�����킹��
			ConveyorSpeed = FromConveyor.ConveyorSpeed;

			// ���݂̃x���g�R���x�A�̎n�_���ЂƂO�̃x���g�R���x�A�̏I�_�ɐݒ肷��
			FromConveyor.ConveyorEndPoint = ConveyorBegin.point;
		}

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
