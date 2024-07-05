using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeltConveyor : MonoBehaviour
{
    [Header("----- 描画 -----")]
    [Header("ラインレンダラー")]
    [SerializeField] private LineRenderer m_lineRenderer = null;

    [Header("描画の分割数(一メモリ分)")]
    [SerializeField] private int m_beltDivision = 5;


    [Header("----- ベルトコンベア -----")]
    [Header("ベルトコンベアのスピード(/s)"), Min(0.0f)]
    [SerializeField] private float m_conveyorSpeed = 1.0f;

    [Header("ベルトコンベアの始点と終点")]
    [SerializeField] private Vector2 m_conveyorBegin = Vector2.zero;
    [SerializeField] private Vector2 m_conveyorEnd = Vector2.zero;
    [Header("端のベクトル")]
    [SerializeField] private Vector2 m_conveyorVectorEnd = Vector2.down;
    // ベルトコンベアの長さ
    private float m_conveyorLength = 1.0f;
    // ベジェ曲線の制御点
    private Vector2 m_controlPoint = Vector2.zero;

	[Header("入ってくる元のベルトコンベア")]
	[SerializeField] private BeltConveyor m_fromConveyor = null;
	[Header("出ていく先のベルトコンベア")]
	[SerializeField] private BeltConveyor m_toConveyor = null;

    [Header("ノーツ")]
    [Header("現在載せているノーツ")]
    [SerializeField] private List<RhythmMaterial> m_materials = new();


	// Start is called before the first frame update
	void Start()
    {
        if (FromConveyor)
        {
            // 始点の設定
            ConveyorBegin = FromConveyor.ConveyorEnd;

            // 制御点の計算
			Ray2D ray1 = new(m_conveyorBegin, FromConveyor.ConveyorVectorEnd);
			Ray2D ray2 = new(m_conveyorEnd, -m_conveyorVectorEnd);
			m_controlPoint = MyFunction.IntersectionRay(ray1, ray2);
		}

        // ベルトコンベア生成
        BakeBeltConveyor();

	}

    // Update is called once per frame
    void Update()
    {
        MoveMaterials();
	}

	// ベルトコンベアの頂点生成
	public void BakeBeltConveyor()
	{
		// ベルトコンベアの長さをマンハッタン距離で求める
		m_conveyorLength = Mathf.Abs(m_conveyorBegin.x - m_conveyorEnd.x) + Mathf.Abs(m_conveyorBegin.y - m_conveyorEnd.y);

		// 描画の設定
		if (m_lineRenderer)
		{
			// 1つ前の座標
			Vector2 prePosition = m_conveyorBegin;
			// トータルの分割数
			int division = m_beltDivision * (int)m_conveyorLength;
			// 頂点数の設定
			m_lineRenderer.positionCount = division + 1;
			for (int i = 0; i <= division; i++)
			{
				// 割合
				float t = (float)i / division;
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
        // ひとつ前にベルトコンベアがある
        if (m_fromConveyor)
        {
            // ひとつ前のベルトコンベアの時間を加算していく
            return m_fromConveyor.GetCurrentLocationTime() + m_fromConveyor.GetBeltPassTime();
        }
        else
        {
            // ベルトコンベアの先端だから、ここにたどり着くまでの時間は 0 である
            return 0.0f;
        }
    }

    // 始点側のベクトル
    public Vector2 GetVectorBegin()
    {
        // 始点の次の座標
        Vector2 next = m_conveyorEnd;

        if (m_lineRenderer)
        {
            next = m_lineRenderer.GetPosition(1);
        }

        return (m_conveyorBegin - next).normalized;
    }
    // 終点側のベクトル
    public Vector2 GetVectorEnd()
    {
        // 終点の前の座標
        Vector2 previous = m_conveyorBegin;

        if (m_lineRenderer)
        {
            previous = m_lineRenderer.GetPosition(m_lineRenderer.positionCount - 2);
        }

        return (m_conveyorEnd - previous).normalized;
    }

    // ノーツの追加
    public void AddMaterial(RhythmMaterial material)
    {
        m_materials.Add(material);
    }
    // ノーツの移動
	public void MoveMaterials()
	{
		// 関数内で要素を削除する可能性があるため末尾から処理
		for (int i = m_materials.Count - 1; i >= 0; i--)
		{
			MoveMaterial(m_materials[i]);
		}
	}



	// 輸送速度
	public float ConveyorSpeed
	{
		get { return m_conveyorSpeed; }
		set { m_conveyorSpeed = value; }
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
    // 終点側のベクトル
    public Vector2 ConveyorVectorEnd
    {
        get { return m_conveyorVectorEnd; }
        set { m_conveyorVectorEnd = value;}
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




    // ノーツの移動
	private void MoveMaterial(RhythmMaterial material)
    {
		// 時間
		float t = (material.Timer - GetCurrentLocationTime()) / GetBeltPassTime();

		// 時間が 1 以上
		if (t >= 1.0f)
		{
			NextConveyor(material);
		}

		// 座標
		Vector2 pos = MyFunction.GetPointOnBezierCurve(m_conveyorBegin, m_conveyorEnd, m_controlPoint, t);
		material.transform.position = pos;

	}

    // マテリアルを次のベルトコンベアに移す
    private void NextConveyor(RhythmMaterial material)
    {
        // 次のベルトコンベアがない
        if (ToConveyor == null)
            return;

        // マテリアルの設定
        ToConveyor.AddMaterial(material);
        // 現在のベルトコンベアから削除
        m_materials.Remove(material);

    }

}
