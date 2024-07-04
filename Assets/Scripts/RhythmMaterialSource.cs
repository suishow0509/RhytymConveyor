using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmMaterialSource : BeltConveyor
{
    [System.Serializable]
    public struct MaterialTiming
    {
        public float time;      // �m�[�c�̃^�C�~���O
        public float step;      // �m�[�c�̉���
    }

    [Header("�����Ԋu")]
    [SerializeField] private MaterialTiming[] m_timings = null;

    [Header("�m�[�c")]
    [SerializeField] private RhythmMaterial m_rhythmMaterial = null;
    private int m_notes = 0;

    [Header("�V�[���}�l�[�W��")]
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

    // �m�[�c����
    public void PopMaterial()
    {
        // ����
        RhythmMaterial rhythm = Instantiate(m_rhythmMaterial, ToConveyor.transform.position, Quaternion.identity);
        // �R���x�A�̐ݒ�
        rhythm.CurrentBeltConveyor = ToConveyor;
    }
}
