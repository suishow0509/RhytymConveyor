using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressedMachine : MonoBehaviour
{
    [Header("設定キー")]
    [SerializeField] private KeyCode m_keyCode = KeyCode.Space;

    [Header("発生源からの時間距離(秒)")]
    [SerializeField] private float m_timeDistance = 1.0f;

    [Header("プレスのクールタイム")]
    [SerializeField] private float m_pressCoolTime = 0.0f; 
    private float m_timer = 0.0f;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // クールタイム中
        if (m_timer > 0.0f)
        {
            // 時間経過
            m_timer -= Time.deltaTime;
            return;
        }

        // プレス
        if (Input.GetKeyDown(m_keyCode))
        {

        }
        
    }
}
