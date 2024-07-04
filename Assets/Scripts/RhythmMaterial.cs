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

    [Header("判定タイミング(ms)")]
    [SerializeField] private float m_timing;

    [Header("現在乗っているベルトコンベア")]
    [SerializeField] private BeltConveyor m_currentBeltConveyor = null;

    [Header("ノーツの状態")]
    [SerializeField] private MaterialState m_materialState = MaterialState.PASS;

    // ノーツ発生からの時間
    private float m_timer = 0.0f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // タイマー加算
        m_timer += Time.deltaTime;

        // ベルトコンベアの始点
        Vector2 conveyorBegin = m_currentBeltConveyor.ConveyorBegin;
        // ベルトコンベアの終点
        Vector2 conveyorEnd = m_currentBeltConveyor.ConveyorEnd;
        // ベルトコンベアの制御点
        Vector2 conveyorControl = m_currentBeltConveyor.ControlPoint;
        // 時間
        float t = (m_timer - m_currentBeltConveyor.GetCurrentLocationTime()) / m_currentBeltConveyor.GetBeltPassTime();

        // 時間が 1 以上
        if (t >= 1.0f)
        {
            NextConveyor();
        }

        // 座標
        Vector2 pos = MyFunction.GetPointOnBezierCurve(conveyorBegin, conveyorEnd, conveyorControl, t);
        transform.position = pos;

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



    private void NextConveyor()
    {
        // 次のベルトコンベアがない
        if (m_currentBeltConveyor.ToConveyor == null)
            return;

        // 次のベルトコンベアを設定する
        m_currentBeltConveyor = m_currentBeltConveyor.ToConveyor;

    }

}
