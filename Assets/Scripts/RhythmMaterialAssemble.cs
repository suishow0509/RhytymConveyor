using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmMaterialAssemble : BeltConveyor
{


	private void Awake()
	{
		if (FromConveyor)
		{
			// 前のベルトコンベアにスピードを合わせる
			ConveyorSpeed = FromConveyor.ConveyorSpeed;
		}
	}

	// Update is called once per frame
	void Update()
    {
        
    }
}
