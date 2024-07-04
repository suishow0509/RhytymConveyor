using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeltConveyor : MonoBehaviour
{
    [Header("----- 描画 -----")]
    [Header("ラインレンダラー")]
    [SerializeField] private LineRenderer m_lineRenderer = null;

    [Header("描画の分割数")]
    [SerializeField] private int m_beltDivision = 5;


    [Header("----- ベルトコンベア -----")]
    [Header("ベルトコンベアのスピード(/s)")]
    [SerializeField] private float m_conveyorSpeed = 1.0f;

    [Header("ベルトコンベアの始点と終点")]
    [SerializeField] private Vector2 m_conveyorBegin = Vector2.zero;
    [SerializeField] private Vector2 m_conveyorEnd = Vector2.zero;
    // ベルトコンベアの長さ
    private float m_conveyorLength = 1.0f;
    // ベジェ曲線の制御点
    private Vector2 m_controlPoint = Vector2.zero;

	[Header("入ってくる元のベルトコンベア")]
	[SerializeField] private BeltConveyor m_fromConveyor = null;
	[Header("出ていく先のベルトコンベア")]
	[SerializeField] private BeltConveyor m_toConveyor = null;


	// Start is called before the first frame update
	void Start()
    {
        // ベルトコンベアの長さをマンハッタン距離で求める
        m_conveyorLength = Mathf.Abs(m_conveyorBegin.x - m_conveyorEnd.x) + Mathf.Abs(m_conveyorBegin.y - m_conveyorEnd.y);

        // とりあえず制御点は始点と終点の中点にしておく
        m_controlPoint = Vector2.Lerp(m_conveyorBegin, m_conveyorEnd, 0.5f);

        // 描画の設定
        if (m_lineRenderer)
        {
            // 分割数の設定
            m_lineRenderer.positionCount = m_beltDivision;
            // 

            for (int i = 0; i < m_beltDivision; i++)
            {
                // 割合
                float t = (float)i / m_beltDivision;
                // 始点側
                Vector2 beginLerp = Vector2.Lerp(m_conveyorBegin, m_controlPoint, t);
                // 終点側
                Vector2 endLerp = Vector2.Lerp(m_controlPoint, m_conveyorEnd, t);
                // 座標の計算
                Vector2 pos = Vector2.Lerp(beginLerp, endLerp, t);
                // 描画座標設定
                m_lineRenderer.SetPosition(i, pos);
            }

		}
	}

    // Update is called once per frame
    void Update()
    {

    }


    // 始点の取得
    public Vector2 ConveyorBegin
    {
        get { return m_conveyorBegin; }
    }
    // 終点の取得
    public Vector2 ConveyorEnd
    {
        get { return m_conveyorEnd; }
    }
    // 制御点
    public Vector2 ControlPoint
    {
        get { return m_controlPoint; }
    }

    // 元のベルトコンベア
    public BeltConveyor FromConveyor
    {
        get { return m_fromConveyor; }
    }
    // 先のベルトコンベア
    public BeltConveyor ToConveyor
    {
        get { return m_toConveyor; }
    }

}
