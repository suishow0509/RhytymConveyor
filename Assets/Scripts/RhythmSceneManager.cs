using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmSceneManager : MonoBehaviour
{
    [Header("BPM")]
    [SerializeField] private int m_beatsPerMinute = 60;
    [Header("オーディオソース")]
    [SerializeField] private AudioSource m_audioSource = null;
    [Header("オーディオクリップ")]
    [SerializeField] private AudioClip m_audioClip = null;


    //[Header("フレームレート")]
    //[SerializeField] private int m_frameRate = 60;

    private float m_sceneTimer = 0.0f;



	private void Awake()
	{
        //// フレームレート設定
        //Application.targetFrameRate = m_frameRate;
	}

	// Start is called before the first frame update
	void Start()
    {
        // タイマー初期化
        m_sceneTimer = 0.0f;

        // 音を鳴らし続ける処理
        InvokeRepeating("PlayBeat", 0.0f, 60.0f / m_beatsPerMinute);

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



    // 音を鳴らす
    private void PlayBeat()
    {
        m_audioSource.Play();
    }

}
