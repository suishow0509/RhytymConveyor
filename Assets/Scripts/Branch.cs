using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Branch : BeltConveyor
{
    [Header("�����")]
    [SerializeField] private List<BeltConveyor> m_beltConveyor = null;

    // ���̕����
    private int m_nextBranch;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	// �m�[�c�����̃x���g�R���x�A��
	public override void NextConveyor(RhythmMaterial material)
	{
        // ���̃x���g�R���x�A
        BeltConveyor belt = m_beltConveyor[m_nextBranch];

		// ���̃x���g�R���x�A���Ȃ�
		if (belt == null)
			return;

		// �}�e���A���̐ݒ�
		belt.AddMaterial(material);
		// ���݂̃x���g�R���x�A����폜
		RemoveMaterial(material);

        // ���̃x���g�R���x�A����
        m_nextBranch++;
        if (m_nextBranch >= m_beltConveyor.Count)
        {
            m_nextBranch = 0;
        }
	}
}
