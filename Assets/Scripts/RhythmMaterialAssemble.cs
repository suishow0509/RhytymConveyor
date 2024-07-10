using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmMaterialAssemble : BeltConveyor
{
	[Header("----- マテリアル収集 -----")]

	[Header("スコア")]
	[SerializeField] private int m_score = 0;


	private void Awake()
	{
		// ベルトコンベアの始点設定
		ConveyorBeginPoint = transform.position + (Vector3.up * (transform.localScale.x / 2.0f));
		// ベルトコンベアの終点設定
		ConveyorEndPoint = transform.position;
		if (FromConveyor)
		{
			// 前のベルトコンベアにスピードを合わせる
			ConveyorSpeed = FromConveyor.ConveyorSpeed;

			// 現在のベルトコンベアの始点をひとつ前のベルトコンベアの終点に設定する
			FromConveyor.ConveyorEndPoint = ConveyorBegin.point;
		}

	}

	// Update is called once per frame
	void Update()
    {
		// ノーツが存在する
		if (RhythmMaterials.Count > 0)
		{
			CollectMaterial();
		}

		// ノーツの移動
		MoveMaterials();
    }



	// ノーツ収集
	private void CollectMaterial()
	{
		for (int i = RhythmMaterials.Count - 1; i >= 0; i--)
		{
			// レーンの末尾にノーツがある
			if (GetMaterialTime(RhythmMaterials[i]) >= 1.0f)
			{
				// ノーツのスコア加算
				m_score = RhythmMaterials[i].GetScore();
				// ノーツの破棄
				DestroyMaterial(RhythmMaterials[i]);
			}
		}
	}

}
