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

    [Header("********** マテリアル発生 **********")]
    [Header("発生ノーツ数")]
    [SerializeField] private int m_generateNotesCount = 0;

    [Header("ノーツ")]
    [SerializeField] private RhythmMaterial m_rhythmMaterial = null;
    private int m_notes = 0;

    [Header("シーンマネージャ")]
    [SerializeField] private RhythmSceneManager m_sceneManager = null;

    [Header("----- デバッグ -----")]
    [Header("ノーツの発生間隔")]
    [SerializeField] private float m_notesInterval = 0.5f;
    private float m_notesTimer = 0.0f;


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
        m_notesTimer -= Time.deltaTime;
        // 0.5秒ごとにノーツを出す
        if (m_notesTimer <= 0.0f)
        {
            m_notesTimer = (m_notesInterval * m_generateNotesCount) + m_notesInterval - m_sceneManager.SceneTimer;
            PopMaterial(m_notesInterval * m_generateNotesCount);
        }


        if (Input.GetKeyUp(KeyCode.P))
        {
            PopMaterial(m_sceneManager.SceneTimer);
        }
		MoveMaterials();

	}

	// ノーツ発生
	public void PopMaterial(float popTime)
    {
        // 生成
        RhythmMaterial rhythm = Instantiate(m_rhythmMaterial, ConveyorBegin.point, Quaternion.identity);
        // コンベアの設定
        AddMaterial(rhythm);
        // タイマーの設定
        rhythm.Timer = m_sceneManager.SceneTimer - popTime;
		// 発生ノーツ数の加算
		m_generateNotesCount++;
	}
}
