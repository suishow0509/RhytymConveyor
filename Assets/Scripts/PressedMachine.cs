using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressedMachine : BeltConveyor
{
    // 判定範囲
    [System.Serializable]
    public struct JudgementRange
    {
        public float range;
        public RhythmMaterial.MaterialState result;
    }

    [Header("---------- プレス機 ----------")]

    [Header("設定キー")]
    [SerializeField] private KeyCode m_keyCode = KeyCode.Space;

    [Header("発生源からの時間距離(秒)")]
    [SerializeField] private float m_timeDistance = 1.0f;

    [Header("プレスのクールタイム")]
    [SerializeField] private float m_pressCoolTime = 0.0f; 
    private float m_timer = 0.0f;

    [Header("判定範囲")]
    [SerializeField] private List<JudgementRange> m_range = new();



	private void Awake()
	{
        // 判定範囲のソート(range の昇順)
        m_range.Sort((lhs, rhs) => lhs.range.CompareTo(rhs.range));

	}

    void Update()
    {
        // ノーツの移動
        MoveMaterials();

        // クールタイム中
        if (m_timer > 0.0f)
        {
            // 時間経過
            m_timer -= Time.deltaTime;
            return;
        }

        // プレス
        if (Input.GetKeyDown(m_keyCode))
        {
            Press();
        }
        
    }

    // プレス
    public void Press()
    {
        // クールタイム中は処理しない
        if (m_timer > 0.0f)
        {
            return;
        }

        // 現在のレーンに乗っているノーツ取得
        List<RhythmMaterial> materials = RhythmMaterials;

        // ノーツ分確認
        foreach (RhythmMaterial material in materials)
        {
            // 移動割合取得
            float t = GetMaterialTime(material);
            // 中心との差
            float dif = Mathf.Abs(t - 0.5f);

            // 判定のループ
            foreach (JudgementRange judge in m_range)
            {
                // 判定範囲内
                if (dif <= judge.range)
                {
                    // 判定の設定
                    material.SetState(judge.result);
                    Debug.Log(judge.result);
                    break;
                }
            }
        }

    }
}
