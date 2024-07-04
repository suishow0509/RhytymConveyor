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
    [Header("ベルトコンベアのスピード(/s)"), Min(0.0f)]
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
        // 制御点の計算(線分と線分の最短座標)
        // とりあえず制御点は始点と終点の中点にしておく
        m_controlPoint = Vector2.Lerp(m_conveyorBegin, m_conveyorEnd, 0.5f);

        // ベルトコンベアの長さ初期化
        m_conveyorLength = 0.0f;

		// 描画の設定
		if (m_lineRenderer)
		{
            // 1つ前の座標
            Vector2 prePosition = m_conveyorBegin;
			// 頂点数の設定
			m_lineRenderer.positionCount = m_beltDivision + 1;
			for (int i = 0; i <= m_beltDivision; i++)
			{
				// 割合
				float t = (float)i / m_beltDivision;
				// 座標の計算
				Vector2 pos = MyFunction.GetPointOnBezierCurve(m_conveyorBegin, m_conveyorEnd, m_controlPoint, t);

				// 描画座標設定
				m_lineRenderer.SetPosition(i, pos);

                // 距離を求める
                m_conveyorLength += Vector2.Distance(prePosition, pos);
                // 前の座標を設定する
                prePosition = pos;
			}
		}
        // ベルトコンベアの長さ
        else
        {
			// ベルトコンベアの長さをマンハッタン距離で求める
			m_conveyorLength = Mathf.Abs(m_conveyorBegin.x - m_conveyorEnd.x) + Mathf.Abs(m_conveyorBegin.y - m_conveyorEnd.y);
		}
	}

    // Update is called once per frame
    void Update()
    {

    }

    // ベルトコンベアを流れる時間
    public float GetBeltPassTime()
    {
        // スピードが 0 以上
        if (m_conveyorSpeed > 0.0f)
        {
			return m_conveyorLength / m_conveyorSpeed;
		}
		return 0.0f;
    }

    // 現在地までの時間
    public float GetCurrentLocationTime()
    {
        if (m_fromConveyor)
        {
            return m_fromConveyor.GetCurrentLocationTime() + m_fromConveyor.GetBeltPassTime();
        }
        else
        {
            return 0.0f;
        }
    }


    // 始点
    public Vector2 ConveyorBegin
    {
        get { return m_conveyorBegin; }
        set { m_conveyorBegin = value; }
    }
    // 終点
    public Vector2 ConveyorEnd
    {
        get { return m_conveyorEnd; }
        set { m_conveyorEnd = value; }
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
        set { m_fromConveyor = value; }
    }
    // 先のベルトコンベア
    public BeltConveyor ToConveyor
    {
        get { return m_toConveyor; }
        set { m_toConveyor = value; }
    }

}
