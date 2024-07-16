using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Branch : BeltConveyor
{
    [Header("分岐先")]
    [SerializeField] private List<BeltConveyor> m_beltConveyor = null;

    // 次の分岐先
    private int m_nextBranch;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	// ノーツを次のベルトコンベアへ
	public override void NextConveyor(RhythmMaterial material)
	{
        // 次のベルトコンベア
        BeltConveyor belt = m_beltConveyor[m_nextBranch];

		// 次のベルトコンベアがない
		if (belt == null)
			return;

		// マテリアルの設定
		belt.AddMaterial(material);
		// 現在のベルトコンベアから削除
		RemoveMaterial(material);

        // 次のベルトコンベア決定
        m_nextBranch++;
        if (m_nextBranch >= m_beltConveyor.Count)
        {
            m_nextBranch = 0;
        }
	}
}
