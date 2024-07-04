using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmSceneManager : MonoBehaviour
{
    [Header("�t���[�����[�g")]
    [SerializeField] private int m_frameRate = 60;

    private float m_sceneTimer = 0.0f;



	private void Awake()
	{
        // �t���[�����[�g�ݒ�
        Application.targetFrameRate = m_frameRate;
	}

	// Start is called before the first frame update
	void Start()
    {
        // �^�C�}�[������
        m_sceneTimer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        // �^�C�}�[�쓮
        m_sceneTimer += Time.deltaTime;

    }



    // �^�C�}�[
    public float SceneTimer
    {
        get { return m_sceneTimer; }
    }

}
