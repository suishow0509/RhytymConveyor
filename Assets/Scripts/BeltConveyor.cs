using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static UnityEditor.PlayerSettings;

public class BeltConveyor : MonoBehaviour
{
    [Header("----- システム -----")]
    [Header("上書きを許可")]
    [SerializeField] private bool m_permitOverwrite = true;

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
    [Header("ベルトコンベアの長さ")]
    [SerializeField] private float m_beltLength = 1.0f;
    [Header("曲線を構成する点")]
    [SerializeField] private Vector3[] m_points = new Vector3[0];

	[Header("入ってくる元のベルトコンベア")]
	[SerializeField] private BeltConveyor m_fromConveyor = null;
	[Header("出ていく先のベルトコンベア")]
	[SerializeField] private BeltConveyor m_toConveyor = null;

    [Header("----- ノーツ -----")]
    [Header("現在載せているノーツ")]
    [SerializeField] private List<RhythmMaterial> m_materials = new();

    [Header("デバッグ")]
    [SerializeField] private bool m_debug = true;
    [Header("ベルトコンベアの通過時間")]
    [SerializeField] private float m_passTime;
    [Header("ベルトコンベアへの到達時間")]
    [SerializeField] private float m_arriveTime;
    [Header("識別用の色")]
    [SerializeField] private Color m_colorBegin = Color.green;
    [SerializeField] private Color m_colorEnd = Color.red;



	// Start is called before the first frame update
	void Start()
    {
        // 前にベルトコンベアがある場合は始点を設定する
		if (FromConveyor)
        {
            // 始点の設定
            ConveyorBeginPoint = FromConveyor.ConveyorEnd.point;
		}

        // ベルトコンベア生成
        BakeBeltConveyor();

	}

    // Update is called once per frame
    void Update()
    {
        // ノーツ移動
        MoveMaterials();
	}

    // ****************************** ベルトコンベアの処理 ****************************** //
    // ベルトコンベアの頂点生成
    [ContextMenu("BakeBeltConveyor")]
	public void BakeBeltConveyor()
	{
		// ベルトコンベアの長さをマンハッタン距離で求める
		m_beltLength = Mathf.Abs(m_conveyorBegin.point.x - m_conveyorEnd.point.x) + Mathf.Abs(m_conveyorBegin.point.y - m_conveyorEnd.point.y);

		// 1つ前の座標
		Vector2 prePosition = m_conveyorBegin.point;
		// トータルの分割数
		int division = m_beltDivision * (int)m_beltLength;
		// ベルトコンベアの長さ初期化
		m_beltLength = 0;
        // 点のリスト初期化
        m_points = new Vector3[division + 1];
        // 頂点設定
        for (int i = 0; i <= division; i++)
        {
            // 割合
            float t = (float)i / division;
            // 座標の計算
            Vector2 pos = MyFunction.GetPointOnBezierCurve(m_conveyorBegin, m_conveyorEnd, t);
            // 座標設定
            m_points[i] = pos;
            // ベルトコンベアの長さ加算
            m_beltLength += Vector2.Distance(prePosition, pos);
            // 前の座標を設定する
            prePosition = pos;
        }

		// 描画の設定
		if (m_lineRenderer)
		{
			// 頂点数の設定
			m_lineRenderer.positionCount = division + 1;

			// 描画座標設定
			m_lineRenderer.SetPositions(m_points);
		}

	}

	// ベルトコンベアを流れる時間
	public float GetBeltPassTime()
    {
        // スピードが 0 以上
        if (m_conveyorSpeed > 0.0f)
        {
			return m_beltLength / m_conveyorSpeed;
		}
		return 0.0f;
    }

    // 現在のベルトコンベアにたどり着くまでの時間
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
		if (m_debug)
		{
			// 表示確認
			Debug.DrawRay(m_conveyorBegin.point, m_conveyorBegin.control, m_colorBegin);
			Debug.DrawRay(m_conveyorEnd.point, m_conveyorEnd.control, m_colorEnd);
            // 時間
			m_passTime = GetBeltPassTime();
			m_arriveTime = GetCurrentLocationTime();
		}

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
		return (material.Timer - GetCurrentLocationTime()) / passTime;
	}
	// ノーツの移動
	public virtual void MoveMaterial(RhythmMaterial material)
	{
		// 時間
		float t = GetMaterialTime(material);

		// 時間が 1 以上
		if (t >= 1.0f)
		{
			NextConveyor(material);
            return;
		}

		// 座標
		Vector2 pos = GetMaterialPosition(t)/*MyFunction.GetPointOnBezierCurve(m_conveyorBegin, m_conveyorEnd, t)*/;
		material.transform.position = pos;

	}
    // ノーツの位置取得
    public Vector3 GetMaterialPosition(float time)
    {
        // 現在の時間に応じたベルトコンベアの長さ計算
        float length = m_beltLength * time;
        // 計算用の長さ
        float l = 0.0f;
		// 1つ前の座標
		Vector2 prePosition = m_conveyorBegin.point;
        // 使用する頂点の添え字を取得
        int index = 0;
        // 頂点間の距離
        float distance = 0.0f;
		for (int i = 0; i < m_points.Length; i++)
        {
			// 座標設定
			Vector3 pos = m_points[i];
            // 頂点間の距離
            distance = Vector2.Distance(prePosition, pos);
			// 頂点[i]までの長さが現在地の長さ以上
			if (l + distance >= length)
            {
                // ひとつ前が始点として使用したい添え字
                index = i - 1;
                break;
            }
			// ベルトコンベアの長さ加算
			l += distance;
			// 前の座標を設定する
			prePosition = pos;
		}
		// 始点
		Vector3 begin = m_points[index];
        // 終点
        Vector3 end = m_points[index + 1];
        // 割合計算
        float t = (length - l) / distance;

        // 位置計算
        return Vector3.Lerp(begin, end, t);

    }
	// マテリアルを次のベルトコンベアに移す
	public virtual void NextConveyor(RhythmMaterial material)
	{
		// 次のベルトコンベアがない
		if (ToConveyor == null)
			return;

		// マテリアルの設定
		ToConveyor.AddMaterial(material);
		// 現在のベルトコンベアから削除
		RemoveMaterial(material);

    }
    // 繋がっているベルトコンベアのノーツを取得する
    public List<RhythmMaterial> GetMaterialsConnectingBelt()
    {
        return GetPreviousMaterials().Concat(GetBackwardMaterials()).Distinct().ToList();
    }
    // 前側のベルトコンベアのノーツ取得
    public List<RhythmMaterial> GetPreviousMaterials()
    {
        // 前方にベルトコンベアがある
        if (FromConveyor)
        {
            return FromConveyor.GetPreviousMaterials().Concat(RhythmMaterials).ToList();
        }
        // 一番端
        else
        {
            return RhythmMaterials;
        }
    }
    // 後方のベルトコンベアのノーツ取得
    public List<RhythmMaterial> GetBackwardMaterials()
    {
        // 後方にベルトコンベアがある
        if (ToConveyor)
        {
            return ToConveyor.GetBackwardMaterials().Concat(RhythmMaterials).ToList();
        }
        else
        {
            return RhythmMaterials;
        }
    }


	// ****************************** プロパティ ****************************** //
	// 上書きを許可
	public bool PermitOverwrite
    {
        get { return m_permitOverwrite; }
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



}
