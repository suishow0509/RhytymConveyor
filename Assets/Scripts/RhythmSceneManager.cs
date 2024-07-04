using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmSceneManager : MonoBehaviour
{
    [Header("フレームレート")]
    [SerializeField] private int m_frameRate = 60;

    private float m_sceneTimer = 0.0f;



	private void Awake()
	{
        // フレームレート設定
        Application.targetFrameRate = m_frameRate;
	}

	// Start is called before the first frame update
	void Start()
    {
        // タイマー初期化
        m_sceneTimer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        // タイマー作動
        m_sceneTimer += Time.deltaTime;

    }



    // タイマー
    public float SceneTimer
    {
        get { return m_sceneTimer; }
    }

}
