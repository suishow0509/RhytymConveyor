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

    [Header("判定タイミング(ms)")]
    [SerializeField] private float m_timing;

    [Header("現在乗っているベルトコンベア")]
    [SerializeField] private BeltConveyor m_currentBeltConveyor = null;

    [Header("ノーツの状態")]
    [SerializeField] private MaterialState m_materialState = MaterialState.PASS;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // ベルトコンベアの始点
        Vector2 conveyorBegin = m_currentBeltConveyor.ConveyorBegin;
        // ベルトコンベアの終点
        Vector2 conveyorEnd = m_currentBeltConveyor.ConveyorEnd;
        // ベルトコンベアの制御点
        Vector2 conveyorControl = m_currentBeltConveyor.ControlPoint;



    }


    // 現在のベルトコンベア
    public BeltConveyor CurrentBeltConveyor
    {
        set { m_currentBeltConveyor = value; }
    }

}
