using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmMaterial : MonoBehaviour
{
    // 状態
    [System.Serializable]
    public enum MaterialState
    {
        RAW,               // 未加工
        SLIDE,              // かすった
        PRESSED,            // 加工済み
        CRITICAL_PRESSED,   // 完璧
    }

    [Header("ノーツの状態")]
    [SerializeField] private MaterialState m_materialState = MaterialState.RAW;

    // ノーツ発生からの時間
    private float m_timer = 0.0f;

    Vector3 oldPos = Vector3.zero;


	private void Update()
	{
        // タイマー加算
        m_timer += Time.deltaTime;

        //Debug.Log(Vector3.Distance(transform.position, oldPos));
        oldPos = transform.position;
	}


    // 状態の設定
    public void SetState(MaterialState state)
    {
        // より良い状態
        if (state > m_materialState)
        {
            m_materialState = state;
        }
    }


    // ノーツの状態
    public MaterialState State
    {
        get { return m_materialState; }
    }

    // タイマー
    public float Timer
    {
        get { return m_timer; } 
        set { m_timer = value; }
    }



}
