using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmMaterialAssemble : BeltConveyor
{


	private void Awake()
	{
		if (FromConveyor)
		{
			// �O�̃x���g�R���x�A�ɃX�s�[�h�����킹��
			ConveyorSpeed = FromConveyor.ConveyorSpeed;
		}
	}

	// Update is called once per frame
	void Update()
    {
        
    }
}
