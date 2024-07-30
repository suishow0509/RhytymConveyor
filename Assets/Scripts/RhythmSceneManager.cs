using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmSceneManager : MonoBehaviour
{
    [Header("BPM")]
    [SerializeField] private int m_beatsPerMinute = 60;
    [Header("�I�[�f�B�I�\�[�X")]
    [SerializeField] private AudioSource m_audioSource = null;
    [Header("�I�[�f�B�I�N���b�v")]
    [SerializeField] private AudioClip m_audioClip = null;


    //[Header("�t���[�����[�g")]
    //[SerializeField] private int m_frameRate = 60;

    private float m_sceneTimer = 0.0f;



	private void Awake()
	{
        //// �t���[�����[�g�ݒ�
        //Application.targetFrameRate = m_frameRate;
	}

	// Start is called before the first frame update
	void Start()
    {
        // �^�C�}�[������
        m_sceneTimer = 0.0f;

        // ����炵�����鏈��
        InvokeRepeating("PlayBeat", 0.0f, 60.0f / m_beatsPerMinute);

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



    // ����炷
    private void PlayBeat()
    {
        m_audioSource.Play();
    }

}
