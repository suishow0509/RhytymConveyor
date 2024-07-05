using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmMaterial : MonoBehaviour
{
    // 状態
    [System.Serializable]
    public enum MaterialState
    {
        PASS,               // 未加工
        SLIDE,              // かすった
        PRESSED,            // 加工済み
        CRITICAL_PRESSED,   // 完璧
    }

    [Header("シーンマネージャ")]
    [SerializeField] private RhythmSceneManager m_sceneManager = null;

    [Header("現在乗っているベルトコンベア")]
    [SerializeField] private BeltConveyor m_currentBeltConveyor = null;

    [Header("ノーツの状態")]
    [SerializeField] private MaterialState m_materialState = MaterialState.PASS;

    // ノーツ発生からの時間
    private float m_timer = 0.0f;



	private void Update()
	{
        // タイマー加算
        m_timer += Time.deltaTime;
	}

	// スコア取得
	public int GetScore()
    {
        return 1;
        return (int)m_materialState;
    }



    // シーンマネージャ
    public RhythmSceneManager SceneManager
    {
        set { m_sceneManager = value; }
    }
    // 現在のベルトコンベア
    public BeltConveyor CurrentBeltConveyor
    {
        set { m_currentBeltConveyor = value; }
    }
    // タイマー
    public float Timer
    {
        get { return m_timer; } 
    }



}
