using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmMaterialSource : BeltConveyor
{
    [System.Serializable]
    public struct MaterialTiming
    {
        public float time;      // ノーツのタイミング
        public float step;      // ノーツの音程
    }

	[Header("----- マテリアル発生 -----")]

    [Header("ノーツ")]
    [SerializeField] private RhythmMaterial m_rhythmMaterial = null;
    private int m_notes = 0;

    [Header("シーンマネージャ")]
    [SerializeField] private RhythmSceneManager m_sceneManager = null;

	private void Awake()
	{
        // ベルトコンベアの始点設定
		ConveyorBeginPoint = transform.position;
        // ベルトコンベアの終点設定
		ConveyorEndPoint = transform.position + (Vector3.down * (transform.localScale.x / 2.0f));
		// 次のベルトコンベアがある
		if (ToConveyor)
		{
			// 次のベルトコンベアの始点を現在のベルトコンベアの終点に設定する
			ToConveyor.ConveyorBeginPoint = ConveyorEnd.point;
            // スピードを次のベルトコンベアに合わせる
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

	// ノーツ発生
	public void PopMaterial()
    {
        // 生成
        RhythmMaterial rhythm = Instantiate(m_rhythmMaterial, ConveyorBegin.point, Quaternion.identity);
        // コンベアの設定
        AddMaterial(rhythm);
    }
}
