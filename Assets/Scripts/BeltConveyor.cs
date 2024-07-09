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
    [SerializeField] private MyFunction.BezierEdge m_conveyorBegin;
    [SerializeField] private MyFunction.BezierEdge m_conveyorEnd;
    [Header("端のベクトル")]
    [SerializeField] private Vector2 m_conveyorVectorEnd = Vector2.down;
    // ベルトコンベアの長さ
    private float m_conveyorLength = 1.0f;
    //// ベジェ曲線の制御点
    //private Vector2 m_controlPoint = Vector2.zero;

	[Header("入ってくる元のベルトコンベア")]
	[SerializeField] private BeltConveyor m_fromConveyor = null;
	[Header("出ていく先のベルトコンベア")]
	[SerializeField] private BeltConveyor m_toConveyor = null;

    [Header("制御点(確認用)")]
    [SerializeField] private GameObject m_controlObject = null;

    [Header("----- ノーツ -----")]
    [Header("現在載せているノーツ")]
    [SerializeField] private List<RhythmMaterial> m_materials = new();


	// Start is called before the first frame update
	void Start()
    {
        // 前にベルトコンベアがある場合は始点を設定する
		if (FromConveyor)
        {
            // 始点の設定
            ConveyorBeginPoint = FromConveyor.ConveyorEnd.point;
		}

        // 表示確認
        if (m_controlObject)
        {
            Instantiate(m_controlObject, m_conveyorBegin.point + m_conveyorBegin.control, Quaternion.identity);
            Instantiate(m_controlObject, m_conveyorEnd.point + m_conveyorEnd.control, Quaternion.identity);
        }

        // ベルトコンベア生成
        BakeBeltConveyor();

	}

    // Update is called once per frame
    void Update()
    {
        MoveMaterials();
	}

    // ****************************** ベルトコンベアの処理 ****************************** //
	// ベルトコンベアの頂点生成
	public void BakeBeltConveyor()
	{
		// ベルトコンベアの長さをマンハッタン距離で求める
		m_conveyorLength = Mathf.Abs(m_conveyorBegin.point.x - m_conveyorEnd.point.x) + Mathf.Abs(m_conveyorBegin.point.y - m_conveyorEnd.point.y);

		// 描画の設定
		if (m_lineRenderer)
		{
			// 1つ前の座標
			Vector2 prePosition = m_conveyorBegin.point;
			// トータルの分割数
			int division = m_beltDivision * (int)m_conveyorLength;
			// 頂点数の設定
			m_lineRenderer.positionCount = division + 1;
			for (int i = 0; i <= division; i++)
			{
				// 割合
				float t = (float)i / division;
				// 座標の計算
				Vector2 pos = MyFunction.GetPointOnBezierCurve(m_conveyorBegin, m_conveyorEnd, t);

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

	// ****************************** ノーツの処理 ****************************** //
	// ノーツの追加
	public void AddMaterial(RhythmMaterial material)
    {
        m_materials.Add(material);
    }
    // ノーツ削除
    public void RemoveMaterial(RhythmMaterial material)
    {
        m_materials.Remove(material);
    }
    // ノーツの破棄
    public void DestroyMaterial(RhythmMaterial material)
    {
        m_materials.Remove(material);
		Destroy(material.gameObject);
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
    // ノーツの移動割合
    public float GetMaterialTime(RhythmMaterial material)
    {
        // 通過時間
        float passTime = GetBeltPassTime();
        // 通過時間が 0 の場合はゼロ除算になるため処理しない
        if (passTime <= 0.0f)
        {
            return 0.0f;
        }
		// 時間
		return (material.Timer - GetCurrentLocationTime()) / GetBeltPassTime();
	}


	// 輸送速度
	public float ConveyorSpeed
	{
		get { return m_conveyorSpeed; }
		set { m_conveyorSpeed = value; }
	}

	// 始点
	public MyFunction.BezierEdge ConveyorBegin
    {
        get { return m_conveyorBegin; }
        set { m_conveyorBegin = value; }
    }
    public Vector2 ConveyorBeginPoint
    {
        set { m_conveyorBegin.point = value; }
    }
    // 終点
    public MyFunction.BezierEdge ConveyorEnd
    {
        get { return m_conveyorEnd; }
        set { m_conveyorEnd = value; }
    }
    public Vector2 ConveyorEndPoint
    {
        set { m_conveyorEnd.point = value; }
    }
    // 終点側のベクトル
    public Vector2 ConveyorVectorEnd
    {
        get { return m_conveyorVectorEnd; }
        set { m_conveyorVectorEnd = value;}
    }

    //// 制御点
    //public Vector2 ControlPoint
    //{
    //    get { return m_controlPoint; }
    //}

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

    // ノーツ
    public List<RhythmMaterial> RhythmMaterials
    {
        get { return m_materials; }
    }



    // ノーツの移動
    private void MoveMaterial(RhythmMaterial material)
    {
		// 時間
		float t = GetMaterialTime(material);

		// 時間が 1 以上
		if (t >= 1.0f)
		{
			NextConveyor(material);
		}

		// 座標
		Vector2 pos = MyFunction.GetPointOnBezierCurve(m_conveyorBegin, m_conveyorEnd, t);
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
        RemoveMaterial(material);

    }

}
