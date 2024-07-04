using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmMaterialSource : BeltConveyor
{
    [System.Serializable]
    public struct MaterialTiming
    {
        public float time;      // ノーツのタイミング
        public float step;      // ノーツの音程
    }

    [Header("発生間隔")]
    [SerializeField] private MaterialTiming[] m_timings = null;

    [Header("ノーツ")]
    [SerializeField] private RhythmMaterial m_rhythmMaterial = null;
    private int m_notes = 0;

    [Header("シーンマネージャ")]
    [SerializeField] private RhythmSceneManager m_sceneManager = null;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.P))
        {
            PopMaterial();
        }

    }

    // ノーツ発生
    public void PopMaterial()
    {
        // 生成
        RhythmMaterial rhythm = Instantiate(m_rhythmMaterial, ToConveyor.transform.position, Quaternion.identity);
        // コンベアの設定
        rhythm.CurrentBeltConveyor = ToConveyor;
    }
}
